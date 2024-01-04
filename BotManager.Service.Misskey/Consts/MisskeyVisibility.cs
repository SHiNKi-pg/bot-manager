using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Misskey.Consts
{
    /// <summary>
    /// Misskey 公開範囲
    /// </summary>
    public static class MisskeyVisibility
    {
        /// <summary>
        /// public
        /// </summary>
        public const string PUBLIC = "public";

        /// <summary>
        /// home
        /// </summary>
        public const string HOME = "home";

        /// <summary>
        /// followers
        /// </summary>
        public const string FOLLOWERS = "followers";

        /// <summary>
        /// specified
        /// </summary>
        public const string SPECIFIED = "specified";
    }
}
