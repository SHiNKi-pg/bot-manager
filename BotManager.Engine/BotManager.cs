using BotManager.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Engine
{
    /// <summary>
    /// Botマネージャ
    /// </summary>
    internal class BotManager : IBotManager
    {
        #region Private Field
        private readonly Dictionary<string, IBot> _bots;

        private readonly static ILog Logger = Log.GetLogger("engine");
        #endregion

        #region Constructor
        public BotManager()
        {
            _bots = new();
            Logger.Debug("BotManager Created");
        }
        #endregion

        public IEnumerable<IBot> Bots => _bots.Select(kv => kv.Value);

        public async Task StartAsync()
        {
            Logger.Info("BotManager StartAsync Start");
            try
            {
                List<Task> tasklist = new();
                foreach (var bot in _bots)
                {
                    tasklist.Add(bot.Value.StartAsync());
                    Logger.Trace($"Bot {bot.Value.Id} Starting");
                }
                await Task.WhenAll(tasklist);
            }
            finally
            {
                Logger.Info("BotManager StartAsync End");
            }
        }

        public async Task EndAsync()
        {
            Logger.Info("BotManager EndAsync Start");
            try
            {
                List<Task> tasklist = new();
                foreach (var bot in _bots)
                {
                    tasklist.Add(bot.Value.EndAsync());
                    Logger.Trace($"Bot {bot.Value.Id} Ending");
                }
                await Task.WhenAll(tasklist);
            }
            finally
            {
                Logger.Info("BotManager EndAsync End");
            }
        }

        /// <summary>
        /// Botを追加します。
        /// </summary>
        /// <param name="bot"></param>
        public void AddBot(IBot bot)
        {
            _bots.Add(bot.Id, bot);
        }

        public bool TryGetBot(string id, [NotNullWhen(true)] out IBot? outbot)
        {
            if (_bots.TryGetValue(id, out IBot? _bot))
            {
                outbot = _bot;
                return true;
            }
            else
            {
                outbot = null;
                return false;
            }
        }

        public void Dispose()
        {
            Logger.Debug("BotManager Disposing");
            foreach (var kv in _bots)
            {
                kv.Value.Dispose();
            }
            _bots.Clear();
            Logger.Debug("BotManager Disposed");
        }
    }
}
