using BotManager;
using BotManager.Engine;
using BotManager.External;
using BotManager.Notifiers.EarthquakeMonitor;
using BotManager.Notifiers;
using BotManager.Common;

namespace BotManagerWebAPI
{
    internal static class Entry
    {
        public static IBotMechanism<SubscriptionArguments> BotMechanism { get; }
        private static Clock clock;
        private static IEEWMonitor eewMonitor;
        public static ILog Logger { get; }

        static Entry()
        {
            Logger = Log.GetLogger("system");
            clock = new Clock();
            eewMonitor = EEWNotifier.Create(clock);
            BotMechanism = Core.Create<SubscriptionArguments>(new BotManagerCompiler("botmanage.dll"),
            (named, bm) => new()
            {
                BotManager = bm,
                Clock = clock,
                EEWMonitor = eewMonitor,
                Logger = Log.GetLogger("SBSC_" + named.Id)
            });
        }

        public static void Close()
        {
            BotMechanism.Dispose();
            eewMonitor.Dispose();
            clock.Dispose();
        }
    }
}
