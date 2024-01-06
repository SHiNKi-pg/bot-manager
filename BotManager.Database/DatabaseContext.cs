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
        public DatabaseContext(string connectionString)
        {
            this._connectionString = connectionString;
        }
        #endregion

        #region Overrides
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseOracle(_connectionString);
        }
        #endregion

        #region DBSet
        #endregion

        #region Method
        public Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return base.Database.BeginTransactionAsync();
        }
        #endregion
    }
}
