using BotManager.Common;
using BotManager.Engine;
using BotManager.Reactive;
using BotManager.Service.Compiler;
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

namespace Compiler.Tests
{
    public class TestCompiler : CSharpCompiler
    {
        public TestCompiler(string assemblyName) : base(assemblyName, LanguageVersion.Latest)
        {
            // スクリプトで使用しているクラスを正しく読み取れるようにする
            Import<Regex>();
            Import<Mutex>();
            Import<Task>();
            Import<IBotManager>();
            Import<IDisposable>();
            Import(typeof(Encoding));
            Import(typeof(Observable));
            Import(typeof(Enumerable));
            Import(typeof(KeyValuePair));
            Import(typeof(System.Collections.Generic.List<>));
            Import<IDisposalNotifier>();
            Import(typeof(AppSettings));
            Import<IDbConnection>();
            Import<Expression>();
        }

        public async Task CompileFrom(DirectoryInfo directory, string filter)
        {
            ClearSources();
            var files = directory.EnumerateFiles(filter, SearchOption.AllDirectories);
            foreach (var file in files)
            {
                await AddSourceFile(file.FullName, Encoding.UTF8, true);
            }
            Compile();
        }
    }
}
