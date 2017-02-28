using System;
using System.IO;
using System.Text;

namespace GeneralThings.Monads
{
    public static partial class FuncExtensions // Func<T> : IMonad<Func<>>
    {
        // Multiply: Func<Func<T> -> Func<T>
        public static Func<TSource> Multiply<TSource>(this Func<Func<TSource>> sourceWrapper) =>
            sourceWrapper.SelectMany(source => source, (source, value) => value);

        // Unit: Unit -> Func<Unit>
        public static Func<TSource> Unit<TSource>(TSource value) => Func(value);

        // SelectMany: (Func<TSource>, TSource -> Func<TSelector>, (TSource, TSelector) -> TResult) -> Func<TResult>
        public static Func<TResult> SelectMany<TSource, TSelector, TResult>(
            this Func<TSource> source,
            Func<TSource, Func<TSelector>> selector,
            Func<TSource, TSelector, TResult> resultSelector) => () =>
        {
            TSource value = source();
            return resultSelector(value, selector(value)());
        };
    }

    internal static void Workflow()
    {
        Func<string> query = from filePath in new Func<string>(Console.ReadLine)
                             from encodingName in new Func<string>(Console.ReadLine)
                             from encoding in new Func<Encoding>(() => Encoding.GetEncoding(encodingName))
                             from fileContent in new Func<string>(() => File.ReadAllText(filePath, encoding))
                             select fileContent; // Define query.
        string result = query(); // Execute query.
    }
}