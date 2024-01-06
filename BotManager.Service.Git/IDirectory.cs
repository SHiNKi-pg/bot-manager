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
    }
}
