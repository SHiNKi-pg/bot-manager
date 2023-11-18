using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Notifiers.EarthquakeMonitor
{
    /// <summary>
    /// 緊急地震速報情報インターフェース
    /// </summary>
    public interface IEEWInfo
    {
        /// <summary>
        /// 結果データ
        /// </summary>
        IEEWResult Result { get; }

        /// <summary>
        /// 更新日時
        /// </summary>
        DateTime ReportTime { get; }

        /// <summary>
        /// 地震ID
        /// </summary>
        string ReportId { get; }

        /// <summary>
        /// 発生時刻
        /// </summary>
        string OriginTime { get; }

        /// <summary>
        /// 第n報
        /// </summary>
        string ReportNum { get; }

        /// <summary>
        /// 震源地
        /// </summary>
        string RegionName { get; }

        /// <summary>
        /// 緯度
        /// </summary>
        string Latitude { get; }

        /// <summary>
        /// 経度
        /// </summary>
        string Longitude { get; }

        /// <summary>
        /// 震源の深さ
        /// </summary>
        string Depth { get; }

        /// <summary>
        /// 最大震度
        /// </summary>
        string Calcintensity { get; }

        /// <summary>
        /// マグニチュード
        /// </summary>
        string Magunitude { get; }

        /// <summary>
        /// 最終報
        /// </summary>
        bool IsFinal { get; }

        /// <summary>
        /// キャンセル報
        /// </summary>
        bool IsCancel { get; }

        /// <summary>
        /// 訓練報
        /// </summary>
        bool IsTraining { get; }

        /// <summary>
        /// 予報ではなく警報かどうか
        /// </summary>
        bool IsAlert { get; }
    }

    /// <summary>
    /// 緊急地震速報結果インターフェース
    /// </summary>
    public interface IEEWResult
    {
        /// <summary>
        /// ステータス
        /// </summary>
        string Status { get; }

        /// <summary>
        /// メッセージ
        /// </summary>
        string Message { get; }

        /// <summary>
        /// 
        /// </summary>
        bool IsAuth { get; }
    }
}
