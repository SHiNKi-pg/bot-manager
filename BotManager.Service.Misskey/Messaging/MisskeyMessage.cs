using BotManager.Common.Messaging;
using BotManager.Service.Misskey.Schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Misskey.Messaging
{
    internal class MisskeyMessage : IReplyableMessage, IDeletableMessage
    {
        private readonly Note note;
        private readonly IMisskeyServiceClient client;

        public MisskeyMessage(IMisskeyServiceClient client, Note note)
        {
            this.client = client;
            this.note = note;
        }

        public string Content => note.Text ?? string.Empty;

        public DateTime ReceivedTime => note.CreatedAt.DateTime;

        public bool IsBot => note.User?.IsBot ?? false;

        public async Task Delete()
        {
            await client.Api.Notes.DeleteNote(note.Id);
        }

        public async Task<IReplyableMessage> Reply(string content)
        {
            var replyNote = await client.Api.Notes.CreateNote(
                visibility: note.Visibility,
                text: content,
                replyId: note.Id
                );
            return new MisskeyMessage(this.client, replyNote.Note);
        }
    }
}
