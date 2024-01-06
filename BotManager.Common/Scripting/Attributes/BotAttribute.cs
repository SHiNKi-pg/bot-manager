using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Common.Scripting.Attributes
{
    /// <summary>
    /// このクラスがBot定義クラスであることを示します。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class BotAttribute : Attribute
    {
    }
}
