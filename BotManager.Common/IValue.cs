using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Common
{
    /// <summary>
    /// 値インターフェース
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IValue<out T>
    {
        /// <summary>
        /// 値を取得します。
        /// </summary>
        T Value { get; }
    }
}
