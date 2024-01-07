using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Common.Scripting.Attributes
{
    /// <summary>
    /// この属性が付与されていると、 <see cref="BotAttribute"/>属性や <see cref="ActionAttribute"/>属性が付与されていても無視されるようになります。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class IgnoreAttribute : Attribute
    {
    }
}
