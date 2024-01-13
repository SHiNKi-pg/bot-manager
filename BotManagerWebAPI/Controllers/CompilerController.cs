using BotManager.Engine;
using BotManager.Service.Compiler.Results;
using BotManager.Service.Git;
using LibGit2Sharp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.Reactive.Linq;

namespace BotManagerWebAPI.Controllers
{
    [Route("api/compiler")]
    [ApiController]
    public class CompilerController : ControllerBase
    {
        [HttpPost("precompile")]
        public async Task<ActionResult> Precompile(string branchName = "main")
        {
            try
            {
                var compiler = Entry.BotMechanism.PreCompiler;
                using (var repos = Entry.BotMechanism.GetScriptRepositry())
                {
                    repos.Checkout(branchName);
                    var compilerobs = compiler.Precompile().Publish();

                    bool success = false;
                    IEnumerable<Diagnostic> messages = Enumerable.Empty<Diagnostic>();

                    compilerobs.OfType<ICompileSuccessed>().Subscribe(r => success = r.Success);
                    compilerobs.OfType<ICompileMessage>().Subscribe(c => messages = c.Messages);

                    using(compilerobs.Connect())
                    {
                        await compilerobs.Count();
                    }

                    // ブランチを元に戻す
                    repos.Checkout(AppSettings.Script.BranchName);

                    return StatusCode(200, new
                    {
                        Result = success,
                        Messages = messages.Select(mes => new
                        {
                            Severity = mes.Severity,
                            Id = mes.Id,
                            Line = mes.Location.GetLineSpan().StartLinePosition.Line,
                            Message = mes.GetMessage(),
                        })
                    });
                }
            }catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
