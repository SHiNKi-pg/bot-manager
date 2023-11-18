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
    public interface IBot : IStartEnd, IDisposable
    {
        /// <summary>
        /// 一意にBotを識別する文字列
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Botの名前
        /// </summary>
        string Name { get; }

        /// <summary>
        /// このBotの名前を表す正規表現パターン
        /// </summary>
        [StringSyntax(StringSyntaxAttribute.Regex)]
        string NamePattern { get; }
    }
}
