using BotManager.Common.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Discord.Messaging
{
    internal class DiscordMessage : IReplyableMessageWithId<ulong>, IDeletableMessage, IWaitableMessage<IReplyableMessageWithId<ulong>>
    {
        private readonly global::Discord.IMessage message;
        private readonly IDiscordServiceClient client;

        public DiscordMessage(IDiscordServiceClient client, global::Discord.IMessage message)
        {
            this.message = message;
            this.client = client;
        }

        public string Content => message.Content;

        public DateTime ReceivedTime => message.CreatedAt.DateTime;

        public ulong Id => message.Id;

        public bool IsBot => message.Author.IsBot || message.Author.IsWebhook;

        public ulong ReplyMessageId => message.Reference.MessageId.Value;

        public IObservable<IReplyableMessageWithId<ulong>> CreateReceiveNotifier()
        {
            return Observable.Create<IReplyableMessageWithId<ulong>>(observer =>
            {
                IMessageReceived<IReplyableMessageWithId<ulong>> messageReceived = client;
                return messageReceived.MessageReceived
                    .Where(m => m.ReplyMessageId == Id)
                    .Subscribe(observer);
            });
        }

        public async Task Delete()
        {
            await message.DeleteAsync();
        }

        public async Task<IReplyableMessage> Reply(string content)
        {
            var mes = await message.Channel.SendMessageAsync(content, messageReference: message.Reference);
            return new DiscordMessage(client, mes);
        }
    }
}
