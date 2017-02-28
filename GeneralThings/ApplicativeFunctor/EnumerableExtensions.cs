using System;
using System.Collections.Generic;
using System.Linq;
using GeneralThings.Categories;
using GeneralThings.MonoidalFunctor;
using GeneralThings.Monoids;

namespace GeneralThings.ApplicativeFunctor
{
    public static partial class EnumerableExtensions // IEnumerable<T> : IApplicativeFunctor<IEnumerable<>>
    {
        // Apply: (IEnumerable<TSource -> TResult>, IEnumerable<TSource>) -> IEnumerable<TResult>
        public static IEnumerable<TResult> Apply<TSource, TResult>(
            this IEnumerable<Func<TSource, TResult>> selectorWrapper, IEnumerable<TSource> source)
        {
            foreach (Func<TSource, TResult> selector in selectorWrapper)
            {
                foreach (TSource value in source)
                {
                    yield return selector(value);
                }
            }
        }

        // Wrap: TSource -> IEnumerable<TSource>
        public static IEnumerable<TSource> Enumerable<TSource>(this TSource value)
        {
            yield return value;
        }

        //internal static void ApplicativeLaws()
        //{
        //    IEnumerable<int> source = new int[] { 0, 1, 2, 3, 4 };
        //    Func<int, double> selector = int32 => Math.Sqrt(int32);
        //    IEnumerable<Func<int, double>> selectorWrapper1 =
        //        new Func<int, double>[] { int32 => int32 / 2D, int32 => Math.Sqrt(int32) };
        //    IEnumerable<Func<double, string>> selectorWrapper2 =
        //        new Func<double, string>[] { @double => @double.ToString("0.0"), @double => @double.ToString("0.00") };
        //    Func<Func<double, string>, Func<Func<int, double>, Func<int, string>>> o =
        //        new Func<Func<double, string>, Func<int, double>, Func<int, string>>(Linq.FuncExtensions.o).Curry();
        //    int value = 5;

        //    // Functor preservation: source.Select(selector) == selector.Wrap().Apply(source).
        //    source.Select(selector).WriteLines(); // 0 1 1.4142135623731 1.73205080756888 2
        //    selector.Enumerable().Apply(source).WriteLines(); // 0 1 1.4142135623731 1.73205080756888 2
        //                                                      // Identity preservation: Id.Wrap().Apply(source) == source.
        //    new Func<int, int>(Functions.Id).Enumerable().Apply(source).WriteLines(); // 0 1 2 3 4
        //                                                                              // Composition preservation: o.Wrap().Apply(selectorWrapper2).Apply(selectorWrapper1).Apply(source) == selectorWrapper2.Apply(selectorWrapper1.Apply(source)).
        //    o.Enumerable().Apply(selectorWrapper2).Apply(selectorWrapper1).Apply(source).WriteLines();
        //    // 0.0  0.5  1.0  1.5  2.0
        //    // 0.0  1.0  1.4  1.7  2.0 
        //    // 0.00 0.50 1.00 1.50 2.00
        //    // 0.00 1.00 1.41 1.73 2.00
        //    selectorWrapper2.Apply(selectorWrapper1.Apply(source)).WriteLines();
        //    // 0.0  0.5  1.0  1.5  2.0
        //    // 0.0  1.0  1.4  1.7  2.0 
        //    // 0.00 0.50 1.00 1.50 2.00
        //    // 0.00 1.00 1.41 1.73 2.00
        //    // Homomorphism: selector.Wrap().Apply(value.Wrap()) == selector(value).Wrap().
        //    selector.Enumerable().Apply(value.Enumerable()).WriteLines(); // 2.23606797749979
        //    selector(value).Enumerable().WriteLines(); // 2.23606797749979
        //                                               // Interchange: selectorWrapper.Apply(value.Wrap()) == (selector => selector(value)).Wrap().Apply(selectorWrapper).
        //    selectorWrapper1.Apply(value.Enumerable()).WriteLines(); // 2.5 2.23606797749979
        //    new Func<Func<int, double>, double>(function => function(value)).Enumerable().Apply(selectorWrapper1)
        //        .WriteLines(); // 2.5 2.23606797749979
        //}
    }


