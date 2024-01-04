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

        public void Dispose()
        {
            client.Dispose();
        }
    }
}
