using BotManager.Common;
using BotManager.Service.Compiler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Engine
{
    /// <summary>
    /// BotManager 実行機関クラス
    /// </summary>
    internal sealed class BotMechanism : IBotMechanism
    {
        #region Private Fields
        private ICompiler compiler;
        private IBotManager? _botManager;
        #endregion

        #region Constructor
        public BotMechanism(string assemblyName)
        {
            compiler = new CSharpCompiler(assemblyName);
        }
        #endregion

        public Task Start()
        {
            // TODO: 開始した時に行う処理
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _botManager?.Dispose();
            compiler.Dispose();
        }
    }
}
