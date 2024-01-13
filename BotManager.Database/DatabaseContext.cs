using BotManager.Common;
using BotManager.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Database
{
    internal class DatabaseContext : DbContext, IDatabaseContent
    {
        #region Private Field
        private string _connectionString;

        private static readonly ILog Logger = Log.GetLogger("DB");
        #endregion
        #region Constructor
#pragma warning disable 8618
        public DatabaseContext(string connectionString)
        {
            this._connectionString = connectionString;
            Logger.Debug("DBContext Created");
        }
#pragma warning restore
        #endregion

        #region Overrides
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Logger.Debug("Use Oracle");
            optionsBuilder.UseOracle(_connectionString);
        }
        #endregion

        #region DBSet
        // NOTE: 実テーブル名のプロパティを作成しないとアクセスできない
        public DbSet<User> MST_USER { get; internal set; }
        public DbSet<User> Users => MST_USER;

        public DbSet<Bot> MST_BOT { get; internal set; }

        public DbSet<Bot> Bots => MST_BOT;

        #endregion

        #region Method
        public Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return base.Database.BeginTransactionAsync();
        }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        public override void Dispose()
        {
            Logger.Debug("DBContext Disposing");
            base.Dispose();
            Logger.Debug("DBContext Disposed");
        }

        public override ValueTask DisposeAsync()
        {
            try
            {
                Logger.Debug("DBContext Disposing Async");
                return base.DisposeAsync();
            }
            finally
            {
                Logger.Debug("DBContext Disposed Async");
            }
        }
        #endregion
    }
}
