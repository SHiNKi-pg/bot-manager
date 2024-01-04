using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Discord.Wrapper
{
    /// <summary>
    /// ラッパーインターフェース
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IWrapped<out T>
    {
        /// <summary>
        /// サードパーティ製オブジェクトを取得します。
        /// </summary>
        T Native { get; }
    }
}
