using BotManager.Engine;
using BotManager.Service.Compiler.Results;
using BotManager.Service.Git;
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
                        await compilerobs;
                    }

                    // ブランチを元に戻す
                    repos.Checkout(AppSettings.Script.BranchName);

                    return StatusCode(200, new
                    {
                        result = success,
                        messages = messages
                    });
                }
            }catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
