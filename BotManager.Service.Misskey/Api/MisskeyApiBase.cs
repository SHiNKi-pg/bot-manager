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
    public abstract class MisskeyApiBase
    {
        #region Private Fields
        private MisskeyApi misskeyApi;
        #endregion

        #region Constractor
        internal MisskeyApiBase(MisskeyApi misskeyApi)
        {
            this.misskeyApi = misskeyApi;
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
            string url = misskeyApi.BaseUrl + endPoint;

            // 認証情報の付与
            requestBody.Add("i", misskeyApi.AccessToken);
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
