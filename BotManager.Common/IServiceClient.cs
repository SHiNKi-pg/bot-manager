using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Common
{
    /// <summary>
    /// サービスクライアントインターフェース
    /// </summary>
    public interface IServiceClient : IDisposable, IStartEnd
    {
    }
}
