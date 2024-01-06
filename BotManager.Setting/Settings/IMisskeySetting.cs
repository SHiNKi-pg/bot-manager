using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Setting.Settings
{
    /// <summary>
    /// Misskey Bot設定スキーマ
    /// </summary>
    public interface IMisskeySetting
    {
        /// <summary>
        /// 認証
        /// </summary>
        IMisskeyCertification Certificate { get; }
    }

    /// <summary>
    /// Misskey Bot認証スキーマ
    /// </summary>
    public interface IMisskeyCertification
    {
        /// <summary>
        /// 接続先ホスト名
        /// </summary>
        string Host { get; }

        /// <summary>
        /// アクセストークン
        /// </summary>
        string AccessToken { get; }
    }

#pragma warning disable 8618
    internal class MisskeySetting : IMisskeySetting
    {
        public MisskeyCertification Certification { get; set; }

        public IMisskeyCertification Certificate { get => Certification; }
    }

    internal class MisskeyCertification : IMisskeyCertification
    {
        public string Host { get; set; }

        public string AccessToken { get; set; }
    }
#pragma warning restore
}
