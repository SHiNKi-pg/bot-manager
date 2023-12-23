using BotManager.Service.Misskey;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Misskey.Tests.API
{
    public class NotesTest : IDisposable
    {
        private readonly MisskeyClient client;
        private readonly ITestOutputHelper output;

        public NotesTest(ITestOutputHelper output)
        {
            this.output = output;
            client = new("misskey.io", "");
        }

        [Fact]
        public async Task GetNotesTest()
        {
            try
            {
                var api = client.GetMisskeyHttpApiClient();
                var notes = await api.Notes.GetNotesAsync();
                foreach(var note in notes)
                {
                    output.WriteLine(note.Text);
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        public void Dispose()
        {
            client.Dispose();
        }
    }
}
