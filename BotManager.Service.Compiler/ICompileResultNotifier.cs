using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Compiler
{
    /// <summary>
    /// コンパイル結果を通知する機能を持つインターフェース
    /// </summary>
    public interface ICompileResultNotifier
    {
        /// <summary>
        /// コンパイル時にエラーや警告、情報メッセージ含むコンパイルメッセージを通知します。これはコンパイルの成否に関わらず、コンパイルすると必ず通知されます。
        /// </summary>
        IObservable<IEnumerable<Diagnostic>> CompileError { get; }

        /// <summary>
        /// コンパイルに失敗した時に通知されます。
        /// </summary>
        IObservable<Unit> CompileFailed { get; }

        /// <summary>
        /// コンパイルに成功した時に通知されます。
        /// </summary>
        IObservable<Unit> CompileSuccess { get; }
    }
}
