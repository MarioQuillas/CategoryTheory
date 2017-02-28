using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneralThings.KleisliComposition
{
    static partial class GeneralExample
    {
        public static Func<TSource, IEnumerable<TResult>> o<TSource, TMiddle, TResult>( // After.
            this Func<TMiddle, IEnumerable<TResult>> selector2,
            Func<TSource, IEnumerable<TMiddle>> selector1) =>
            value => selector1(value).SelectMany(selector2, (result1, result2) => result2);

        // Equivalent to:
        //// value => selector1(value).Select(selector2).Multiply();
        //internal static void KleisliComposition()
        //{
        //    Func<bool, IEnumerable<int>> selector1 =
        //        boolean => boolean ? new int[] { 0, 1, 2, 3, 4 } : new int[] { 5, 6, 7, 8, 9 };
        //    Func<int, IEnumerable<double>> selector2 = int32 => new double[] { int32 / 2D, Math.Sqrt(int32) };
        //    Func<double, IEnumerable<string>> selector3 =
        //        @double => new string[] { @double.ToString("0.0"), @double.ToString("0.00") };

        //    // Associativity: selector3.o(selector2).o(selector1) == selector3.o(selector2.o(selector1)).
        //    selector3.o(selector2).o(selector1)(true).WriteLines();
        //    // 0.0 0.00 0.0 0.00
        //    // 0.5 0.50 1.0 1.00
        //    // 1.0 1.00 1.4 1.41
        //    // 1.5 1.50 1.7 1.73
        //    // 2.0 2.00 2.0 2.00
        //    selector3.o(selector2.o(selector1))(true).WriteLines();
        //    // 0.0 0.00 0.0 0.00
        //    // 0.5 0.50 1.0 1.00
        //    // 1.0 1.00 1.4 1.41
        //    // 1.5 1.50 1.7 1.73
        //    // 2.0 2.00 2.0 2.00
        //    // Left unit: Unit.o(selector) == selector.
        //    Func<int, IEnumerable<int>> leftUnit = Enumerable;
        //    leftUnit.o(selector1)(true).WriteLines(); // 0 1 2 3 4
        //    selector1(true).WriteLines(); // 0 1 2 3 4
        //                                  // Right unit: selector == selector.o(Unit).
        //    selector1(false).WriteLines(); // 5 6 7 8 9
        //    Func<bool, IEnumerable<bool>> rightUnit = Enumerable;
        //    selector1.o(rightUnit)(false).WriteLines(); // 5 6 7 8 9
        //}

        // Cannot be compiled.
        //internal static void Workflow<TMonad<>, T1, T2, T3, T4, TResult>( // Non generic TMonad can work too.
        //    Func<TMonad<T1>> operation1,
        //    Func<TMonad<T2>> operation2,
        //    Func<TMonad<T3>> operation3,
        //    Func<TMonad<T4>> operation4,
        //    Func<T1, T2, T3, T4, TResult> resultSelector) where TMonad<> : IMonad<TMonad<>>
        //{
        //    TMonad<TResult> query = from /* T1 */ value1 in /* TMonad<T1> */ operation1()
        //                            from /* T2 */ value2 in /* TMonad<T1> */ operation2()
        //                            from /* T3 */ value3 in /* TMonad<T1> */ operation3()
        //                            from /* T4 */ value4 in /* TMonad<T1> */ operation4()
        //                            select /* TResult */ resultSelector(value1, value2, value3, value4); // Define query.
        //}


    }
}
