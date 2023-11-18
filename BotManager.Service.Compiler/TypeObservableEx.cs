using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Compiler
{
    /// <summary>
    /// <see cref="Type"/>の<see cref="IObservable{T}"/>の拡張メソッド定義用
    /// </summary>
    public static class TypeObservableEx
    {
        /// <summary>
        /// 流れてきた<see cref="Type"/>の属性が指定した条件を満たす場合、後続に型情報を流します。
        /// </summary>
        /// <typeparam name="Attr"></typeparam>
        /// <param name="observable"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IObservable<Type> HasAttribute<Attr>(this IObservable<Type> observable, Func<Attr, bool> predicate) where Attr : Attribute
        {
            return observable.Where(t =>
            {
                var attr = t.GetCustomAttribute<Attr>();
                return attr is not null && predicate(attr);
            });
        }

        /// <summary>
        /// 流れてきた<see cref="Type"/>の属性を持つ場合、後続に型情報を流します。
        /// </summary>
        /// <typeparam name="Attr"></typeparam>
        /// <param name="observable"></param>
        /// <returns></returns>
        public static IObservable<Type> HasAttribute<Attr>(this IObservable<Type> observable) where Attr : Attribute
        {
            return observable.Where(t =>
            {
                var attr = t.GetCustomAttribute<Attr>();
                return attr is not null;
            });
        }

        /// <summary>
        /// 流れてきた型のインスタンスを作成します。コンストラクタの引数が0個のものを定義されている必要があります。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="observable"></param>
        /// <returns></returns>
        public static IObservable<T> NewAs<T>(this IObservable<Type> observable)
        {
            return Observable.Create<T>(obsr =>
            {
                return observable.Subscribe(t =>
                {
                    try
                    {
                        // Typeからインスタンスを作成する
                        var obj = Activator.CreateInstance(t);
                        if (obj is not null && obj is T tobj)
                        {
                            // T型であればオブザーバーに作成したインスタンスを通知する
                            obsr.OnNext(tobj);
                        }
                    }
                    catch(Exception e)
                    {
                        obsr.OnError(e);
                    }
                },
                obsr.OnError,
                obsr.OnCompleted);
            });
        }

        #region WarppedAssembly

        /// <summary>
        /// 指定した型名の<see cref="Type"/>オブジェクトを後続に流します。
        /// </summary>
        /// <param name="observable"></param>
        /// <param name="typeName">型名</param>
        /// <returns></returns>
        public static IObservable<Type> GetType(this IObservable<IAssembly> observable, string typeName)
        {
            return Observable.Create<Type>(obsr =>
            {
                return observable.Subscribe(a =>
                {
                    a.GetType(typeName).Subscribe(t => obsr.OnNext(t));
                },obsr.OnError, obsr.OnCompleted);
            });
        }

        /// <summary>
        /// このアセンブリが持つ型を列挙します。
        /// </summary>
        /// <param name="observable"></param>
        /// <returns></returns>
        public static IObservable<Type> GetTypes(this IObservable<IAssembly> observable)
        {
            return Observable.Create<Type>(obsr =>
            {
                return observable.Subscribe(a =>
                {
                    foreach(var type in a.GetTypes())
                    {
                        obsr.OnNext(type);
                    }
                }, obsr.OnError, obsr.OnCompleted);   
            });
        }

        #endregion
    }
}
