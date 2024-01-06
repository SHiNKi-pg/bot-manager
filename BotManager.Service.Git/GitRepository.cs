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

    }
    internal sealed class GitRepository : IGitRepositry
    {
        #region Private Fields
        private Repository repository;
        #endregion

        #region Constructor
        public GitRepository(string repositryPath)
        {
            repository = new Repository(repositryPath);
        }
        #endregion

        #region Method
        public void Dispose()
        {
            repository.Dispose();
        }
        #endregion
    }
}
