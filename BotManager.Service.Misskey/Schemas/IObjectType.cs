using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Misskey.Schemas
{
    /// <summary>
    /// オブジェクトタイプインターフェース
    /// </summary>
    public interface IObjectType
    {
        /// <summary>
        /// タイプ
        /// </summary>
        string Type { get; }
    }
}
