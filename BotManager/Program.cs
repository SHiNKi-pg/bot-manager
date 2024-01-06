using BotManager.Engine;

namespace BotManager
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using(var botm = Core.Create("botmanage.dll"))
            {
                await botm.Start();

                // TODO: 待機処理を作成する
                Console.ReadLine();
            }
        }
    }
}