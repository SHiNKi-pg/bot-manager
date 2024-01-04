using BotManager.Engine;
using BotManager.Service.Twitter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Twitter.Tests
{
    public class Tweets : IDisposable
    {
        private readonly TwitterServiceClient client;
        private readonly ITestOutputHelper output;

        public Tweets(ITestOutputHelper output)
        {
            var certification = AppSettings.GetBotDictionary()["test"].TwitterSetting?.Certificate!;
            this.output = output;
            client = new(certification.ConsumerKey, certification.ConsumerSecret, certification.AccessToken, certification.AccessTokenSecret);
        }

        [Fact(DisplayName = "Bot設定取得")]
        public void GetBotsTest()
        {
            var bots = AppSettings.Bots;
            foreach (var bot in bots)
            {
                output.WriteLine("BotId : {0}", bot.Id);
                var twitter = bot.TwitterSetting;
                if (twitter != null)
                {
                    output.WriteLine("ConsumerKey : {0}", twitter.Certificate.ConsumerKey);
                    output.WriteLine("ConsumerSecret : {0}", twitter.Certificate.ConsumerSecret);
                    output.WriteLine("AccessToken : {0}", twitter.Certificate.AccessToken);
                    output.WriteLine("AccessTokenSecret : {0}", twitter.Certificate.AccessTokenSecret);
                    output.WriteLine("");
                }
            }
        }

        public void Dispose()
        {
            client.Dispose();
        }
    }
}
