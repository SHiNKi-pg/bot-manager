using NLog.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Compiler
{
    /// <summary>
    /// コンパイラ拡張メソッド定義
    /// </summary>
    public static class CompilerEx
    {
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
    }
}
