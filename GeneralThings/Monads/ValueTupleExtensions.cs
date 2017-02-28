using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralThings.Monads
{

    public static partial class ValueTupleExtensions // ValueTuple<T, TResult> : IMonad<ValueTuple<T,>>
    {
        // Multiply: ValueTuple<T, ValueTuple<T, TSource> -> ValueTuple<T, TSource>
        public static (T, TSource) Multiply<T, TSource>(this (T, (T, TSource)) sourceWrapper) =>
            sourceWrapper.SelectMany(source => source, (source, value) => value); // Immediate execution.

        // Unit: TSource -> ValueTuple<T, TSource>
        public static (T, TSource) Unit<T, TSource>(TSource value) => ValueTuple<T, TSource>(value);

        // SelectMany: (ValueTuple<T, TSource>, TSource -> ValueTuple<T, TSelector>, (TSource, TSelector) -> TResult) -> ValueTuple<T, TResult>
        public static (T, TResult) SelectMany<T, TSource, TSelector, TResult>(
            this (T, TSource) source,
            Func<TSource, (T, TSelector)> selector,
            Func<TSource, TSelector, TResult> resultSelector) =>
                (source.Item1, resultSelector(source.Item2, selector(source.Item2).Item2)); // Immediate execution.


        internal static void Workflow()
        {
            ValueTuple<string> query = from filePath in new ValueTuple<string>(Console.ReadLine())
                                       from encodingName in new ValueTuple<string>(Console.ReadLine())
                                       from encoding in new ValueTuple<Encoding>(Encoding.GetEncoding(encodingName))
                                       from fileContent in new ValueTuple<string>(File.ReadAllText(filePath, encoding))
                                       select fileContent; // Define and execute query.
            string result = query.Item1; // Query result.
        }
    }
}
