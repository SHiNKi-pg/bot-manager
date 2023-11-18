using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Notifiers.EarthquakeMonitor
{
#pragma warning disable CS8618

    [JsonObject("EEW")]
    internal class EEWInfo : IEEWInfo
    {
        private static string ALERT_FLAG_A = "警報";

        public IEEWResult Result { get => _EEWResult; }

        [JsonProperty("result")]
        internal EEWResult _EEWResult { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; } = "";

        [JsonProperty("message")]
        public string Message { get; set; } = "";

        [JsonProperty("is_auth")]
        public bool IsAuth { get; set; }

        [JsonProperty("report_time")]
        internal string _reportTime { get; set; }

        public DateTime ReportTime => throw new NotImplementedException();

        [JsonProperty("report_id")]
        public string ReportId { get; set; }

        [JsonProperty("origin_time")]
        public string OriginTime { get; set; }

        [JsonProperty("report_num")]
        public string ReportNum { get; set; }

        [JsonProperty("region_name")]
        public string RegionName { get; set; }

        [JsonProperty("latitude")]
        public string Latitude { get; set; }

        [JsonProperty("longitude")]
        public string Longitude { get; set; }

        [JsonProperty("depth")]
        public string Depth { get; set; }

        [JsonProperty("calcintensity")]
        public string Calcintensity { get; set; }

        [JsonProperty("magunitude")]
        public string Magunitude { get; set; }

        [JsonProperty("is_final")]
        internal bool? _IsFinal { get; set; }

        [JsonProperty("is_cancel")]
        internal bool? _IsCancel { get; set; }

        [JsonProperty("is_training")]
        internal bool? _IsTraining { get; set; }

        [JsonProperty("alertflg")]
        internal string? alertFlag { get; set; }

        public bool IsAlert => !string.IsNullOrEmpty(alertFlag) && alertFlag == ALERT_FLAG_A;

        public bool IsFinal => _IsFinal.HasValue && _IsFinal.Value;

        public bool IsCancel => _IsCancel.HasValue && _IsCancel.Value;

        public bool IsTraining => _IsTraining.HasValue && _IsTraining.Value;
    }

    [JsonObject("result")]
    internal class EEWResult : IEEWResult
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("is_auth")]
        public bool IsAuth { get; set; }
    }
#pragma warning restore

}
