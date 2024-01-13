using BotManager.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Notifiers.EarthquakeMonitor
{
    /// <summary>
    /// 強震モニタインターフェース
    /// </summary>
    public interface IEEWMonitor : IDisposable
    {
        /// <summary>
        /// 緊急地震速報が発表されると通知されます。
        /// </summary>
        IObservable<IEEWInfo> EEWObservable { get; }
    }

    /// <summary>
    /// 強震モニタ
    /// </summary>
    internal class EEWMonitor : IEEWMonitor
    {
        // 強震モニタ取得URL
        private const string MONITOR_BASE_URL = "http://www.kmoni.bosai.go.jp/webservice/hypo/eew/";
        // 緊急地震速報の情報が無い場合の Result.Message の文字列
        private const string NO_DATA_MESSAGE = "データがありません";

        private CancellationTokenSource cancellationTokenSource;

        private Subject<IEEWInfo> _eewSubject;

        private readonly static ILog Logger = Log.GetLogger("eew");

        /// <summary>
        /// 緊急地震速報が発表されると通知されます。
        /// </summary>
        public IObservable<IEEWInfo> EEWObservable { get => _eewSubject.AsObservable(); }

        private IDisposable subscription;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nowObservable">現在日時が通知されるオブザーバー</param>
        public EEWMonitor(IObservable<DateTime> nowObservable)
        {
            cancellationTokenSource = new();
            _eewSubject = new();
            // 出来れば1秒毎に通知されるものが望ましい。
            subscription = nowObservable.Subscribe(NotifyEarthquakeNotification);
            Logger.Debug("EEWMonitor Created");
        }

        private async void NotifyEarthquakeNotification(DateTime dateTime)
        {
            try
            {
                var eewInfo = await FetchData(dateTime);
                if(eewInfo is not null && eewInfo.Result.Message != NO_DATA_MESSAGE)
                {
                    Logger.Info($"緊急地震速報({(eewInfo.IsAlert ? "警報" : "予報")})\n地震ID : {eewInfo.ReportId}(第{eewInfo.ReportNum}報)\n予想最大震度 : {eewInfo.Calcintensity}\nマグニチュード : {eewInfo.Magunitude}");
                    _eewSubject.OnNext(eewInfo);
                }
            }
            catch (ObjectDisposedException ex)
            {
                Logger.Error(ex, "EEW");
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "EEW");
                _eewSubject.OnError(ex);
            }
        }

        private async Task<IEEWInfo?> FetchData(DateTime dateTime)
        {
            var httpClient = Common.Web.HttpClientSingleton.HttpClient;
            string pathName = dateTime.ToString("yyyyMMddHHmmss") + ".json";

            using (var response = await httpClient.GetAsync(MONITOR_BASE_URL + pathName, cancellationTokenSource.Token))
            {
                string content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<EEWInfo>(content);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Logger.Debug("EEWMonitor Disposing");
            subscription.Dispose();
            _eewSubject.Dispose();
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
            Logger.Debug("EEWMonitor Disposed");
        }
    }
}
