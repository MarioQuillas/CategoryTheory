using System;
using System.IO;
using System.Text;
using GeneralThings.Functors;

namespace GeneralThings.Monads
{
    public static partial class OptionalExtensions // Optional<T> : IMonad<Optional<>>
    {
        // Multiply: Optional<Optional<TSource> -> Optional<TSource>
        public static Optional<TSource> Multiply<TSource>(this Optional<Optional<TSource>> sourceWrapper) =>
            sourceWrapper.SelectMany(source => source, (source, value) => value);

        // Unit: TSource -> Optional<TSource>
        public static Optional<TSource> Unit<TSource>(TSource value) => Optional(value);

        // SelectMany: (Optional<TSource>, TSource -> Optional<TSelector>, (TSource, TSelector) -> TResult) -> Optional<TResult>
        public static Optional<TResult> SelectMany<TSource, TSelector, TResult>(
            this Optional<TSource> source,
            Func<TSource, Optional<TSelector>> selector,
            Func<TSource, TSelector, TResult> resultSelector) => new Optional<TResult>(() =>
        {
            if (source.HasValue)
            {
                Optional<TSelector> result = selector(source.Value);
                if (result.HasValue)
                {
                    return (true, resultSelector(source.Value, result.Value));
                }
            }
            return (false, default(TResult));
        });
    }

    internal static void Workflow()
    {
        string input;
        Optional<string> query =
            from filePath in new Optional<string>(() => string.IsNullOrWhiteSpace(input = Console.ReadLine())
                ? (false, default(string)) : (true, input))
            from encodingName in new Optional<string>(() => string.IsNullOrWhiteSpace(input = Console.ReadLine())
                ? (false, default(string)) : (true, input))
            from encoding in new Optional<Encoding>(() =>
            {
                try
                {
                    return (true, Encoding.GetEncoding(encodingName));
                }
                catch (ArgumentException)
                {
                    return (false, default(Encoding));
                }
            })
            from fileContent in new Optional<string>(() => File.Exists(filePath)
                ? (true, File.ReadAllText(filePath, encoding)) : (false, default(string)))
            select fileContent; // Define query.
        if (query.HasValue) // Execute query.
        {
            string result = query.Value;
        }
    }

}