using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Git
{
    /// <summary>
    /// ディレクトリインターフェース
    /// </summary>
    public interface IDirectory
    {
        /// <summary>
        /// ディレクトリのパス
        /// </summary>
        string Path { get; }

        /// <summary>
        /// ディレクトリ名
        /// </summary>
        string Name { get; }

        /// <summary>
        /// このディレクトリを削除します。このディレクトリの中にあるディレクトリやファイルも全て削除されます。
        /// </summary>
        void Delete();
    }

    internal class GitDirectory : IDirectory
    {
        private DirectoryInfo _directory;

        public GitDirectory(string directoryPath)
        {
            this._directory = new DirectoryInfo(directoryPath);
        }

        public string Path => _directory.FullName;

        public string Name => _directory.Name;

        public void Delete()
        {
            this._directory.Delete(true);
        }
    }
}
