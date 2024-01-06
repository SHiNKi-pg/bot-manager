using BotManager.Service.Compiler;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Engine
{
    internal class Compiler : CSharpCompiler
    {
        public Compiler(string assemblyName) : base(assemblyName, LanguageVersion.Latest)
        {
        }
    }
}
