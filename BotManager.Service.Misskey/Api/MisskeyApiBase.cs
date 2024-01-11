using BotManager.Common.Web;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Misskey.Api
{
    /// <summary>
    /// MisskeyAPIベースクラス（エンドポイント別）
    /// </summary>
    internal abstract class MisskeyApiBase
    {
        #region Private Fields
        /// <summary>
        /// MisskeyAPI
        /// </summary>
        protected MisskeyApi misskeyApi { get; private set; }

        private JsonSerializerSettings JsonSerializerSettings;
        #endregion

        #region Constractor
        internal MisskeyApiBase(MisskeyApi misskeyApi)
        {
            this.misskeyApi = misskeyApi;
            this.JsonSerializerSettings = new();
            JsonSerializerSettings.NullValueHandling = NullValueHandling.Ignore;
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
        internal protected async Task<T> PostAsync<T>(string endPoint, object requestBody)
        {
            string url = misskeyApi.BaseUrl + endPoint;

            string json = JsonConvert.SerializeObject(requestBody, JsonSerializerSettings);
            using (var content = new StringContent(json, Encoding.UTF8, "application/json"))
            {
                var response = await HttpClientSingleton.HttpClient.PostAsync(url, content);
                if (!response.IsSuccessStatusCode)
                {
                    // エラー系のステータスコードの場合は例外を投げる
                    throw new HttpRequestException($"[{url}] Status Code : {(int)response.StatusCode}", null, statusCode: response.StatusCode);
                }
                string responseString = await response.Content.ReadAsStringAsync();

                var jsonObj = JsonConvert.DeserializeObject<T>(responseString)!;
                return jsonObj;
            }
        }
        #endregion
    }
}
