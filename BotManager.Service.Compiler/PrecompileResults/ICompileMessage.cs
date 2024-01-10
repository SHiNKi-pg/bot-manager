using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Compiler.Results
{
    /// <summary>
    /// コンパイルメッセージインターフェース
    /// </summary>
    public interface ICompileMessage : ICompileResult
    {
        /// <summary>
        /// コンパイルメッセージ
        /// </summary>
        IEnumerable<Diagnostic> Messages { get; }
    }
}
