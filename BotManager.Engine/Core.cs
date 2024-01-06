using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Engine
{
    /// <summary>
    /// Botマネージャー機関
    /// </summary>
    public static class Core
    {
        /// <summary>
        /// Botマネージャー機関インスタンスを作成します。
        /// </summary>
        /// <param name="assembly">アセンブリ名</param>
        /// <returns></returns>
        public static IBotMechanism Create(string assembly)
        {
            return new BotMechanism(assembly);
        }
    }
}
