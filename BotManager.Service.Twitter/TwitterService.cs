using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Twitter
{
    /// <summary>
    /// Twitterサービス
    /// </summary>
    public static class TwitterService
    {
        /// <summary>
        /// Twitterクライアントを返します。
        /// </summary>
        /// <param name="consumerKey">APIキー</param>
        /// <param name="consumerSecret">APIキーシークレット</param>
        /// <param name="accessToken">アクセストークン</param>
        /// <param name="accessTokenSecret">アクセストークンシークレット</param>
        /// <returns></returns>
        public static ITwitterServiceClient Create(string consumerKey, string consumerSecret, string accessToken, string accessTokenSecret)
        {
            return new TwitterServiceClient(consumerKey, consumerSecret, accessToken, accessTokenSecret);
        }
    }
}
