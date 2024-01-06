using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Git
{
    /// <summary>
    /// Gitリポジトリインターフェース
    /// </summary>
    public interface IGitRepositry : IDisposable
    {
        /// <summary>
        /// ローカルリポジトリのディレクトリを取得します。
        /// </summary>
        IDirectory LocalDirectory { get; }
    }
    internal sealed class GitRepository : IGitRepositry
    {
        #region Private Fields
        private Repository repository;
        private IDirectory localDirectory;
        #endregion

        #region Constructor
        public GitRepository(string repositryPath, string directory)
        {
            repository = new Repository(repositryPath);
            this.localDirectory = new GitDirectory(directory);
        }
        #endregion

        #region Proparty
        public IDirectory LocalDirectory => this.localDirectory;
        #endregion

        #region Method
        public void Dispose()
        {
            repository.Dispose();
        }
        #endregion
    }
}
