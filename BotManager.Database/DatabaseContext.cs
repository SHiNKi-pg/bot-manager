using BotManager.Common;
using BotManager.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
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

        public DbSet<DiscordUser> MST_DISCORD_USER { get; internal set; }

        public DbSet<DiscordUser> DiscordUsers => MST_DISCORD_USER;

        public DbSet<MisskeyUser> MST_MISSKEY_USER { get; internal set; }

        public DbSet<MisskeyUser> MisskeyUsers => MST_MISSKEY_USER;

        public DbSet<Emotion> TBL_EMOTION { get; internal set; }

        public DbSet<Emotion> Emotions => TBL_EMOTION;

        public DbSet<EmotionTotal> MV_EMOTION { get; internal set; }

        public DbSet<EmotionTotal> EmotionTotals => MV_EMOTION;

        public DbSet<UserName> MST_USER_NAME { get; internal set; }
        public DbSet<UserName> UserNames => MST_USER_NAME;

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

        public async Task TransferFromMisskeyAsync(ulong userid, string misskeyUserId)
        {
            using(var command = base.Database.GetDbConnection().CreateCommand())
            {
                await base.Database.OpenConnectionAsync();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PKG_DISCORD.TRANSFER_FROM_MISSKEY";

                // userid
                var param = command.CreateParameter();
                param.Size = 12;
                param.DbType = DbType.Int64;
                param.Value = userid;
                command.Parameters.Add(param);

                // misskeyUserId
                param = command.CreateParameter();
                param.DbType = DbType.AnsiString;
                param.Value = misskeyUserId;
                command.Parameters.Add(param);

                await command.PrepareAsync();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task TransferFromDiscordAsync(ulong userid, ulong discordUserId)
        {
            using (var command = base.Database.GetDbConnection().CreateCommand())
            {
                await base.Database.OpenConnectionAsync();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PKG_MISSKEY.TRANSFER_FROM_DISCORD";

                // userid
                var param = command.CreateParameter();
                param.Size = 12;
                param.DbType = DbType.Int64;
                param.Value = userid;
                command.Parameters.Add(param);

                // discordUserId
                param = command.CreateParameter();
                param.Size = 20;
                param.DbType = DbType.Int64;
                param.Value = discordUserId;
                command.Parameters.Add(param);

                await command.PrepareAsync();
                await command.ExecuteNonQueryAsync();
            }
        }
        #endregion
    }
}
