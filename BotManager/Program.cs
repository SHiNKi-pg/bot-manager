using BotManager.Common;
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
            var sysLog = Log.GetLogger("main");
            sysLog.Info("BotManager Start");
            try
            {
                using (Clock clock = new Clock())
                using (var eewMonitor = EEWNotifier.Create(clock))
                using (var botm = Core.Create<SubscriptionArguments>(new MyCompiler("botmanage.dll"),
                    (named, bm) => new()
                    {
                        BotManager = bm,
                        Clock = clock,
                        EEWMonitor = eewMonitor,
                        Logger = Log.GetLogger("SBSC_" + named.Id)
                    }))
                {
                    await botm.Start();

                    // TODO: 待機処理を作成する
                    Console.ReadLine();
                }

                sysLog.Info("BotManager End");
            }catch(Exception ex)
            {
                sysLog.Fatal(ex.Message);
                sysLog.Fatal("BotManager Abort");
                throw;
            }
        }
    }
}