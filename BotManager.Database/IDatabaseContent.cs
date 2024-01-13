﻿using BotManager.Database.Entities;
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

        /// <summary>
        /// 変更内容をコミットします。
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync();

        /// <summary>
        /// MST_USER
        /// </summary>
        DbSet<User> Users { get; }

        /// <summary>
        /// MST_BOT
        /// </summary>
        DbSet<Bot> Bots { get; }

        /// <summary>
        /// MST_DISCORD_USER
        /// </summary>
        DbSet<DiscordUser> DiscordUsers { get; }

        /// <summary>
        /// MST_MISSKEY_USER
        /// </summary>
        DbSet<MisskeyUser> MisskeyUsers { get; }
    }
}
