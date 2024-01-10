using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Compiler.Results
{
    /// <summary>
    /// コンパイル成否インターフェース
    /// </summary>
    public interface ICompileSuccessed : ICompileResult
    {
        /// <summary>
        /// コンパイルに成功したかどうか取得します。
        /// </summary>
        bool Success { get; }
    }
}
