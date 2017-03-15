using System;

namespace GeneralThings.KleisliComposition
{
    public static class FuncExtensions
    {
        //// Cannot be compiled.
        //public static Func<TSource, TMonad<TResult>> o<TMonad<>, TSource, TMiddle, TResult>( // After.

        //this Func<TMiddle, TMonad<TResult>> selector2,
        //    Func<TSource, TMonad<TMiddle>> selector1) where TMonad<> : IMonad<TMonad<>> =>
        //value => selector1(value).SelectMany(selector2, (result1, result2) => result2);
        //// Equivalent to:
        //// value => selector1(value).Select(selector2).Multiply();
    }

}