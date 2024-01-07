using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Common
{
    /// <summary>
    /// <see cref="string"/>型のIDと名称を取得できるインターフェース
    /// </summary>
    public interface INamed
    {
        /// <summary>
        /// 一意に識別する文字列
        /// </summary>
        string Id { get; }

        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; }
    }
}
