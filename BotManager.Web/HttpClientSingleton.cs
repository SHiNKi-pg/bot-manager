using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Common.Web
{
    /// <summary>
    /// <see cref="HttpClient"/>シングルトンクラス
    /// </summary>
    public static class HttpClientSingleton
    {
        /// <summary>
        /// HTTPクライアント
        /// </summary>
        public static HttpClient HttpClient { get; }

        static HttpClientSingleton()
        {
            // .NET Core 2.1以降はこの書き方が推奨されている
            // HttpClientは1つのインスタンスを使い回さないとソケットが枯渇することがある。
            HttpClient = new ServiceCollection()
                .AddHttpClient()
                .BuildServiceProvider()
                .GetService<IHttpClientFactory>()!
                .CreateClient();
        }

        /// <summary>
        /// HttpClientを閉じます。終了時に呼び出してください。
        /// </summary>
        public static void Close()
        {
            HttpClient.Dispose();
        }
    }
}
