using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Database
{
    internal class DatabaseContext : DbContext
    {
        #region Constructor
        #endregion

        #region Overrides
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseOracle();
        }
        #endregion

        #region DBSet
        #endregion

        #region Method
        #endregion
    }
}
