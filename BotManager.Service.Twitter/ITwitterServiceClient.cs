using BotManager.Common;
using LinqToTwitter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Twitter
{
    /// <summary>
    /// Twitter Bot用クライアントインターフェース
    /// </summary>
    public interface ITwitterServiceClient : IServiceClient
    {
        /// <summary>
        /// 指定したテキストをツイートします。
        /// </summary>
        /// <param name="text">ツイート本文</param>
        /// <returns></returns>
        Task<Tweet?> Tweet(string text);

        /// <summary>
        /// 指定したテキストと添付ファイルをツイートします。
        /// </summary>
        /// <param name="text">ツイート本文</param>
        /// <param name="mediaBytes">メディアデータ</param>
        /// <param name="mediaType">メディアタイプ</param>
        /// <param name="mediaCategory">メディアカテゴリ</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        Task<Tweet?> Tweet(string text, byte[] mediaBytes, string mediaType, string mediaCategory);

        /// <summary>
        /// 指定したIDのツイートを削除します。
        /// </summary>
        /// <param name="tweetId">ツイートID</param>
        /// <returns></returns>
        Task<TweetDeletedResponse?> DeleteTweet(string tweetId);
    }
}
