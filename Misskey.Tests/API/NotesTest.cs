using BotManager.Engine;
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
            var certification = AppSettings.GetBotDictionary()["test"].MisskeySetting!.Certificate;
            this.output = output;
            client = new(certification.Host, certification.AccessToken);
        }

        [Fact(DisplayName = "設定ファイルからの取得テスト")]
        public void MisskeyBotSettingTest()
        {
            var bots = AppSettings.Bots;
            foreach (var bot in bots)
            {
                var misskey = bot.MisskeySetting;
                if (misskey != null)
                {
                    output.WriteLine("BotId : {0}, Host : {1}, Token : {2}", bot.Id, misskey.Certificate.Host, misskey.Certificate.AccessToken);
                }
            }
        }

        [Fact(DisplayName = "ノート情報取得")]
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

        [Fact(DisplayName = "ノート投稿テスト")]
        public async Task PostNoteAsync()
        {
            try
            {
                var api = client.GetMisskeyHttpApiClient();
                var note = await api.Notes.CreateNote(text: "APIで投稿テスト");
                output.WriteLine("ノートID : {0}", note.Id);
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
