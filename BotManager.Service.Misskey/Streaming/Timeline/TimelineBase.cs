using BotManager.Service.Misskey.Schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Websocket.Client;
using System.Net.WebSockets;
using Newtonsoft.Json;
using System.Reactive.Subjects;

namespace BotManager.Service.Misskey.Streaming.Timeline
{
    internal abstract class TimelineBase : ITimeline, IDisposable, IObserver<ResponseMessage>
    {
        public IObservable<Note> Notes { get => _notes.AsObservable(); }

        public TimelineBase(IObservable<ResponseMessage> responseMessage)
        {
            _notes = new();
            _subscription = responseMessage.Subscribe(this);
        }

        public void Dispose()
        {
            _subscription.Dispose();
        }

        public void OnCompleted()
        {
            _notes.OnCompleted();
        }

        public void OnError(Exception error)
        {
            _notes.OnError(error);
        }

        
        public void OnNext(ResponseMessage value)
        {
            if (value.MessageType != WebSocketMessageType.Text)
            {
                // テキスト形式のデータのみ受け付ける
                return;
            }

            string? text = value.Text;
            if (string.IsNullOrEmpty(text))
            {
                // テキストデータが無ければ何もしない
                return;
            }

            var note = JsonConvert.DeserializeObject<Note>(text);
            if(note is not null)
            {
                // ノートイベントの発行
                _notes.OnNext(note);
            }
        }

        #region Private Field
        private IDisposable _subscription;
        private Subject<Note> _notes;
        #endregion
    }
}
