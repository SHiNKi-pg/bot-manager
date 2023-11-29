using LinqToTwitter;
using LinqToTwitter.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Twitter
{
    /// <summary>
    /// Twitterクライアントクラス
    /// </summary>
    public class TwitterServiceClient : ITwitterServiceClient
    {
        private readonly IAuthorizer authorizer;
        private readonly TwitterContext twitterContext;

        #region Constructor
        /// <summary>
        /// <see cref="TwitterServiceClient"/>オブジェクトを作成します。
        /// </summary>
        /// <param name="authorizerGetter"></param>
        public TwitterServiceClient(Func<IAuthorizer> authorizerGetter)
        {
            this.authorizer = authorizerGetter();
            twitterContext = new(authorizer);
        }

        /// <summary>
        /// APIキーとアクセストークンを使用して<see cref="TwitterServiceClient"/>オブジェクトを作成します。
        /// </summary>
        /// <param name="consumerKey">APIキー</param>
        /// <param name="consumerSecret">APIキーシークレット</param>
        /// <param name="accessToken">アクセストークン</param>
        /// <param name="accessTokenSecret">アクセストークンシークレット</param>
        public TwitterServiceClient(string consumerKey, string consumerSecret, string accessToken, string accessTokenSecret) 
            : this(() => new SingleUserAuthorizer()
            {
                CredentialStore = new SingleUserInMemoryCredentialStore()
                {
                    ConsumerKey = consumerKey,
                    ConsumerSecret = consumerSecret,
                    AccessToken = accessToken,
                    AccessTokenSecret = accessTokenSecret,
                }
            })
        {

        }
        #endregion

        #region Method
        /// <summary>
        /// 認証を開始します。
        /// </summary>
        /// <returns></returns>
        public async Task StartAsync()
        {
            await authorizer.AuthorizeAsync();
        }

        /// <summary>
        /// このメソッドを呼び出しても何も起こりません。
        /// </summary>
        /// <returns></returns>
        public Task EndAsync()
        {
            return Task.CompletedTask;
        }
        #endregion

        #region Disposal
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            twitterContext.Dispose();
        }
        #endregion
    }
}
