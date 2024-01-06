﻿using BotManager.Common;
using BotManager.Common.Scripting;
using BotManager.Common.Scripting.Attributes;
using BotManager.Service.Compiler;
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
    internal sealed class BotMechanism : IBotMechanism
    {
        #region Private Fields
        private readonly Compiler compiler;
        private BotManager? _botManager;

        private readonly IDisposable compileSubscription;
        #endregion

        #region Constructor
        public BotMechanism(string assemblyName)
        {
            compiler = new Compiler(assemblyName);
            compileSubscription = CompileSubscription();
        }

        private IDisposable CompileSubscription()
        {
            CompositeDisposable disposables = new();

            // コンパイルエラー時
            disposables.Add(
                compiler.CompileError.Subscribe(errors =>
                {
                    // TODO: エラーや警告の内容を表示する
                })
            );

            // アセンブリアンロード要求通知時
            disposables.Add(
                compiler.AssemblyUnloading.Subscribe(_ =>
                {
                    _botManager?.Dispose();
                })
            );

            // コンパイル成功時
            disposables.Add(
                compiler.AssemblyCreated.Subscribe(async asm =>
                {
                    _botManager = new BotManager();
                    List<Task> tasklist = new();
                    var types = asm.GetTypes().ToObservable();

                    // Botコンパイル
                    var botobs = types
                        .HasAttribute<BotAttribute>()
                        .NewAs<IBot>()
                        .Publish();

                    botobs.Subscribe(bot =>
                    {
                        _botManager.AddBot(bot);
                        tasklist.Add(bot.StartAsync());
                    });

                    // Botが全てコンパイル、
                    using (botobs.Connect())
                    {
                        await botobs.Count();
                    }
                    await Task.WhenAll(tasklist);

                    // コマンドのコンパイル
                    var subscobs = types
                        .HasAttribute<ActionAttribute>()
                        .NewAs<ISubscription>()
                        .Subscribe(s =>
                        {
                            ISubscriptionArguments args = new SubscriptionArguments()
                            {
                                BotManager = _botManager,
                            };
                            var subscription = s.SubscribeFrom(args);

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

        public Task Start()
        {
            // TODO: 開始した時に行う処理
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            compileSubscription.Dispose();
            _botManager?.Dispose();
            compiler.Dispose();
        }
    }
}