using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Compiler
{
    /// <summary>
    /// コンパイラインターフェース
    /// </summary>
    public interface ICompiler : IDisposable, ICompilerNotifier, ISourceIncluding
    {
        /// <summary>
        /// 現在の設定でソースコードをコンパイルします。
        /// </summary>
        void Compile();
    }
}
