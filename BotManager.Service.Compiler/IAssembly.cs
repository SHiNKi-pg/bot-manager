using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Compiler
{
    /// <summary>
    /// アセンブリインターフェース
    /// </summary>
    public interface IAssembly : IDisposable
    {
        /// <summary>
        /// このアセンブリが破棄される時に通知されます。
        /// </summary>
        IObservable<Unit> Disposing { get; }

        /// <summary>
        /// 指定した名前の型の<see cref="Type"/>オブジェクトを返します。
        /// </summary>
        /// <param name="typeName">型名</param>
        /// <returns></returns>
        IObservable<Type> GetType(string typeName);

        /// <summary>
        /// このアセンブリに存在する型を列挙します。
        /// </summary>
        /// <returns></returns>
        IEnumerable<Type> GetTypes();

        /// <summary>
        /// このアセンブリが破棄されているかどうかを取得します。
        /// </summary>
        bool IsDisposed { get; }
    }
}
