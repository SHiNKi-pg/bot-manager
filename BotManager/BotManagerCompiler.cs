using BotManager.Common.Web;
using BotManager.Common;
using BotManager.Database;
using BotManager.Engine;
using BotManager.Notifiers.EarthquakeMonitor;
using BotManager.Reactive;
using BotManager.Service.Compiler;
using BotManager.Service.Discord;
using BotManager.Service.Misskey;
using BotManager.Service.Twitter;
using Discord.Rest;
using Discord.WebSocket;
using Discord;
using LinqToTwitter;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Websocket.Client;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json.Linq;
using BotManager.Json;

namespace BotManager
{
    public class BotManagerCompiler : CSharpCompiler
    {
        public BotManagerCompiler(string assemblyName) : base(assemblyName, LanguageVersion.Latest)
        {
            // スクリプトで使用しているクラスを正しく読み取れるようにする
            Import<Regex>();
            Import<Mutex>();
            Import<Task>();
            Import<IBotManager>();
            Import<IEEWInfo>();
            Import<IDisposable>();
            Import(typeof(Encoding));
            Import(typeof(Observable));
            Import(typeof(Enumerable));
            Import(typeof(KeyValuePair));
            Import(typeof(BotDatabase));
            Import(typeof(HttpClientSingleton));
            Import(typeof(System.Collections.Generic.List<>));
            Import<IDisposalNotifier>();
            Import(typeof(AppSettings));
            Import<Tweet>();
            Import<SocketMessage>();
            Import<IChannel>();
            Import<WebsocketClient>();
            Import<IDiscordServiceClient>();
            Import<ITwitterServiceClient>();
            Import<IMisskeyApi>();
            Import<IDbConnection>();
            Import<Expression>();
            Import<RestUserMessage>();
            Import<External.SubscriptionArguments>();
            Import<DbContext>();
            Import<TableAttribute>();
            Import<ColumnAttribute>();
            Import(typeof(DbSet<>));
            Import<System.Net.Http.HttpClient>();
            Import<StringSyntaxAttribute>();
            Import<JObject>();
            Import(typeof(JsonUtility));
            Import<ValueTask>();
        }
    }
}
