using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Common
{
    /// <summary>
    /// Botマネージャーインターフェース
    /// </summary>
    public interface IBotManager : IDisposable, IStartEnd
    {
        /// <summary>
        /// Bot
        /// </summary>
        IEnumerable<IBot> Bots { get; }

        /// <summary>
        /// 指定したIDの<see cref="IBot"/>があればそれを返します。
        /// </summary>
        /// <param name="id">BotID</param>
        /// <param name="outbot">Bot</param>
        /// <returns>見つかった場合は true、それ以外は false</returns>
        bool TryGetBot(string id, [NotNullWhen(true)] out IBot? outbot);

        /// <summary>
        /// 指定したIDの<see cref="IBot"/>が存在し、かつ型と条件も一致するBotがあればそれを返します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">Bot ID</param>
        /// <param name="outbot">見つかった場合はBotオブジェクト、見つからなければ null</param>
        /// <param name="predicate">条件式</param>
        /// <returns>見つかった場合は true、それ以外は false</returns>
        public bool TryGetBot<T>(string id, [NotNullWhen(true)] out T? outbot, Func<T, bool> predicate) where T : IBot
        {
            if(TryGetBot(id, out IBot? bot) && (bot is T t && predicate(t)))
            {
                outbot = t;
                return true;
            }
            else
            {
                outbot = default;
                return false;
            }
        }

    }
}
