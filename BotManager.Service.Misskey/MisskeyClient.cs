using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Websocket.Client;

namespace BotManager.Service.Misskey
{
    /// <summary>
    /// Misskeyクライアントクラス
    /// </summary>
    internal class MisskeyClient : IMisskeyServiceClient
    {
        #region Private Field
        private WebsocketClient websocketClient;
        #endregion

        #region Constructor
        /// <summary>
        /// <see cref="MisskeyClient"/>オブジェクトを作成します。
        /// </summary>
        /// <param name="host">ホスト名</param>
        /// <param name="token">アクセストークン</param>
        public MisskeyClient(string host, string token)
        {
            websocketClient = new(new($"wss://{host}/streaming&i={token}"));
        }
        #endregion

        #region Method
        public Task StartAsync()
        {
            return Task.CompletedTask;
        }

        public Task EndAsync()
        {
            return Task.CompletedTask;
        }
        #endregion

        #region Disposal
        public void Dispose()
        {
            websocketClient.Dispose();
        }
        #endregion
    }
}
