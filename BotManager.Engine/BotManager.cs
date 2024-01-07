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
        #endregion

        #region Constructor
        public BotManager()
        {
            _bots = new();
        }
        #endregion

        public IEnumerable<IBot> Bots => _bots.Select(kv => kv.Value);

        public async Task StartAsync()
        {
            List<Task> tasklist = new();
            foreach(var bot in _bots)
            {
                tasklist.Add(bot.Value.StartAsync());
            }
            await Task.WhenAll(tasklist);
        }

        public async Task EndAsync()
        {
            List<Task> tasklist = new();
            foreach (var bot in _bots)
            {
                tasklist.Add(bot.Value.EndAsync());
            }
            await Task.WhenAll(tasklist);
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
            foreach (var kv in _bots)
            {
                kv.Value.Dispose();
            }
            _bots.Clear();
        }
    }
}
