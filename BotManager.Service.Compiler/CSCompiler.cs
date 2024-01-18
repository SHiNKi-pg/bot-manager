using BotManager.Service.Compiler.Results;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Compiler
{
    /// <summary>
    /// C#コンパイラークラス
    /// </summary>
    public class CSharpCompiler : ICompiler, IDisposable, IPrecompilableCompiler
    {
        #region Private Fields

        private AssemblyLoadContext? assemblyLoadContext;

        private readonly string assemblyName;

        private CSharpParseOptions parseOptions;

        private CSharpCompilationOptions compilationOptions;

        private List<MetadataReference> references;

        private List<SyntaxTree> syntaxTrees;

        private Subject<Unit> _AssemblyUnloading;

        private Subject<IAssembly> _AssemblyCreated;

        private Subject<IEnumerable<Diagnostic>> _CompileError;

        private Subject<Unit> _CompileFailed;

        private Subject<Unit> _CompileSuccess;
        #endregion

        /// <summary>
        /// <see cref="CSharpCompiler"/>オブジェクトを作成します。
        /// </summary>
        /// <param name="assemblyName">アセンブリ名</param>
        /// <param name="languageVersion">言語バージョン</param>
        public CSharpCompiler(string assemblyName, LanguageVersion languageVersion = LanguageVersion.Latest)
        {
            this._AssemblyUnloading = new();
            this._AssemblyCreated = new();
            this._CompileError = new();
            this._CompileFailed = new();
            this._CompileSuccess = new();
            this.assemblyName = assemblyName;
            this.assemblyLoadContext = new(assemblyName, true);
            this.parseOptions = new(languageVersion);
            this.references = new();
            this.syntaxTrees = new();
            this.compilationOptions = new(OutputKind.DynamicallyLinkedLibrary);
            ClearImports();
        }

        /// <summary>
        /// .NET標準アセンブリのディレクトリパス
        /// </summary>
        public static string AssemblyDirectoryPath { get
            {
                // objectのTypeで.NETランタイムのディレクトリを取得する
                return Path.GetDirectoryName(typeof(object).Assembly.Location)!;
            }
        }

        /// <summary>
        /// コンパイルしたアセンブリがアンロードされる時に通知されます。このオブジェクトから通知された時、アセンブリの参照を全て解除する必要があります。
        /// </summary>
        public IObservable<Unit> AssemblyUnloading { get => _AssemblyUnloading.AsObservable(); }

        /// <summary>
        /// コンパイルが成功し、コンパイル済みアセンブリが作成された時に通知されます。再コンパイルする可能性がある場合は、<see cref="AssemblyUnloading"/>から通知を受け取った時にこのアセンブリの参照をすべて解除するような仕組みにする必要があります。
        /// </summary>
        public IObservable<IAssembly> AssemblyCreated { get => _AssemblyCreated.AsObservable(); }

        /// <summary>
        /// コンパイル時にエラーや警告、情報メッセージ含むコンパイルメッセージを通知します。これはコンパイルの成否に関わらず、コンパイルすると必ず通知されます。
        /// </summary>
        public IObservable<IEnumerable<Diagnostic>> CompileError { get => _CompileError.AsObservable(); }

        /// <summary>
        /// コンパイルに失敗した時に通知されます。
        /// </summary>
        public IObservable<Unit> CompileFailed { get => _CompileFailed.AsObservable(); }

        /// <summary>
        /// コンパイルに成功した時に通知されます。<see cref="AssemblyCreated"/>とは異なり、アセンブリが通知されません。
        /// </summary>
        public IObservable<Unit> CompileSuccess { get => _CompileSuccess.AsObservable(); }

        /// <summary>
        /// 現在の設定でコンパイルします。
        /// </summary>
        public void Compile()
        {
            var compilation = CSharpCompilation.Create(assemblyName, syntaxTrees, references, compilationOptions);
            using(var ms = new MemoryStream())
            {
                // コンパイル
                var result = compilation.Emit(ms);
                // コンパイルエラーや警告をオブザーバーに通知
                _CompileError.OnNext(result.Diagnostics);
                if (result.Success)
                {
                    // コンパイル成功
                    _CompileSuccess.OnNext(Unit.Default);
                    if (assemblyLoadContext is not null)
                    {
                        if (assemblyLoadContext.Assemblies.Any())
                        {
                            // 前回コンパイルしたものはアンロードする
                            // アンロードする時にアンロードする旨を通知する。
                            _AssemblyUnloading.OnNext(Unit.Default);
                            assemblyLoadContext.Unload();
                            assemblyLoadContext = null;
                            // AssemblyLoadContextオブジェクトの作成し直し
                            assemblyLoadContext = new(assemblyName, true);
                        }
                        ms.Seek(0, SeekOrigin.Begin);
                        var asm = assemblyLoadContext.LoadFromStream(ms);
                        // コンパイルしたアセンブリの通知
                        _AssemblyCreated.OnNext(new WrappingAssembly(asm));
                    }
                }
                else
                {
                    // コンパイルエラー
                    _CompileFailed.OnNext(Unit.Default);
                }
            }
        }

        /// <summary>
        /// このオブジェクトで使用しているリソースを全て破棄します。
        /// </summary>
        public void Dispose()
        {
            if (assemblyLoadContext is not null && assemblyLoadContext.Assemblies.Any())
            {
                _AssemblyUnloading.OnNext(Unit.Default);
                assemblyLoadContext.Unload();
                assemblyLoadContext = null;
            }
            _AssemblyCreated.Dispose();
            _CompileError.Dispose();
            _AssemblyUnloading.Dispose();
            _CompileSuccess.Dispose();
            _CompileFailed.Dispose();
        }

        /// <summary>
        /// 指定した型をインポートします。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Import<T>()
        {
            Import(typeof(T).Assembly.Location);
        }

        /// <summary>
        /// 指定したファイルから型をインポートします。
        /// </summary>
        /// <param name="filePath"></param>
        public void Import(string filePath)
        {
            references.Add(MetadataReference.CreateFromFile(filePath));
        }

        /// <summary>
        /// 指定した型をインポートします。
        /// </summary>
        /// <param name="type"></param>
        public void Import(Type type)
        {
            Import(type.Assembly.Location);
        }

        /// <summary>
        /// インポート情報をクリアします。
        /// </summary>
        public void ClearImports()
        {
            references.Clear();
            Import<object>();
            Import($"{AssemblyDirectoryPath}/mscorlib.dll");
            Import($"{AssemblyDirectoryPath}/System.Runtime.dll");
        }

        /// <summary>
        /// ソースコードを追加します。
        /// </summary>
        /// <param name="source"></param>
        public void AddSource(string source)
        {
            syntaxTrees.Add(CSharpSyntaxTree.ParseText(source, parseOptions));
        }

        /// <summary>
        /// ソースファイルを追加します。
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="encoding"></param>
        /// <param name="usingBOM"></param>
        /// <returns></returns>
        public async Task AddSourceFile(string filePath, Encoding encoding, bool usingBOM)
        {
            using(StreamReader sr = new(filePath, encoding))
            {
                syntaxTrees.Add(CSharpSyntaxTree.ParseText(await sr.ReadToEndAsync(), parseOptions, filePath));
            }
        }

        /// <summary>
        /// ソースコードをクリアします。
        /// </summary>
        public void ClearSources()
        {
            syntaxTrees.Clear();
        }

        #region Precompile
        /// <summary>
        /// 現在の設定で試験的にコンパイルを実行し、結果を通知します。このメソッドではコンパイルしたアセンブリは通知されません。通常は<seealso cref="Compile"/>を実行する前に呼び出します。
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IObservable<ICompileResult> Precompile()
        {
            return Observable.Create<ICompileResult>(observer =>
            {
                Random rnd = new();
                string tmpAssemblyName = $"assemblyName_{rnd.Next(10000)}_{rnd.Next(10000)}_{rnd.Next(10000)}";
                var compilation = CSharpCompilation.Create(tmpAssemblyName, syntaxTrees, references, compilationOptions);

                var ms = new MemoryStream();
                try
                {
                    // コンパイル
                    var result = compilation.Emit(ms);
                    observer.OnNext(new PreCompileSuccess(result.Success));

                    // コンパイルエラーや警告をオブザーバーに通知
                    observer.OnNext(new PreCompileMessage(result.Diagnostics));
                }catch(Exception ex)
                {
                    observer.OnError(ex);
                }
                observer.OnCompleted();
                return ms;
            });
        }

        private class PreCompileSuccess : ICompileSuccessed
        {
            public bool Success { get; }

            public PreCompileSuccess(bool success)
            {
                this.Success = success;
            }
        }

        private class PreCompileMessage : ICompileMessage
        {
            public IEnumerable<Diagnostic> Messages { get; }

            public PreCompileMessage(IEnumerable<Diagnostic> messages)
            {
                Messages = messages;
            }
        }
        #endregion
    }
}
