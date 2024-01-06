using BotManager.Service.Misskey.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Misskey
{
    /// <summary>
    /// Misskey APIインターフェース
    /// </summary>
    public interface IMisskeyApi
    {
        /// <summary>
        /// ノートAPI
        /// </summary>
        INotes Notes { get; }
    }
}
