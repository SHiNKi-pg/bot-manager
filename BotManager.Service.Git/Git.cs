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
    /// Gitコマンドクラス
    /// </summary>
    public static class Git
    {
        private readonly static ILog Logger = Log.GetLogger("git");

        /// <summary>
        /// 指定したリポジトリを指定ディレクトリにクローンします。
        /// </summary>
        /// <param name="repositryPath">リポジトリのURLまたはパス</param>
        /// <param name="path">クローン先のディレクトリ</param>
        /// <returns></returns>
        public static IGitRepositry Clone(string repositryPath, string path)
        {
            try
            {
                Logger.Info($"git clone {repositryPath} -> {path}");
                return new GitRepository(Repository.Clone(repositryPath, path), path);
            }catch(Exception ex)
            {
                Logger.Error(ex, "Clone");
                throw;
            }
        }

        /// <summary>
        /// 指定したディレクトリが存在する場合はそのリポジトリ、存在しなければクローンして返します。
        /// </summary>
        /// <param name="repositryPath">リポジトリのURLまたはパス</param>
        /// <param name="path">クローン先のディレクトリ</param>
        /// <returns></returns>
        public static IGitRepositry GetOrClone(string repositryPath, string path)
        {
            try
            {
                Logger.Debug("GetOrClone Start");
                if (Directory.Exists(path))
                {
                    Logger.Trace($"'{path}' が存在");
                    // ディレクトリが存在していればそのリポジトリを返す
                    // NOTE: Repositryコンストラクタにはローカルのディレクトリを入れる
                    var repositry = new Repository(path);
                    IGitRepositry gitRepositry = new GitRepository(repositry, path);
                    return gitRepositry;
                }
                else
                {
                    Logger.Trace($"{path} は存在しない");
                    // ディレクトリが存在しなければCloneする
                    return Clone(repositryPath, path);
                }
            }catch(Exception ex)
            {
                Logger.Error(ex, "GetOrClone");
                throw;
            }
        }
    }
}
