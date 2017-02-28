using System;
using System.Collections.Generic;
using System.Text;
using GeneralThings.Functors;

namespace GeneralThings.MonoidalFunctor
{
    /// <summary>
    /// . In the definition, (C, ⊗, IC) and (D, ⊛, ID) are both (DotNet, ValueTuple&lt;,&gt;, Unit), 
    /// so monoidal functor can be IEnumerable&lt;T1&gt;, IEnumerable&lt;T2&gt;defined as:
    /// </summary>
    /// <typeparam name="TMonoidalFunctor"></typeparam>
    public interface IMonoidalFunctor<TMonoidalFunctor <>> : IFunctor<TMonoidalFunctor<>>
        where TMonoidalFunctor : IMonoidalFunctor<TMonoidalFunctor<>>
    {
        // From IFunctor<TMonoidalFunctor<>>:
        // Select: (TSource -> TResult) -> (TMonoidalFunctor<TSource> -> TMonoidalFunctor<TResult>)
        // Func<TMonoidalFunctor<TSource>, TMonoidalFunctor<TResult>> Select<TSource, TResult>(Func<TSource, TResult> selector);

        // Multiply: TMonoidalFunctor<T1> x TMonoidalFunctor<T2> -> TMonoidalFunctor<T1 x T2>
        // Multiply: ValueTuple<TMonoidalFunctor<T1>, TMonoidalFunctor<T2>> -> TMonoidalFunctor<ValueTuple<T1, T2>>
        TMonoidalFunctor<Tuple<T1, T2>> Multiply<T1, T2>(
            Tuple<TMonoidalFunctor<T1>, TMonoidalFunctor<T2>> bifunctor);

        // Unit: Unit -> TMonoidalFunctor<Unit>
        TMonoidalFunctor<Unit> Unit(Unit unit);
    }

    public interface IMonoidalFunctor<TMonoidalFunctor <>> : IFunctor<TMonoidalFunctor<>>
        where TMonoidalFunctor : IMonoidalFunctor<TMonoidalFunctor<>>
    {
        // Multiply: TMonoidalFunctor<T1> x TMonoidalFunctor<T2> -> TMonoidalFunctor<T1 x T2>
        // Multiply: (TMonoidalFunctor<T1>, TMonoidalFunctor<T2>) -> TMonoidalFunctor<(T1, T2)>
        TMonoidalFunctor<(T1, T2)> Multiply<T1, T2>(
            TMonoidalFunctor<T1> source1, TMonoidalFunctor<T2> > source2); // Unit: Unit

// Unit: Unit -> TMonoidalFunctor<Unit>
        TMonoidalFunctor<Unit> Unit(Unit unit);
    }
}
