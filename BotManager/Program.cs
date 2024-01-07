using BotManager.Engine;
using BotManager.External;
using BotManager.Notifiers;
using BotManager.Notifiers.EarthquakeMonitor;

namespace BotManager
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using(Clock clock = new Clock())
            using(IEEWMonitor eewMonitor = new EEWMonitor(clock))
            using(var botm = Core.Create<SubscriptionArguments>("botmanage.dll",
                bm => new()
                {
                    BotManager = bm,
                    Clock = clock,
                    EEWMonitor = eewMonitor
                }))
            {
                await botm.Start();

                // TODO: 待機処理を作成する
                Console.ReadLine();
            }
        }
    }
}