using System;

namespace GeneralThings.Monads
{
    public partial interface IMonadWithSelectMany<TMonad> where TMonad<> : IMonad<TMonad<>>
    {
        // SelectMany: (TMonad<TSource>, TSource -> TMonad<TSelector>, (TSource, TSelector) -> TResult) -> TMonad<TResult>
        TMonad<TResult> SelectMany<TSource, TSelector, TResult>(
            TMonad<TSource> source,
            Func<TSource, TMonad<TSelector>> selector,
            Func<TSource, TSelector, TResult> resultSelector);

        // Wrap: TSource -> IEnumerable<TSource>
        TMonad<TSource> Wrap<TSource>(TSource value);
    }
}