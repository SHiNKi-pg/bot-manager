using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Reactive.Json
{
    /// <summary>
    /// リアクティブJSON拡張メソッド定義クラス
    /// </summary>
    public static class RxJson
    {
        /// <summary>
        /// JSON文字列を .NET オブジェクトに変換し、後続へ通知します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonStrings">JSON文字列が通知されるストリーム</param>
        /// <returns></returns>
        public static IObservable<T> WhereIs<T>(this IObservable<string> jsonStrings)
        {
            return Observable.Create<T>(observer =>
            {
                return jsonStrings.Subscribe(jsonString =>
                {
                    try
                    {
                        var json = JsonConvert.DeserializeObject<T>(jsonString);
                        if (json is T t)
                        {
                            // JSON文字列を変換した結果、T型なら後続に通知する。
                            observer.OnNext(t);
                        }
                    }catch(Exception ex)
                    {
                        // 変換等で何らかエラーが起きた場合は後続に通知
                        observer.OnError(ex);
                    }
                },
                observer.OnError,
                observer.OnCompleted);
            });
        }

        /// <summary>
        /// JSON文字列を .NET オブジェクトに変換し、それが指定した条件を満たす場合に後続へ通知します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonStrings">JSON文字列が通知されるストリーム</param>
        /// <param name="predicate">条件</param>
        /// <returns></returns>
        public static IObservable<T> WhereIs<T>(this IObservable<string> jsonStrings, Func<T, bool> predicate)
        {
            return Observable.Create<T>(observer =>
            {
                return jsonStrings.Subscribe(jsonString =>
                {
                    try
                    {
                        var json = JsonConvert.DeserializeObject<T>(jsonString);
                        if (json is T t && predicate(t))
                        {
                            // JSON文字列を変換した結果、T型なら後続に通知する。
                            observer.OnNext(t);
                        }
                    }
                    catch (Exception ex)
                    {
                        // 変換等で何らかエラーが起きた場合は後続に通知
                        observer.OnError(ex);
                    }
                },
                observer.OnError,
                observer.OnCompleted);
            });
        }
    }
}
