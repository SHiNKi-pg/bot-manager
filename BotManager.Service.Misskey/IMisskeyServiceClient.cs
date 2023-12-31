﻿using BotManager.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Misskey
{
    /// <summary>
    /// Misskey Bot用クライアントインターフェース
    /// </summary>
    public interface IMisskeyServiceClient : IServiceClient, IMisskeyEventNotifier
    {
        /// <summary>
        /// Misskey HTTP APIにアクセスするオブジェクトを返します。
        /// </summary>
        /// <returns></returns>
        IMisskeyApi GetMisskeyHttpApiClient();
    }
}
