using BotManager.Common.Messaging;
using BotManager.Service.Misskey.Schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Misskey.Messaging
{
    internal class MisskeyMessage : IReplyableMessageWithId<string, string>, IDeletableMessage, IWaitableMessage<IReplyableMessageWithId<string, string>>
    {
        private readonly Note note;
        private readonly IMisskeyServiceClient client;

        public MisskeyMessage(IMisskeyServiceClient client, Note note)
        {
            this.client = client;
            this.note = note;
        }

        public string Id => note.Id;

        public string Content => note.Text ?? string.Empty;

        public DateTime ReceivedTime => note.CreatedAt.LocalDateTime;

        public bool IsBot => note.User?.IsBot ?? false;

        public string ReplyMessageId => note.ReplyId ?? string.Empty;

        public string AuthorId => note.UserId;

        public string AuthorName => note.User?.UserName ?? string.Empty;

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

        public IObservable<IReplyableMessageWithId<string, string>> CreateReceiveNotifier()
        {
            return Observable.Create<IReplyableMessageWithId<string, string>>(observer =>
            {
                return client.MessagingReceived
                    .Where(m => m.ReplyMessageId == Id)
                    .Subscribe(observer);
            });
        }
    }
}
