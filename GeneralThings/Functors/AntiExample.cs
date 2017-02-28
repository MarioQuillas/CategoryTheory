using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralThings.Functors
{
    class AntiExample
    {
    }

    public static Lazy<TResult> Select<TSource, TResult>(
    this Lazy<TSource> source, Func<TSource, TResult> selector) =>
        new Lazy<TResult>(() => default(TResult));

    internal static void FunctorLaws()
    {
        Lazy<int> lazy = new Lazy<int>(() => 1);
        Func<int, string> selector1 = Convert.ToString;
        Func<string, double> selector2 = Convert.ToDouble;

        // Associativity preservation: TFunctor<T>.Select(f2.o(f1)) == TFunctor<T>.Select(f1).Select(f2)
        lazy.Select(selector2.o(selector1)).Value.WriteLine(); // 0
        lazy.Select(selector1).Select(selector2).Value.WriteLine(); // 0
                                                                    // Identity preservation: TFunctor<T>.Select(Id) == Id(TFunctor<T>)
        lazy.Select(Id).Value.WriteLine(); // 0
        Id(lazy).Value.WriteLine(); // 1
    }
}
