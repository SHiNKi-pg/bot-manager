using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Setting.Settings
{
    /// <summary>
    /// スクリプトスキーマインターフェース
    /// </summary>
    public interface IScript
    {
        /// <summary>
        /// リポジトリのクローンのあるディレクトリのパス
        /// </summary>
        string Path { get; }

        /// <summary>
        /// スクリプトのリポジトリURL
        /// </summary>
        string RepositoryUrl { get; }

        /// <summary>
        /// Gitユーザー名
        /// </summary>
        string UserName { get; }

        /// <summary>
        /// Gitユーザーのメールアドレス
        /// </summary>
        string Email { get; }
    }

#pragma warning disable 8618
    internal class Script : IScript
    {
        public string Path { get; set; }

        public string RepositoryUrl { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }
    }
#pragma warning restore
}
