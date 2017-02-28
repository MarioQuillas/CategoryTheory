using System;

namespace GeneralThings.Bifunctor
{
    public static partial class LazyExtensions // Lazy<T1, T2> : IBifunctor<Lazy<,>>
    {
        // Bifunctor Select: (TSource1 -> TResult1, TSource2 -> TResult2) -> (Lazy<TSource1, TSource2> -> Lazy<TResult1, TResult2>).
        public static Func<Lazy<TSource1, TSource2>, Lazy<TResult1, TResult2>> Select<TSource1, TSource2, TResult1, TResult2>(
            Func<TSource1, TResult1> selector1, Func<TSource2, TResult2> selector2) => source =>
            Select(source, selector1, selector2);

        // LINQ-like Select: (Lazy<TSource1, TSource2>, TSource1 -> TResult1, TSource2 -> TResult2) -> Lazy<TResult1, TResult2>).
        public static Lazy<TResult1, TResult2> Select<TSource1, TSource2, TResult1, TResult2>(
            this Lazy<TSource1, TSource2> source,
            Func<TSource1, TResult1> selector1,
            Func<TSource2, TResult2> selector2) =>
            new Lazy<TResult1, TResult2>(() => new Tuple<TResult1, TResult2>(selector1(source.Value1), selector2(source.Value2)));
    }
}