    public static partial class EnumerableExtensions // IEnumerable<T> : IApplicativeFunctor<IEnumerable<>>
    {
        // Apply: (IEnumerable<TSource -> TResult>, IEnumerable<TSource>) -> IEnumerable<TResult>
        public static IEnumerable<TResult> Apply<TSource, TResult>(
            this IEnumerable<Func<TSource, TResult>> selectorWrapper, IEnumerable<TSource> source) =>
                selectorWrapper.Multiply(source).Select(product => product.Item1(product.Item2));

        // Wrap: TSource -> IEnumerable<TSource>
        public static IEnumerable<TSource> Enumerable<TSource>(this TSource value) => Unit().Select(unit => value);
    }

    public static partial class EnumerableExtensions // IEnumerable<T> : IMonoidalFunctor<IEnumerable<>>
    {
        // Multiply: IEnumerable<T1> x IEnumerable<T2> -> IEnumerable<T1 x T2>
        // Multiply: (IEnumerable<T1>, IEnumerable<T2>) -> IEnumerable<(T1, T2)>
        public static IEnumerable<(T1, T2)> Multiply<T1, T2>(
            this IEnumerable<T1> source1, IEnumerable<T2> source2) =>
                new Func<T1, T2, (T1, T2)>(ValueTuple.Create).Curry().Enumerable().Apply(source1).Apply(source2);

        // Unit: Unit -> IEnumerable<Unit>
        public static IEnumerable<Unit> Unit(Unit unit = default(Unit)) => unit.Enumerable();
    }

    // Cannot be compiled.
    public static class MonoidalFunctorExtensions // (Multiply, Unit) implements (Apply, Wrap).
    {
        // Apply: (TMonoidalFunctor<TSource -> TResult>, TMonoidalFunctor<TSource>) -> TMonoidalFunctor<TResult>
        public static TMonoidalFunctor<TResult> Apply<TMonoidalFunctor<>, TSource, TResult>(
    
            this TMonoidalFunctor<Func<TSource, TResult>> selectorWrapper, TMonoidalFunctor<TSource> source)
            where TMonoidalFunctor<> : IMonoidalFunctor<TMonoidalFunctor<>> =>
                selectorWrapper.Multiply(source).Select(product => product.Item1(product.Item2));

        // Wrap: TSource -> TMonoidalFunctor<TSource>
        public static TMonoidalFunctor<TSource> Wrap<TMonoidalFunctor<>, TSource>(this TSource value)
            where TMonoidalFunctor<> : IMonoidalFunctor<TMonoidalFunctor<>> => TMonoidalFunctor < TSource >
                TMonoidalFunctor<TSource>.Unit().Select(unit => value);
    }

    // Cannot be compiled.
    public static class ApplicativeFunctorExtensions // (Apply, Wrap) implements (Multiply, Unit).
    {
        // Multiply: TApplicativeFunctor<T1> x TApplicativeFunctor<T2> -> TApplicativeFunctor<T1 x T2>
        // Multiply: (TApplicativeFunctor<T1>, TApplicativeFunctor<T2>) -> TApplicativeFunctor<(T1, T2)>
        public static TApplicativeFunctor<(T1, T2)> Multiply<TApplicativeFunctor<>, T1, T2>(
    
            this TApplicativeFunctor<T1> source1, TApplicativeFunctor<T2> source2)
            where TApplicativeFunctor<> : IApplicativeFunctor<TApplicativeFunctor<>> =>
                new Func<T1, T2, (T1, T2)>(ValueTuple.Create).Curry().Wrap().Apply(source1).Apply(source2);

        // Unit: Unit -> TApplicativeFunctor<Unit>
        public static TApplicativeFunctor<Unit> Unit<TApplicativeFunctor<>>(Unit unit = default(Unit))
            where TApplicativeFunctor<> : IApplicativeFunctor<TApplicativeFunctor<>> => unit.Wrap();
    }
}