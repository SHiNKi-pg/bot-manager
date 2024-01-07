using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Common
{
    /// <summary>
    /// ログクラス
    /// </summary>
    public static class Log
    {
        /// <summary>
        /// ログ出力オブジェクトを返します。
        /// </summary>
        /// <param name="loggerName"></param>
        /// <returns></returns>
        public static ILog GetLogger(string loggerName)
        {
            return new WrappedLogger(loggerName);
        }
    }
}
