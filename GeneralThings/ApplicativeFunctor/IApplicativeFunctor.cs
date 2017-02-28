using System;
using System.Collections.Generic;
using System.Text;
using GeneralThings.Functors;

namespace GeneralThings.ApplicativeFunctor
{
    // Cannot be compiled.
    // A functor, with the above ability to apply functor-wrapped functions with functor-wrapped values, is also called applicative functor.
    // The following is the definition of applicative functor:
    public interface IApplicativeFunctor<TApplicativeFunctor <>> : IFunctor<TApplicativeFunctor<>>
        where TApplicativeFunctor <> : IApplicativeFunctor<TApplicativeFunctor<>>
    {
        // From: IFunctor<TApplicativeFunctor<>>:
        // Select: (TSource -> TResult) -> (TApplicativeFunctor<TSource> -> TApplicativeFunctor<TResult>)
        // Func<TApplicativeFunctor<TSource>, TApplicativeFunctor<TResult>> Select<TSource, TResult>(Func<TSource, TResult> selector);

        // Apply: (TApplicativeFunctor<TSource -> TResult>, TApplicativeFunctor<TSource> -> TApplicativeFunctor<TResult>
        TApplicativeFunctor<TResult> Apply<TSource, TResult>(
            TApplicativeFunctor<Func<TSource, TResult>> selectorWrapper, TApplicativeFunctor<TSource> source);

        // Wrap: TSource -> TApplicativeFunctor<TSource>
        TApplicativeFunctor<TSource> Wrap<TSource>(TSource value);
    }
}
