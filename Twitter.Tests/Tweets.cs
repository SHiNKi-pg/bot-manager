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
        private readonly ITwitterServiceClient client;
        private readonly ITestOutputHelper output;

        public Tweets(ITestOutputHelper output)
        {
            var certification = AppSettings.GetBotDictionary()["test"].TwitterSetting?.Certificate!;
            this.output = output;
            client = TwitterService.Create(certification.ConsumerKey, certification.ConsumerSecret, certification.AccessToken, certification.AccessTokenSecret);
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

        [Fact(DisplayName = "認証＆ツイート＆削除テスト")]
        public async Task PostAndDeleteTweet()
        {
            // 認証
            await client.StartAsync();

            // ツイート（テキスト）
            var tweet = await client.Tweet("APIからツイートテスト");
            if(tweet is null)
            {
                Assert.Fail("Tweetオブジェクトが null");
                return;
            }
            output.WriteLine("ツイートID : {0}", tweet.ID);

            if(tweet.ID is null)
            {
                Assert.Fail("Tweet.IDが null");
                return;
            }

            // 3秒待機
            await Task.Delay(3000);

            // 先ほどのツイートを削除
            var response = await client.DeleteTweet(tweet.ID);

            // 停止
            await client.EndAsync();
        }

        public void Dispose()
        {
            client.Dispose();
        }
    }
}
