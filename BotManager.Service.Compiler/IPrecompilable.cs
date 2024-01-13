using BotManager.Service.Compiler.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Compiler
{
    /// <summary>
    /// コンパイルの結果のみ通知する機能を提供するインターフェース
    /// </summary>
    public interface IPrecompilable
    {
        /// <summary>
        /// 現在の設定で試験的にコンパイルを実行し、結果を通知します。このメソッドではコンパイルしたアセンブリは通知されません。通常は<seealso cref="ICompiler.Compile"/>を実行する前に呼び出します。
        /// </summary>
        /// <returns></returns>
        IObservable<ICompileResult> Precompile();
    }

    /// <summary>
    /// <seealso cref="IPrecompilable"/>と <seealso cref="ICompiler"/>インターフェースを両方実装したインターフェース
    /// </summary>
    public interface IPrecompilableCompiler : IPrecompilable, ICompiler
    {

    }
}
