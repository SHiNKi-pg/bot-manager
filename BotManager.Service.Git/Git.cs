using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Git
{
    /// <summary>
    /// Gitコマンドクラス
    /// </summary>
    public static class Git
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repositry">リポジトリのURLまたはパス</param>
        /// <param name="path">クローン先のディレクトリ</param>
        /// <returns></returns>
        public static IGitRepositry Clone(string repositry, string path)
        {
            return new GitRepository(Repository.Clone(repositry, path), path);
        }
    }
}
