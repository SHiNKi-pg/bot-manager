using BotManager.Common.Web;
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
    public partial class MisskeyApi
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
        }
        #endregion

        #region Base Method
        /// <summary>
        /// POSTリクエストを送信します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="endPoint">エンドポイント名</param>
        /// <param name="requestBody">リクエストボディ</param>
        /// <returns></returns>
        /// <exception cref="HttpRequestException"></exception>
        internal protected async Task<T?> PostAsync<T>(string endPoint, Dictionary<string, object?> requestBody)
        {
            string url = baseUrl + endPoint;

            // 認証情報の付与
            requestBody.Add("i", accessToken);
            requestBody.Add("detail", false);

            string json = JsonConvert.SerializeObject(requestBody);
            using (var content = new StringContent(json, Encoding.UTF8, "application/json"))
            {
                var response = await HttpClientSingleton.HttpClient.PostAsync(url, content);
                if (!response.IsSuccessStatusCode)
                {
                    // エラー系のステータスコードの場合は例外を投げる
                    throw new HttpRequestException($"[{url}] Status Code : {(int)response.StatusCode}", null, statusCode: response.StatusCode);
                }
                string responseString = await response.Content.ReadAsStringAsync();

                var jsonObj = JsonConvert.DeserializeObject<T>(responseString);
                return jsonObj;
            }
        }
        #endregion
    }
}
