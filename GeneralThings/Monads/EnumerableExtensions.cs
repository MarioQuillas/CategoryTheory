using System;
using System.Collections.Generic;
using System.Linq;
using GeneralThings.ApplicativeFunctor;

namespace GeneralThings.Monads
{
    public static partial class EnumerableExtensions // IEnumerable<T> : IMonad<IEnumerable<>>
    {
        // SelectMany: (IEnumerable<TSource>, TSource -> IEnumerable<TSelector>, (TSource, TSelector) -> TResult) -> IEnumerable<TResult>
        public static IEnumerable<TResult> SelectMany<TSource, TSelector, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, IEnumerable<TSelector>> selector,
            Func<TSource, TSelector, TResult> resultSelector)
        {
            foreach (TSource value in source)
            {
                foreach (TSelector result in selector(value))
                {
                    yield return resultSelector(value, result);
                }
            }
        }

        // Wrap: TSource -> IEnumerable<TSource>
        public static IEnumerable<TSource> Enumerable<TSource>(this TSource value)
        {
            yield return value;
        }
    }

    public static partial class EnumerableExtensions // (Select, Multiply, Unit) implements (SelectMany, Wrap).
    {
        // SelectMany: (IEnumerable<TSource>, TSource -> IEnumerable<TSelector>, (TSource, TSelector) -> TResult) -> IEnumerable<TResult>
        public static IEnumerable<TResult> SelectMany<TSource, TSelector, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, IEnumerable<TSelector>> selector,
            Func<TSource, TSelector, TResult> resultSelector) =>
        (from value in source
            select (from result in selector(value)
                select resultSelector(value, result))).Multiply();

        // Compiled to:
        // source.Select(value => selector(value).Select(result => resultSelector(value, result))).Multiply();

        // Wrap: TSource -> IEnumerable<TSource>
        public static IEnumerable<TSource> Enumerable<TSource>(this TSource value) => Unit(value);
    }

    public static partial class EnumerableExtensions // (SelectMany, Wrap) implements (Select, Multiply, Unit).
    {
        // Select: (TSource -> TResult) -> (IEnumerable<TSource> -> IEnumerable<TResult>).
        public static Func<IEnumerable<TSource>, IEnumerable<TResult>> Select<TSource, TResult>(
            Func<TSource, TResult> selector) => source =>
            from value in source
            from result in value.Enumerable()
            select result;

        // source.SelectMany(Enumerable, (result, value) => value);

        // Multiply: IEnumerable<IEnumerable<TSource>> -> IEnumerable<TSource>
        public static IEnumerable<TSource> Multiply<TSource>(this IEnumerable<IEnumerable<TSource>> sourceWrapper) =>
            from source in sourceWrapper
            from value in source
            select value;

        // sourceWrapper.SelectMany(source => source, (source, value) => value);

        // Unit: TSource -> IEnumerable<TSource>
        public static IEnumerable<TSource> Unit<TSource>(TSource value) => value.Enumerable();
    }

    public static partial class EnumerableExtensions // IEnumerable<T> : IMonadFromFunctor<IEnumerable<>>
    {
        // Multiply: IEnumerable<IEnumerable<TSource>> -> IEnumerable<TSource>
        public static IEnumerable<TSource> Multiply<TSource>(this IEnumerable<IEnumerable<TSource>> sourceWrapper)
        {
            foreach (IEnumerable<TSource> source in sourceWrapper)
            {
                foreach (TSource value in source)
                {
                    yield return value;
                }
            }
        }

        // Unit: TSource -> IEnumerable<TSource>
        public static IEnumerable<TSource> Unit<TSource>(TSource value)
        {
            yield return value;
        }


        //internal static void MonoidLaws()
        //{
        //    IEnumerable<int> source = new int[] { 0, 1, 2, 3, 4 };

        //    // Associativity preservation: source.Wrap().Multiply().Wrap().Multiply() == source.Wrap().Wrap().Multiply().Multiply().
        //    source.Enumerable().Multiply().Enumerable().Multiply().WriteLines();
        //    // 0 1 2 3 4
        //    source.Enumerable().Enumerable().Multiply().Multiply().WriteLines();
        //    // 0 1 2 3 4
        //    // Left unit preservation: Unit(source).Multiply() == f.
        //    Unit(source).Multiply().WriteLines(); // 0 1 2 3 4
        //                                          // Right unit preservation: source == source.Select(Unit).Multiply().
        //    source.Select(Unit).Multiply().WriteLines(); // 0 1 2 3 4
        //}

        internal static void Workflow<T1, T2, T3, T4>(
            Func<IEnumerable<T1>> source1,
            Func<IEnumerable<T2>> source2,
            Func<IEnumerable<T3>> source3,
            Func<T1, T2, T3, IEnumerable<T4>> source4)
        {
            IEnumerable<T4> query = from value1 in source1()
                from value2 in source2()
                from value3 in source3()
                from value4 in source4(value1, value2, value3)
                select value4; // Define query.
            query.WriteLines(); // Execute query.
        }

        internal static void CompiledWorkflow<T1, T2, T3, T4>(
            Func<IEnumerable<T1>> source1,
            Func<IEnumerable<T2>> source2,
            Func<IEnumerable<T3>> source3,
            Func<T1, T2, T3, IEnumerable<T4>> source4)
        {
            IEnumerable<T4> query = source1()
                .SelectMany(value1 => source2(), (value1, value2) => new {Value1 = value1, Value2 = value2})
                .SelectMany(result2 => source3(), (result2, value3) => new {Result2 = result2, Value3 = value3})
                .SelectMany(
                    result3 => source4(result3.Result2.Value1, result3.Result2.Value2, result3.Value3),
                    (result3, value4) => value4); // Define query.
            query.WriteLines(); // Execute query.
        }

        internal static void MonadLaws()
        {
            IEnumerable<int> source = new int[] {0, 1, 2, 3, 4};
            Func<int, IEnumerable<char>> selector = int32 => new string('*', int32);
            Func<int, IEnumerable<double>> selector1 = int32 => new double[] {int32 / 2D, Math.Sqrt(int32)};
            Func<double, IEnumerable<string>> selector2 =
                @double => new string[] {@double.ToString("0.0"), @double.ToString("0.00")};
            const int Value = 5;

            // Associativity: source.SelectMany(selector1).SelectMany(selector2) == source.SelectMany(value => selector1(value).SelectMany(selector2)).
            (from value in source
                from result1 in selector1(value)
                from result2 in selector2(result1)
                select result2).WriteLines();
            // 0.0 0.00 0.0 0.00
            // 0.5 0.50 1.0 1.00
            // 1.0 1.00 1.4 1.41
            // 1.5 1.50 1.7 1.73
            // 2.0 2.00 2.0 2.00
            (from value in source
                from result in (from result1 in selector1(value)
                    from result2 in selector2(result1)
                    select result2)
                select result).WriteLines();
            // 0.0 0.00 0.0 0.00
            // 0.5 0.50 1.0 1.00
            // 1.0 1.00 1.4 1.41
            // 1.5 1.50 1.7 1.73
            // 2.0 2.00 2.0 2.00
            // Left unit: value.Wrap().SelectMany(selector) == selector(value).
            (from value in Value.Enumerable()
                from result in selector(value)
                select result).WriteLines(); // * * * * *
            selector(Value).WriteLines(); // * * * * *
            // Right unit: source == source.SelectMany(Wrap).
            (from value in source
                from result in value.Enumerable()
                select result).WriteLines(); // 0 1 2 3 4
        }
    }
}