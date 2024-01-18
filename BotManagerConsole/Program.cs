using BotManager.Engine;
using BotManager.External;
using BotManager.Notifiers.EarthquakeMonitor;
using BotManager.Notifiers;
using BotManager.Service.Compiler;
using BotManager.Common;
using BotManager;
using System.Reactive.Linq;

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

                    Console.WriteLine("Please Enter Command...");
                    Console.WriteLine("quit : Exit bot manager");
                    Console.WriteLine("reload : reload and compile script");
                    while (true)
                    {
                        string? command = Console.ReadLine();
                        if (string.IsNullOrEmpty(command))
                        {
                            Console.WriteLine("Please Enter Command...");
                            Console.WriteLine("quit : Exit bot manager");
                            Console.WriteLine("reload : reload and compile script");
                            continue;
                        }

                        if(command == "quit")
                        {
                            break;
                        }
                        else if(command == "reload")
                        {
                            Console.WriteLine("Please Wait...");
                            await botm.CompileSources();
                            Console.WriteLine("Script Reloaded");
                        }
                        else
                        {
                            Console.WriteLine("Command not found...");
                            Console.WriteLine("quit : Exit bot manager");
                            Console.WriteLine("reload : reload and compile script");
                        }
                    }
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
