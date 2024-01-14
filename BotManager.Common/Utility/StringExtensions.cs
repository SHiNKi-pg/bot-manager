using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BotManager.Common.Utility
{
    /// <summary>
    /// <see cref="string"/>拡張メソッド定義
    /// </summary>
    public static class StringExtensions
    {
        #region Like
        /// <summary>
        /// この文字列が指定した正規表現パターンにマッチするかどうか返します。
        /// </summary>
        /// <param name="text"></param>
        /// <param name="pattern">正規表現パターン</param>
        /// <returns>マッチする場合 true、それ以外は false。</returns>
        public static bool Like(this string text, [StringSyntax(StringSyntaxAttribute.Regex)] string pattern)
        {
            Regex regex = new Regex(pattern);
            Match match = regex.Match(text);
            return match.Success;
        }

        /// <summary>
        /// この文字列が指定した正規表現パターンにマッチするかどうか返します。
        /// </summary>
        /// <param name="text"></param>
        /// <param name="pattern">正規表現パターン</param>
        /// <param name="options">オプション</param>
        /// <returns>マッチする場合 true、それ以外は false。</returns>
        public static bool Like(this string text, [StringSyntax(StringSyntaxAttribute.Regex)] string pattern, RegexOptions options)
        {
            Regex regex = new Regex(pattern, options);
            Match match = regex.Match(text);
            return match.Success;
        }
        #endregion

        #region TryGetRegexGroup
        /// <summary>
        /// 指定した正規表現パターンにマッチした場合、その正規表現グループを返します。
        /// </summary>
        /// <param name="text"></param>
        /// <param name="pattern">正規表現パターン</param>
        /// <param name="groupCollection">正規表現のグループコレクション</param>
        /// <returns></returns>
        public static bool TryGetRegexGroup(this string text, [StringSyntax(StringSyntaxAttribute.Regex)] string pattern,
            [NotNullWhen(true)] out GroupCollection? groupCollection)
        {
            Regex regex = new(pattern);
            Match match = regex.Match(text);
            groupCollection = match.Groups;
            return match.Success;
        }

        /// <summary>
        /// 指定した正規表現パターンにマッチした場合、その正規表現グループを返します。
        /// </summary>
        /// <param name="text"></param>
        /// <param name="pattern">正規表現パターン</param>
        /// <param name="options">オプション</param>
        /// <param name="groupCollection">正規表現のグループコレクション</param>
        /// <returns></returns>
        public static bool TryGetRegexGroup(this string text, [StringSyntax(StringSyntaxAttribute.Regex)] string pattern, RegexOptions options, 
            [NotNullWhen(true)] out GroupCollection? groupCollection)
        {
            Regex regex = new(pattern, options);
            Match match = regex.Match(text);
            groupCollection = match.Groups;
            return match.Success;
        }
        #endregion
    }
}
