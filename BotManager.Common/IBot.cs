using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Common
{
    /// <summary>
    /// Botインターフェース
    /// </summary>
    public interface IBot : INamed, IStartEnd, IDisposable
    {
        /// <summary>
        /// このBotの名前を表す正規表現パターン
        /// </summary>
        [StringSyntax(StringSyntaxAttribute.Regex)]
        string NamePattern { get; }
    }
}
