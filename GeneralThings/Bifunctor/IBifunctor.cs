using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralThings.Bifunctor
{
    // Cannot be compiled.
    // In DotNet category, bifunctors are binary endofunctors, and can be defined as:
    public interface IBifunctor<TBifunctor<,>> where TBifunctor<,> : IBifunctor<TBifunctor<,>>
    {
        Func<TBifunctor<TSource1, TSource2>, TBifunctor<TResult1, TResult2>> Select<TSource1, TSource2, TResult1, TResult2>(
            Func<TSource1, TResult1> selector1, Func<TSource2, TResult2> selector2);
    }
}
