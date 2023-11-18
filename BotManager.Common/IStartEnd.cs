using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Common
{
    /// <summary>
    /// Start-Endインターフェース
    /// </summary>
    public interface IStartEnd
    {
        /// <summary>
        /// 開始します。
        /// </summary>
        /// <returns></returns>
        Task StartAsync();

        /// <summary>
        /// 終了します。
        /// </summary>
        /// <returns></returns>
        Task EndAsync();
    }
}
