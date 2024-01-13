using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Compiler
{
    /// <summary>
    /// ソースコードやインポートする型をコンパイルに通知するインターフェース
    /// </summary>
    public interface ISourceIncluding
    {
        /// <summary>
        /// ソースコードをクリアします。
        /// </summary>
        void ClearSources();

        /// <summary>
        /// 指定した型をインポートします。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        void Import<T>();

        /// <summary>
        /// 指定したファイルから型をインポートします。
        /// </summary>
        /// <param name="filePath"></param>
        void Import(string filePath);

        /// <summary>
        /// 指定した型をインポートします。
        /// </summary>
        /// <param name="type"></param>
        void Import(Type type);

        /// <summary>
        /// インポート情報をクリアします。
        /// </summary>
        void ClearImports();

        /// <summary>
        /// ソースコードを追加します。
        /// </summary>
        /// <param name="source"></param>
        void AddSource(string source);

        /// <summary>
        /// ソースファイルを追加します。
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="encoding"></param>
        /// <param name="usingBOM"></param>
        /// <returns></returns>
        Task AddSourceFile(string filePath, Encoding encoding, bool usingBOM);
    }
}
