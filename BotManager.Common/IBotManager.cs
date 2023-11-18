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
        /// <param name="bot">Bot</param>
        /// <returns></returns>
        bool TryGetBot(string id, [NotNullWhen(true)] out IBot? bot);

    }
}
