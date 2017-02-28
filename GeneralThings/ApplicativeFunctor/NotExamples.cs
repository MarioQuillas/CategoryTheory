using System;
using System.Collections.Generic;
using System.Text;
using GeneralThings.Monoids;

namespace GeneralThings.ApplicativeFunctor
{
    public static partial class ValueTupleExtensions // ValueTuple<T1, T2 : IMonoidalFunctor<ValueTuple<T,>>
    {
        // Multiply: ValueTuple<T, T1> x ValueTuple<T, T2> -> ValueTuple<T, T1 x T2>
        // Multiply: (ValueTuple<T, T1>, ValueTuple<T, T2>) -> ValueTuple<T, (T1, T2)>
        public static (T, (T1, T2)) Multiply<T, T1, T2>(this (T, T1) source1, (T, T2) source2) =>
            (source1.Item1, (source1.Item2, source2.Item2)); // Immediate execution.

        // Unit: Unit -> ValueTuple<Unit>
        public static (T, Unit) Unit<T>(Unit unit = default(Unit)) => (default(T), unit);
    }

    public static partial class ValueTupleExtensions // ValueTuple<T, TResult> : IApplicativeFunctor<ValueTuple<T,>>
    {
        // Apply: (ValueTuple<T, TSource -> TResult>, ValueTuple<T, TSource>) -> ValueTuple<T, TResult>
        public static (T, TResult) Apply<T, TSource, TResult>(
            this (T, Func<TSource, TResult>) selectorWrapper, (T, TSource) source) =>
                selectorWrapper.Multiply(source).Select(product => product.Item1(product.Item2)); // Immediate execution.

        // Wrap: TSource -> ValueTuple<T, TSource>
        public static (T, TSource) ValueTuple<T, TSource>(this TSource value) => Unit<T>().Select(unit => value);
    }

    //internal static void MonoidalFunctorLaws()
    //{
    //    (string, int) source = ("a", 1);
    //    (string, Unit) unit = Unit<string>();
    //    (string, int) source1 = ("b", 2);
    //    (string, char) source2 = ("c", '@');
    //    (string, bool) source3 = ("d", true);

    //    // Associativity preservation: source1.Multiply(source2).Multiply(source3).Select(Associator) == source1.Multiply(source2.Multiply(source3)).
    //    source1.Multiply(source2).Multiply(source3).Select(Associator).WriteLine(); // (b, (2, (@, True)))
    //    source1.Multiply(source2.Multiply(source3)).WriteLine(); // (b, (2, (@, True)))
    //                                                             // Left unit preservation: unit.Multiply(source).Select(LeftUnitor) == source.
    //    unit.Multiply(source).Select(LeftUnitor).WriteLine(); // (, 1)
    //                                                          // Right unit preservation: source == source.Multiply(unit).Select(RightUnitor).
    //    source.Multiply(unit).Select(RightUnitor).WriteLine(); // (a, 1)
    //}

    //internal static void ApplicativeLaws()
    //{
    //    (string, int) source = ("a", 1);
    //    Func<int, double> selector = int32 => Math.Sqrt(int32);
    //    (string, Func<int, double>) selectorWrapper1 =
    //        ("b", new Func<int, double>(int32 => Math.Sqrt(int32)));
    //    (string, Func<double, string>) selectorWrapper2 =
    //        ("c", new Func<double, string>(@double => @double.ToString("0.00")));
    //    Func<Func<double, string>, Func<Func<int, double>, Func<int, string>>> o =
    //        new Func<Func<double, string>, Func<int, double>, Func<int, string>>(Linq.FuncExtensions.o).Curry();
    //    int value = 5;

    //    // Functor preservation: source.Select(selector) == selector.Wrap().Apply(source).
    //    source.Select(selector).WriteLine(); // (a, 1)
    //    selector.ValueTuple<string, Func<int, double>>().Apply(source).WriteLine(); // (, 1)
    //                                                                                // Identity preservation: Id.Wrap().Apply(source) == source.
    //    new Func<int, int>(Functions.Id).ValueTuple<string, Func<int, int>>().Apply(source).WriteLine(); // (, 1)
    //                                                                                                     // Composition preservation: o.Curry().Wrap().Apply(selectorWrapper2).Apply(selectorWrapper1).Apply(source) == selectorWrapper2.Apply(selectorWrapper1.Apply(source)).
    //    o.ValueTuple<string, Func<Func<double, string>, Func<Func<int, double>, Func<int, string>>>>()
    //        .Apply(selectorWrapper2).Apply(selectorWrapper1).Apply(source).WriteLine(); // (, 1.00)
    //    selectorWrapper2.Apply(selectorWrapper1.Apply(source)).WriteLine(); // (c, 1.00)
    //                                                                        // Homomorphism: selector.Wrap().Apply(value.Wrap()) == selector(value).Wrap().
    //    selector.ValueTuple<string, Func<int, double>>().Apply(value.ValueTuple<string, int>()).WriteLine(); // (, 2.23606797749979)
    //    selector(value).ValueTuple<string, double>().WriteLine(); // (, 2.23606797749979)
    //                                                              // Interchange: selectorWrapper.Apply(value.Wrap()) == (selector => selector(value)).Wrap().Apply(selectorWrapper).
    //    selectorWrapper1.Apply(value.ValueTuple<string, int>()).WriteLine(); // (b, 2.23606797749979)
    //    new Func<Func<int, double>, double>(function => function(value))
    //        .ValueTuple<string, Func<Func<int, double>, double>>().Apply(selectorWrapper1).WriteLine(); // (, 2.23606797749979)
    //}
}
