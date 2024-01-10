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
    /// コンパイラ通知インターフェース
    /// </summary>
    public interface ICompilerNotifier : ICompileResultNotifier
    {
        /// <summary>
        /// コンパイルしたアセンブリがアンロードされる時に通知されます。このオブジェクトから通知された時、アセンブリの参照を全て解除する必要があります。
        /// </summary>
        IObservable<Unit> AssemblyUnloading { get; }

        /// <summary>
        /// コンパイルが成功し、コンパイル済みアセンブリが作成された時に通知されます。再コンパイルする可能性がある場合は、<see cref="AssemblyUnloading"/>から通知を受け取った時にこのアセンブリの参照をすべて解除するような仕組みにする必要があります。
        /// </summary>
        IObservable<IAssembly> AssemblyCreated { get; }
    }
}
