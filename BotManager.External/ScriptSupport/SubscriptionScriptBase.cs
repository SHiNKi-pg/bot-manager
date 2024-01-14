using BotManager.Common.Scripting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.External.ScriptSupport
{
    /// <summary>
    /// <see cref="ISubscription{Args}"/>を実装した基本クラス。これを継承するとサブスクリプションスクリプトを作成できます。
    /// 派生クラスでは引数0の publicコンストラクタを作成しなければなりません。
    /// </summary>
    public abstract class SubscriptionScriptBase : ISubscription<SubscriptionArguments>
    {
        /// <summary>
        /// サブスクリプションID
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// サブスクリプション名
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// <see cref="SubscriptionScriptBase"/>のコンストラクタ。派生クラスでは引数0の publicコンストラクタを作成しなければなりません。
        /// </summary>
        /// <param name="id">サブスクリプションID</param>
        /// <param name="name">サブスクリプション名</param>
        protected SubscriptionScriptBase(string id, string name)
        {
            Id = id;
            Name = name;
        }

        /// <summary>
        /// イベントを購読します。
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public IDisposable SubscribeFrom(SubscriptionArguments args)
        {
            CompositeDisposable subscriptions = new();
            RegistSubscription(args, subscriptions);
            return subscriptions;
        }

        /// <summary>
        /// イベントを購読します。
        /// </summary>
        /// <param name="args">サブスクリプション引数。ここにBot等からの通知オブジェクトがあります。</param>
        /// <param name="subscriptions">停止要求された時に購読を解除するオブジェクトを格納するコレクション。</param>
        protected abstract void RegistSubscription(SubscriptionArguments args, CompositeDisposable subscriptions);
    }
}
