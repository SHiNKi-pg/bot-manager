using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Misskey
{
    /// <summary>
    /// Misskeyサービス
    /// </summary>
    public static class MisskeyService
    {
        /// <summary>
        /// Misskeyクライアントを返します。
        /// </summary>
        /// <param name="host">接続先ホスト名</param>
        /// <param name="accessToken">アクセストークン</param>
        /// <returns></returns>
        public static IMisskeyServiceClient Create(string host, string accessToken)
        {
            return new MisskeyClient(host, accessToken);
        }
    }
}
