﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Common.Extensions
{
    /// <summary>
    /// <seealso cref="IBotManager"/>拡張メソッド定義
    /// </summary>
    public static class BotManagerExtensions
    {
        #region Race
        /// <summary>
        /// この Botコレクションのうち、指定したイベントが一番早く来た Botの通知が後続に流れるようにします。
        /// </summary>
        /// <typeparam name="TBot"></typeparam>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="bots"></param>
        /// <param name="observableSelector">イベント通知</param>
        /// <returns></returns>
        public static IObservable<IValueSender<TBot, TSource>> Race<TBot, TSource>(this IEnumerable<TBot> bots, Func<TBot, IObservable<TSource>> observableSelector) where TBot : IBot
        {
            var observables = bots.Select(b => observableSelector(b).Select(t => new _Sender<TBot, TSource>(b, t)));
            return Observable.Amb(observables);
        }

        private class _Sender<TSender, TValue> : IValueSender<TSender, TValue>
        {
            public TSender Sender { get; }
            public TValue Value { get; }

            public _Sender(TSender sender, TValue value)
            {
                this.Sender = sender;
                this.Value = value;
            }
        }

        #endregion

        #region Merge
        /// <summary>
        /// この Botの指定したイベントの通知が全て1つのストリームに統合されて後続に流れるようにします。
        /// </summary>
        /// <typeparam name="TBot"></typeparam>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="bots"></param>
        /// <param name="observableSelector">イベント通知</param>
        /// <returns></returns>
        public static IObservable<IValueSender<TBot, TSource>> Merge<TBot, TSource>(this IEnumerable<TBot> bots, Func<TBot, IObservable<TSource>> observableSelector) where TBot : IBot
        {
            var observables = bots.Select(b => observableSelector(b).Select(t => new _Sender<TBot, TSource>(b, t)));
            return Observable.Merge(observables);
        }
        #endregion

        #region Which
        /// <summary>
        /// 管理中のBotのうち、指定した型のBotを列挙します。
        /// </summary>
        /// <typeparam name="TBot"></typeparam>
        /// <param name="botManager"></param>
        /// <returns></returns>
        public static IEnumerable<TBot> Which<TBot>(this IBotManager botManager) where TBot : IBot
        {
            return botManager.Bots.OfType<TBot>();
        }
        #endregion

        #region WhichWhere
        /// <summary>
        /// 管理中のBotのうち、指定した型であり、指定した条件を満たすBotを列挙します。
        /// </summary>
        /// <typeparam name="TBot"></typeparam>
        /// <param name="botManager"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IEnumerable<TBot> WhichWhere<TBot>(this IBotManager botManager, Func<TBot, bool> predicate) where TBot : IBot
        {
            return botManager.Which<TBot>().Where(predicate);
        }
        #endregion
    }
}
