using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Common.Scripting.Attributes
{
    /// <summary>
    /// 任意のイベントを受け取った時に実行する処理を定義したクラスであることを示します。
    /// この属性を付与する場合はクラスに <seealso cref="ISubscription{Args}"/>インターフェースを実装しなければなりません。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ActionAttribute : Attribute
    {
    }
}
