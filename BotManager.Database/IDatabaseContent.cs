using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Database
{
    /// <summary>
    /// データベースアクセスインターフェース
    /// </summary>
    public interface IDatabaseContent : IDisposable
    {
        /// <summary>
        /// トランザクションを開始します。
        /// </summary>
        /// <returns></returns>
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
