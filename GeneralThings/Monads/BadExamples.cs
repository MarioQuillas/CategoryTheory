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

        internal static void MonoidLaws()
        {
            (string, int) source = ("a", 1);

            // Associativity preservation: source.Wrap().Multiply().Wrap().Multiply() == source.Wrap().Wrap().Multiply().Multiply().
            source
                .ValueTuple<string, (string, int)>()
                .Multiply()
                .ValueTuple<string, (string, int)>()
                .Multiply()
                .WriteLine(); // (, 1)
            source
                .ValueTuple<string, (string, int)>()
                .ValueTuple<string, (string, (string, int))>()
                .Multiply()
                .Multiply()
                .WriteLine(); // (, 1)
                              // Left unit preservation: Unit(f).Multiply() == source.
            Unit<string, (string, int)>(source).Multiply().WriteLine(); // (, 1)
                                                                        // Right unit preservation: source == source.Select(Unit).Multiply().
            source.Select(Unit<string, int>).Multiply().WriteLine(); // (a, 1)
        }

        internal static void MonadLawsUnitorLaws()
        {
            ValueTuple<string, int> source = ("a", 1);
            Func<int, ValueTuple<string, char>> selector = int32 => ("b", '@');
            Func<int, ValueTuple<string, double>> selector1 = int32 => ("c", Math.Sqrt(int32));
            Func<double, ValueTuple<string, string>> selector2 = @double => ("d", @double.ToString("0.00"));
            const int Value = 5;

            // Associativity: source.SelectMany(selector1).SelectMany(selector2) == source.SelectMany(value => selector1(value).SelectMany(selector2)).
            (from value in source
             from result1 in selector1(value)
             from result2 in selector2(result1)
             select result2).WriteLine(); // (a, 1.00)
            (from value in source
             from result in (from result1 in selector1(value) from result2 in selector2(result1) select result2)
             select result).WriteLine(); // (a, 1.00)
                                         // Left unit: value.Wrap().SelectMany(selector) == selector(value).
            (from value in Value.ValueTuple<string, int>()
             from result in selector(value)
             select result).WriteLine(); // (, @)
            selector(Value).WriteLine(); // (b, @)
                                         // Right unit: source == source.SelectMany(Wrap).
            (from value in source
             from result in value.ValueTuple<string, int>()
             select result).WriteLine(); // (a, 1)
        }
    }
}
