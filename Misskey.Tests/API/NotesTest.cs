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
        private readonly IMisskeyServiceClient client;
        private readonly ITestOutputHelper output;

        public NotesTest(ITestOutputHelper output)
        {
            var certification = AppSettings.GetBotDictionary()["test"].MisskeySetting!.Certificate;
            this.output = output;
            client = MisskeyService.Create(certification.Host, certification.AccessToken);
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
                    Assert.False(string.IsNullOrEmpty(note.Id), "取得したノートのIDは取得できるはず");
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Fact(DisplayName = "ノート投稿＆削除テスト")]
        public async Task PostNoteAsync()
        {
            try
            {
                var api = client.GetMisskeyHttpApiClient();
                var note = await api.Notes.CreateNote(text: "APIで投稿テスト\n" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));

                string noteId = note.Note.Id;
                output.WriteLine("ノートID : {0}", noteId);

                Assert.False(string.IsNullOrEmpty(noteId), "作成したノートのIDは取得できるはず");

                // 3秒後、投稿したノートの削除
                await Task.Delay(3000);
                await api.Notes.DeleteNote(noteId);
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
