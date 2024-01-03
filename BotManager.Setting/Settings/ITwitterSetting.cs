using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Setting.Settings
{
    /// <summary>
    /// TwitterBot設定スキーマ
    /// </summary>
    public interface ITwitterSetting
    {
        /// <summary>
        /// 認証
        /// </summary>
        ITwitterCertification Certificate { get; }
    }

    /// <summary>
    /// TwitterBot認証スキーマ
    /// </summary>
    public interface ITwitterCertification
    {
        /// <summary>
        /// APIキー
        /// </summary>
        public string ConsumerKey { get; }

        /// <summary>
        /// APIキーシークレット
        /// </summary>
        public string ConsumerSecret { get; }

        /// <summary>
        /// アクセストークン
        /// </summary>
        public string AccessToken { get; }

        /// <summary>
        /// アクセストークンシークレット
        /// </summary>
        public string AccessTokenSecret { get; }
    }

#pragma warning disable 8618
    internal class TwitterSetting : ITwitterSetting
    {
        public TwitterCertification Certification { get; set; }

        public ITwitterCertification Certificate { get => Certification; }
    }

    internal class TwitterCertification : ITwitterCertification
    {
        public string ConsumerKey { get; set; }

        public string ConsumerSecret { get; set; }

        public string AccessToken { get; set; }

        public string AccessTokenSecret { get; set; }
    }
#pragma warning restore
}
