﻿using BotManager.Common;
using BotManager.Common.Scripting;
using BotManager.Common.Scripting.Attributes;
using BotManager.Database.Entities;
using BotManager.Service.Compiler;
using BotManager.Service.Git;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Engine
{
    /// <summary>
    /// BotManager 実行機関クラス
    /// </summary>
    internal sealed class BotMechanism<SubscriptionArgument> : IBotMechanism<SubscriptionArgument>
        where SubscriptionArgument : ISubscriptionArguments, new()
    {
        #region Private Fields
        private readonly IPrecompilableCompiler compiler;
        private BotManager? _botManager;
        private Func<INamed, IBotManager, CancellationToken, SubscriptionArgument> gettingSubscriptionArgument;
        private ILog logger;    // コンパイラ用ロガー
        private readonly static ILog Logger = Log.GetLogger("engine");

        private CancellationDisposable cancellation;

        private readonly IDisposable compileSubscription;
        #endregion

        #region Constructor
        public BotMechanism(IPrecompilableCompiler compiler, Func<INamed, IBotManager, CancellationToken, SubscriptionArgument> gettingSubscriptionArgument)
        {
            this.logger = Log.GetLogger("compiler");
            this.gettingSubscriptionArgument = gettingSubscriptionArgument;
            this.compiler = compiler;
            compileSubscription = CompileSubscription();
            cancellation = new();
            Logger.Debug("BotMechanism Created");
        }

        private IDisposable CompileSubscription()
        {
            CompositeDisposable disposables = new();

            // コンパイルエラー時
            disposables.Add(
                compiler.CompileError.Subscribe(errors =>
                {
                    foreach(var group in errors.GroupBy(d => d.Location.SourceTree?.FilePath ?? ""))
                    {
                        logger.Info(group.Key);
                        foreach (var diag in group)
                        {
                            string message = string.Format("[{0}][{1}]L{2} : {3}",
                                diag.Severity.ToString(),
                                diag.Id,
                                diag.Location.GetLineSpan().StartLinePosition.ToString(),
                                diag.GetMessage()
                                );

                            switch (diag.Severity)
                            {
                                case Microsoft.CodeAnalysis.DiagnosticSeverity.Hidden:
                                    logger.Debug(message);
                                    break;
                                case Microsoft.CodeAnalysis.DiagnosticSeverity.Info:
                                    logger.Info(message);
                                    break;
                                case Microsoft.CodeAnalysis.DiagnosticSeverity.Warning:
                                    logger.Warn(message);
                                    break;
                                case Microsoft.CodeAnalysis.DiagnosticSeverity.Error:
                                    logger.Error(message);
                                    break;
                            }
                        }
                    }
                })
            );

            // アセンブリアンロード要求通知時
            disposables.Add(
                compiler.AssemblyUnloading.Subscribe(_ =>
                {
                    logger.Info("アセンブリアンロード要求");
                    cancellation.Dispose();
                    cancellation = new();
                    _botManager?.Dispose();
                })
            );

            disposables.Add(
                compiler.CompileSuccess.Subscribe(_ =>
                {
                    logger.Info("コンパイル成功");
                })
            ); ;

            disposables.Add(
                compiler.CompileFailed.Subscribe(_ =>
                {
                    logger.Warn("コンパイル失敗");
                })
            );

            // コンパイル成功時
            disposables.Add(
                compiler.AssemblyCreated.Subscribe(async asm =>
                {
                    logger.Info("アセンブリが作成されました。");
                    _botManager = new BotManager();
                    List<Task> tasklist = new();
                    var types = asm.GetTypes().ToObservable();

                    // Botコンパイル
                    var botobs = types
                        .HasAttribute<BotAttribute>()
                        .NotHaveAttribute<ObsoleteAttribute>()
                        .NotHaveAttribute<IgnoreAttribute>()
                        .NewAs<IBot>()
                        .Publish();

                    botobs.Subscribe(bot =>
                    {
                        _botManager.AddBot(bot);
                        logger.Info($"{bot.Name} Bot({bot.Id}) 追加");
                        tasklist.Add(bot.StartAsync());
                        logger.Info($"Bot {bot.Id}の起動を開始しました。");
                    });

                    // Botが全てコンパイル、
                    using (botobs.Connect())
                    {
                        await botobs.Count();
                    }
                    await Task.WhenAll(tasklist);
                    logger.Info($"すべてのBotの起動が完了しました！");

                    // コマンドのコンパイル
                    var subscobs = types
                        .HasAttribute<ActionAttribute>()
                        .NotHaveAttribute<ObsoleteAttribute>()
                        .NotHaveAttribute<IgnoreAttribute>()
                        .NewAs<ISubscription<SubscriptionArgument>>()
                        .Subscribe(s =>
                        {
                            var args = gettingSubscriptionArgument(s, _botManager, cancellation.Token);
                            var subscription = s.SubscribeFrom(args);
                            logger.Info($"サブスクリプション {s.Name}({s.Id}) 開始");

                            // アセンブリのアンロードが要求されたらサブスクリプションを解除する
                            compiler.AssemblyUnloading
                                    .Take(1)
                                    .Subscribe(_ => subscription.Dispose());
                        });
                })
            );

            return disposables;
        }
        #endregion

        #region Property
        public IPrecompilable PreCompiler => compiler;
        #endregion

        public async Task CompileSources()
        {
            Logger.Info("BotMechanism Start");
            // Gitリポジトリからスクリプトファイルを取得
            var directory = GitPullAndGetDirectory();

            // コンパイル(C#)
            await compiler.CompileFromAsync(directory.EnumerateFiles("*.cs", SearchOption.AllDirectories));
        }

        private DirectoryInfo GitPullAndGetDirectory()
        {
            var setting = AppSettings.Script;
            using(var repos = GetScriptRepositry())
            {
                // GitCheckout
                repos.Checkout(setting.BranchName);

                return repos.LocalDirectory.DirectoryInfo;
            }
        }

        public IGitRepositry GetScriptRepositry()
        {
            var setting = AppSettings.Script;
            return Git.GetOrClone(setting.RepositoryUrl, setting.Path);
        }

        public void Dispose()
        {
            Logger.Debug("BotMechanism Disposing");
            compileSubscription.Dispose();
            _botManager?.Dispose();
            compiler.Dispose();
            Logger.Debug("BotMechanism Disposed");
        }
    }
}
