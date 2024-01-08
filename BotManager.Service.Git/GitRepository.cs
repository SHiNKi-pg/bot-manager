using BotManager.Common;
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

        /// <summary>
        /// このリポジトリで git pullを実行します。
        /// </summary>
        /// <param name="userName">ユーザー名</param>
        /// <param name="emailAddress">ユーザーのメールアドレス</param>
        MergeStatus Pull(string userName, string emailAddress);

        /// <summary>
        /// 指定したブランチに切り替えます。
        /// </summary>
        /// <param name="branchName">ブランチ名</param>
        void Checkout(string branchName);
    }
    internal sealed class GitRepository : IGitRepositry
    {
        #region Private Fields
        private Repository repository;
        private IDirectory localDirectory;

        private readonly static ILog Logger = Log.GetLogger("git");
        #endregion

        #region Constructor
        public GitRepository(string repositryPath, string directory) : this(new Repository(repositryPath), directory)
        {
        }

        public GitRepository(Repository repository, string directory)
        {
            this.repository = repository;
            this.localDirectory = new GitDirectory(directory);
        }
        #endregion

        #region Proparty
        public IDirectory LocalDirectory => this.localDirectory;
        #endregion

        #region Method
        public void Dispose()
        {
            Logger.Trace($"{this.GetType().Name} Disposing");
            repository.Dispose();
            Logger.Trace($"{this.GetType().Name} Disposed");
        }

        public MergeStatus Pull(string userName, string emailAddress)
        {
            PullOptions options = new();
            Signature signature = new(userName, emailAddress, DateTimeOffset.Now);
            Logger.Info($"git pull (User = {userName}, Email = {emailAddress})");
            var mergeResult = Commands.Pull(repository, signature, options);
            Logger.Info($"git pull result : ${mergeResult.Status}");
            return mergeResult.Status;
        }

        public void Checkout(string branchName)
        {
            Logger.Info($"git checkout {branchName}");
            var branch = repository.Branches[branchName];
            if(branch is null)
            {
                Logger.Warn($"Branch '{branch}' is not found.");
                return;
            }
            Commands.Checkout(repository, branch);
            Logger.Info($"switch to branch {branchName}");
        }
        #endregion
    }
}
