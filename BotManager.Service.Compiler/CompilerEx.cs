using NLog.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Compiler
{
    /// <summary>
    /// コンパイラ拡張メソッド定義
    /// </summary>
    public static class CompilerEx
    {
        #region CompileFrom
        /// <summary>
        /// 指定したソースファイルコレクションをコンパイルします。
        /// </summary>
        /// <param name="compiler"></param>
        /// <param name="files">ソースファイルコレクション</param>
        /// <param name="encoding">文字コード</param>
        /// <param name="useBOM">BOM付きかどうか</param>
        /// <returns></returns>
        public static async Task CompileFrom(this ICompiler compiler, IEnumerable<FileInfo> files, Encoding encoding, bool useBOM)
        {
            compiler.ClearSources();
            foreach (var file in files)
            {
                await compiler.AddSourceFile(file.FullName, encoding, useBOM);
            }
            compiler.Compile();
        }

        /// <summary>
        /// 文字コードを BOM付きUTF-8として、指定したソースファイルコレクションをコンパイルします。
        /// </summary>
        /// <param name="compiler">ソースファイルコレクション</param>
        /// <param name="files"></param>
        /// <returns></returns>
        public static Task CompileFrom(this ICompiler compiler, IEnumerable<FileInfo> files)
        {
            return CompileFrom(compiler, files, Encoding.UTF8, true);
        }

        /// <summary>
        /// コンパイルしてからアセンブリ作成通知がされるまで待機します。
        /// </summary>
        /// <param name="compiler"></param>
        /// <returns></returns>
        public static async Task CompileAsync(this ICompiler compiler)
        {
            using(ReplaySubject<IAssembly> subject = new(1))
            {
                using (compiler.AssemblyCreated
                    .TakeUntil(compiler.CompileFailed)  // コンパイル失敗したら購読を止める
                    .Take(1)    // 通知は最初の1回だけ取ることが出来ればよい
                    .Subscribe(subject))
                {
                    compiler.Compile();
                    await subject.Count();
                }
            }
        }

        /// <summary>
        /// 指定したソースファイルコレクションをコンパイルし、アセンブリ作成通知がされるまで待機します。
        /// </summary>
        /// <param name="compiler"></param>
        /// <param name="files">ソースファイルコレクション</param>
        /// <param name="encoding">文字コード</param>
        /// <param name="useBOM">BOM付きかどうか</param>
        /// <returns></returns>
        public static async Task CompileFromAsync(this ICompiler compiler, IEnumerable<FileInfo> files, Encoding encoding, bool useBOM)
        {
            compiler.ClearSources();
            foreach (var file in files)
            {
                await compiler.AddSourceFile(file.FullName, encoding, useBOM);
            }
            await compiler.CompileAsync();
        }

        /// <summary>
        /// 文字コードを BOM付きUTF-8として、指定したソースファイルコレクションをコンパイルし、アセンブリ作成通知がされるまで待機します。
        /// </summary>
        /// <param name="compiler">ソースファイルコレクション</param>
        /// <param name="files"></param>
        /// <returns></returns>
        public static Task CompileFromAsync(this ICompiler compiler, IEnumerable<FileInfo> files)
        {
            return CompileFromAsync(compiler, files, Encoding.UTF8, true);
        }
        #endregion

        #region PreCompileFrom
        /// <summary>
        /// 指定したソースファイルコレクションをコンパイルします。
        /// </summary>
        /// <param name="compiler"></param>
        /// <param name="files">ソースファイルコレクション</param>
        /// <param name="encoding">文字コード</param>
        /// <param name="useBOM">BOM付きかどうか</param>
        /// <returns></returns>
        public static async Task PreCompileFrom(this IPrecompilableCompiler compiler, IEnumerable<FileInfo> files, Encoding encoding, bool useBOM)
        {
            compiler.ClearSources();
            foreach (var file in files)
            {
                await compiler.AddSourceFile(file.FullName, encoding, useBOM);
            }
            compiler.Compile();
        }

        /// <summary>
        /// 文字コードを BOM付きUTF-8として、指定したソースファイルコレクションをコンパイルします。
        /// </summary>
        /// <param name="compiler">ソースファイルコレクション</param>
        /// <param name="files"></param>
        /// <returns></returns>
        public static Task PreCompileFrom(this IPrecompilableCompiler compiler, IEnumerable<FileInfo> files)
        {
            return PreCompileFrom(compiler, files, Encoding.UTF8, true);
        }

        /// <summary>
        /// コンパイルしてからアセンブリ作成通知がされるまで待機します。
        /// </summary>
        /// <param name="compiler"></param>
        /// <returns></returns>
        public static async Task PreCompileAsync(this IPrecompilableCompiler compiler)
        {
            using (ReplaySubject<IAssembly> subject = new(1))
            {
                using (compiler.AssemblyCreated
                    .TakeUntil(compiler.CompileFailed)  // コンパイル失敗したら購読を止める
                    .Take(1)    // 通知は最初の1回だけ取ることが出来ればよい
                    .Subscribe(subject))
                {
                    compiler.Compile();
                    await subject.Count();
                }
            }
        }

        /// <summary>
        /// 指定したソースファイルコレクションをコンパイルし、アセンブリ作成通知がされるまで待機します。
        /// </summary>
        /// <param name="compiler"></param>
        /// <param name="files">ソースファイルコレクション</param>
        /// <param name="encoding">文字コード</param>
        /// <param name="useBOM">BOM付きかどうか</param>
        /// <returns></returns>
        public static async Task PreCompileFromAsync(this IPrecompilableCompiler compiler, IEnumerable<FileInfo> files, Encoding encoding, bool useBOM)
        {
            compiler.ClearSources();
            foreach (var file in files)
            {
                await compiler.AddSourceFile(file.FullName, encoding, useBOM);
            }
            await compiler.CompileAsync();
        }

        /// <summary>
        /// 文字コードを BOM付きUTF-8として、指定したソースファイルコレクションをコンパイルし、アセンブリ作成通知がされるまで待機します。
        /// </summary>
        /// <param name="compiler">ソースファイルコレクション</param>
        /// <param name="files"></param>
        /// <returns></returns>
        public static Task PreCompileFromAsync(this IPrecompilableCompiler compiler, IEnumerable<FileInfo> files)
        {
            return PreCompileFromAsync(compiler, files, Encoding.UTF8, true);
        }
        #endregion

        #region SetSourceFiles
        /// <summary>
        /// 指定したソースファイルを追加します。
        /// </summary>
        /// <param name="sourceIncluding"></param>
        /// <param name="files">ソースファイルコレクション</param>
        /// <param name="encoding">文字コード</param>
        /// <param name="useBOM">BOM付きかどうか</param>
        /// <returns></returns>
        public static async Task SetSourceFilesAsync(this ISourceIncluding sourceIncluding, IEnumerable<FileInfo> files, Encoding encoding, bool useBOM)
        {
            sourceIncluding.ClearSources();
            foreach(var file in files)
            {
                await sourceIncluding.AddSourceFile(file.FullName, encoding, useBOM);
            }
        }

        /// <summary>
        /// 文字コードを BOM付きUTF-8として、指定したソースファイルを追加します。
        /// </summary>
        /// <param name="sourceIncluding"></param>
        /// <param name="files"></param>
        /// <returns></returns>
        public static Task SetSourceFilesAsync(this ISourceIncluding sourceIncluding, IEnumerable<FileInfo> files)
        {
            return SetSourceFilesAsync(sourceIncluding, files, Encoding.UTF8, true);
        }
        #endregion
    }
}
