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
        #endregion
        #region Constructor
#pragma warning disable 8618
        public DatabaseContext(string connectionString)
        {
            this._connectionString = connectionString;
        }
#pragma warning restore
        #endregion

        #region Overrides
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseOracle(_connectionString);
        }
        #endregion

        #region DBSet
        // NOTE: 実テーブル名のプロパティを作成しないとアクセスできない
        public DbSet<User> MST_USER { get; internal set; }
        public DbSet<User> Users => MST_USER;

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

        #endregion
    }
}
