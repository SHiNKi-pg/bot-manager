using BotManager.Common.Web;
using BotManager.Service.Misskey.Api;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Misskey
{
    /// <summary>
    /// Misskey Web API
    /// </summary>
    public partial class MisskeyApi : IMisskeyApi
    {
        #region Private Fields
        /// <summary>
        /// アクセストークン
        /// </summary>
        private string accessToken;

        /// <summary>
        /// ベースURL
        /// </summary>
        private string baseUrl;
        #endregion

        #region Constructor
        /// <summary>
        /// <see cref="MisskeyApi"/>オブジェクトを作成します。
        /// </summary>
        /// <param name="accessToken">アクセストークン</param>
        /// <param name="hostName">接続先ホスト名</param>
        public MisskeyApi(string hostName, string accessToken)
        {
            this.accessToken = accessToken;
            this.baseUrl = $"https://{hostName}/api/";

            #region Property Initialize
            this.Notes = new Notes(this);
            #endregion
        }
        #endregion

        #region Property
        /// <summary>
        /// ベースURL
        /// </summary>
        public string BaseUrl { get => baseUrl; }

        /// <summary>
        /// アクセストークン
        /// </summary>
        internal string AccessToken { get => accessToken; }
        #endregion

        #region EndPoint
        /// <summary>
        /// ノート
        /// </summary>
        public INotes Notes { get; init; }
        #endregion
    }
}
