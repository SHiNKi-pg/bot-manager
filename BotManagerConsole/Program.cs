using BotManager.Engine;
using BotManager.External;
using BotManager.Notifiers.EarthquakeMonitor;
using BotManager.Notifiers;
using BotManager.Service.Compiler;
using BotManager.Common;
using BotManager;

namespace BotManagerConsole
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
                using (var botm = Core.Create<SubscriptionArguments>(new BotManagerCompiler("botmanage.dll"),
                    (named, bm, cToken) => new()
                    {
                        BotManager = bm,
                        Clock = clock,
                        EEWMonitor = eewMonitor,
                        Logger = Log.GetLogger("SBSC_" + named.Id),
                        CancellationToken = cToken,
                    }))
                {
                    await botm.CompileSources();

                    // TODO: 待機処理を作成する
                    Console.ReadLine();
                }

                sysLog.Info("BotManager End");
            }
            catch (Exception ex)
            {
                sysLog.Fatal(ex, "BotManager Abort");
                throw;
            }
        }
    }
}
