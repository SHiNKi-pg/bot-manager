using BotManager.Common.Messaging;
using LinqToTwitter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Twitter.Messaging
{
    internal class TwitterMessage : IDeletableMessage
    {
        private readonly Tweet? tweet;
        private readonly ITwitterServiceClient client;

        public TwitterMessage(ITwitterServiceClient client, Tweet? tweet)
        {
            this.tweet = tweet;
            this.client = client;
        }

        public string Content => tweet?.Text ?? string.Empty;

        public DateTime ReceivedTime => tweet?.CreatedAt ?? DateTime.MinValue;

        public bool IsBot => false;

        public string AuthorName => string.Empty;

        public async Task Delete()
        {
            if(tweet is not null && tweet.ID is not null)
                await client.DeleteTweet(tweet.ID);
        }
    }
}
