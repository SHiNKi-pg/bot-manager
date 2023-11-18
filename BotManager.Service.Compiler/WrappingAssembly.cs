using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Disposables;

namespace BotManager.Service.Compiler
{
    internal class WrappingAssembly : IAssembly
    {
        private Assembly? assembly;

        private Subject<Unit> _disposing;

        /// <summary>
        /// このアセンブリが破棄される時に通知されます。
        /// </summary>
        public IObservable<Unit> Disposing { get => _disposing.AsObservable(); }

        public WrappingAssembly(Assembly assembly)
        {
            this._disposing = new();
            this.assembly = assembly;
        }

        /// <summary>
        /// 指定した名前の型の<see cref="Type"/>オブジェクトを返します。
        /// </summary>
        /// <param name="typeName">型名</param>
        /// <returns></returns>
        public IObservable<Type> GetType(string typeName)
        {
            return Observable.Create<Type>(obsr =>
            {
                if (assembly is not null)
                {
                    var type = assembly.GetType(typeName);
                    if (type is not null)
                        obsr.OnNext(type);
                }
                obsr.OnCompleted();
                return Disposable.Empty;
            });
        }

        /// <summary>
        /// このアセンブリに存在する型を列挙します。
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Type> GetTypes()
        {
            return assembly!.GetTypes();
        }

        /// <summary>
        /// このアセンブリが破棄されているかどうかを取得します。
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// このアセンブリが使用しているリソースを破棄します。
        /// </summary>
        public void Dispose()
        {
            _disposing.OnNext(Unit.Default);
            _disposing.Dispose();
            this.assembly = null;
            this.IsDisposed = true;
        }
    }
}
