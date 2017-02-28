using System;

namespace GeneralThings.Bifunctor
{
    public static partial class ValueTupleExtensions // ValueTuple<T1, T2> : IBifunctor<ValueTuple<,>>
    {
        // Bifunctor Select: (TSource1 -> TResult1, TSource2 -> TResult2) -> (ValueTuple<TSource1, TSource2> -> ValueTuple<TResult1, TResult2>).
        public static Func<Tuple<TSource1, TSource2>, Tuple<TResult1, TResult2>> Select<TSource1, TSource2, TResult1, TResult2>(
            Func<TSource1, TResult1> selector1, Func<TSource2, TResult2> selector2) => source =>
            Select(source, selector1, selector2);

        // LINQ-like Select: (ValueTuple<TSource1, TSource2>, TSource1 -> TResult1, TSource2 -> TResult2) -> ValueTuple<TResult1, TResult2>).
        public static Tuple<TResult1, TResult2> Select<TSource1, TSource2, TResult1, TResult2>(
            this Tuple<TSource1, TSource2> source,
            Func<TSource1, TResult1> selector1,
            Func<TSource2, TResult2> selector2) =>
            new Tuple<TResult1, TResult2>(selector1(source.Item1), selector2(source.Item2));
    }
}