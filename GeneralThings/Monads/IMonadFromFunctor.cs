using System.Collections.Generic;
using System.Text;

namespace GeneralThings.Monads
{
    // Cannot be compiled.
    public partial interface IMonadFromFunctor<TMonad <>> : IFunctor<TMonad<>> where TMonad <> : IMonad<TMonad<>>
    {
        // From IFunctor<TMonad<>>:
        // Select: (TSource -> TResult) -> (TMonad<TSource> -> TMonad<TResult>)
        // Func<TMonad<TSource>, TMonad<TResult>> Select<TSource, TResult>(Func<TSource, TResult> selector);

        // Multiply: TMonad<TMonad<TSource>> -> TMonad<TSource>
        TMonad<TSource> Multiply<TSource>(TMonad<TMonad<TSource>> sourceWrapper);

        // Unit: TSource -> TMonad<TSource>
        TMonad<TSource> Unit<TSource>(TSource value);
    }
}
