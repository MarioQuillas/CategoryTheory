using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GeneralThings.Monads
{
    public static partial class LazyExtensions // Lazy<T> : IMonad<Lazy<>>
    {
        // Multiply: Lazy<Lazy<TSource> -> Lazy<TSource>
        public static Lazy<TSource> Multiply<TSource>(this Lazy<Lazy<TSource>> sourceWrapper) =>
            sourceWrapper.SelectMany(Id, False);

        // Unit: TSource -> Lazy<TSource>
        public static Lazy<TSource> Unit<TSource>(TSource value) => Lazy(value);

        // SelectMany: (Lazy<TSource>, TSource -> Lazy<TSelector>, (TSource, TSelector) -> TResult) -> Lazy<TResult>
        public static Lazy<TResult> SelectMany<TSource, TSelector, TResult>(
            this Lazy<TSource> source,
            Func<TSource, Lazy<TSelector>> selector,
            Func<TSource, TSelector, TResult> resultSelector) =>
                new Lazy<TResult>(() => resultSelector(source.Value, selector(source.Value).Value));

        internal static void Workflow()
        {
            Lazy<string> query = from filePath in new Lazy<string>(Console.ReadLine)
                                 from encodingName in new Lazy<string>(Console.ReadLine)
                                 from encoding in new Lazy<Encoding>(() => Encoding.GetEncoding(encodingName))
                                 from fileContent in new Lazy<string>(() => File.ReadAllText(filePath, encoding))
                                 select fileContent; // Define query.
            string result = query.Value; // Execute query.
        }
    }
}
