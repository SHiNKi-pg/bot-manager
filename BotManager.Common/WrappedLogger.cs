using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Common
{
    /// <summary>
    /// ログインターフェース
    /// </summary>
    public interface ILog
    {
        /// <summary>
        /// TRACEレベルのログを出力します。
        /// </summary>
        /// <param name="message">ログメッセージ</param>
        void Trace(string message);

        /// <summary>
        /// DEBUGレベルのログを出力します。
        /// </summary>
        /// <param name="message">ログメッセージ</param>
        void Debug(string message);

        /// <summary>
        /// INFOレベル（情報）のログを出力します。
        /// </summary>
        /// <param name="message">ログメッセージ</param>
        void Info(string message);

        /// <summary>
        /// WARNレベル（警告）のログを出力します。
        /// </summary>
        /// <param name="message">ログメッセージ</param>
        void Warn(string message);

        /// <summary>
        /// ERRORレベル（エラー）のログを出力します。
        /// </summary>
        /// <param name="message">ログメッセージ</param>
        void Error(string message);

        /// <summary>
        /// ERRORレベル（エラー）のログを出力します。
        /// </summary>
        /// <param name="exception">エラー</param>
        /// <param name="message">ログメッセージ</param>
        void Error(Exception exception, string message);

        /// <summary>
        /// FATALレベル（重大エラー）のログを出力します。
        /// </summary>
        /// <param name="message">ログメッセージ</param>
        void Fatal(string message);

        /// <summary>
        /// FATALレベル（重大エラー）のログを出力します。
        /// </summary>
        /// <param name="exception">エラー</param>
        /// <param name="message">ログメッセージ</param>
        void Fatal(Exception exception, string message);
    }
    internal class WrappedLogger : ILog
    {
        private readonly ILogger logger;

        public WrappedLogger(string loggerName)
        {
            logger = NLog.LogManager.GetLogger(loggerName);
        }

        public void Debug(string message)
        {
            logger.Debug(message);
        }

        public void Error(string message)
        {
            logger.Error(message);
        }

        public void Error(Exception exception, string message)
        {
            logger.Error(exception, message);
        }

        public void Fatal(string message)
        {
            logger.Fatal(message);
        }

        public void Fatal(Exception exception, string message)
        {
            logger.Fatal(exception, message);
        }

        public void Info(string message)
        {
            logger.Info(message);
        }

        public void Trace(string message)
        {
            logger.Trace(message);
        }

        public void Warn(string message)
        {
            logger.Warn(message);
        }
    }
}
