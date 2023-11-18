using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Reactive
{
    /// <summary>
    /// <seealso cref="IDisposable.Dispose"/>を呼び出すと通知される機能を提供するインターフェース
    /// </summary>
    public interface IDisposalNotifier : IDisposable, IObservable<Unit>;

    internal class DisposalNotifier : IDisposalNotifier
    {
        #region Private Field
        private readonly IDisposable baseDisposable;
        private readonly Subject<Unit> disposalSubject;
        #endregion

        #region Constructor
        public DisposalNotifier( IDisposable baseDisposable)
        {
            this.disposalSubject = new();
            this.baseDisposable = baseDisposable;
        }

        #region Method
        public IDisposable Subscribe(IObserver<Unit> observer)
        {
            return disposalSubject.Subscribe(observer);
        }

        public void Dispose()
        {
            // オブザーバーに元のオブジェクトのDisposeが呼ばれる手前であることを通知する。
            disposalSubject.OnNext(Unit.Default);
            disposalSubject.OnCompleted();

            baseDisposable.Dispose();

            disposalSubject.Dispose();
        }
        #endregion
        #endregion
    }

}
