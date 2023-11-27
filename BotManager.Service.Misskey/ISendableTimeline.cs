using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Misskey
{
    /// <summary>
    /// 送信可能タイムライン
    /// </summary>
    public interface ISendableTimeline
    {
        /// <summary>
        /// データを送信します
        /// </summary>
        /// <typeparam name="TBody"></typeparam>
        /// <param name="body"></param>
        void Send<TBody>(TBody body);
    }

    /// <summary>
    /// 汎用タイムラインインターフェース
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGenericTimeline<out T> : ISendableTimeline, IObservable<T>
    {

    }
}
