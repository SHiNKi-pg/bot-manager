using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Engine.Settings
{
    /// <summary>
    /// データベース設定インターフェース
    /// </summary>
    public interface IDatabase
    {
        /// <summary>
        /// 接続文字列
        /// </summary>
        string ConnectionString { get; }
    }

#pragma warning disable 8618
    internal class Database : IDatabase
    {
        public string ConnectionString { get; set; }
    }
#pragma warning restore
}
