using System;
using GeneralThings.Monoids;

namespace GeneralThings.KleisliComposition
{
    static partial class GeneralExample
    {
        // Cannot be compiled.

        // Cannot be compiled.

        public static partial class MonadExtensions // (SelectMany, Wrap) implements (Apply, Wrap).
        {
            // Apply: (TMonad<TSource -> TResult>, TMonad<TSource>) -> TMonad<TResult>
            public static TMonad<TResult> Apply<TMonad<>, TSource, TResult>(
        
            this TMonad<Func<TSource, TResult>> selectorWrapper,
                TMonad<TSource> source) where TMonad<> : IMonad<TMonad<>> =>
            from selector in selectorWrapper
                from value in source
                select selector(value);
            // selectorWrapper.SelectMany(selector => source, (selector, value) => selector(value));

            // Monad's Wrap is identical to applicative functor's Wrap.
        }
    }

    static partial class GeneralExample
    {
        public static partial class MonadExtensions // (SelectMany, Wrap) implements (Multiply, Unit).
        {
            // Multiply: (TMonad<T1>, TMonad<T2>) => TMonad<(T1, T2)>
            public static TMonad<(T1, T2)> Multiply<TMonad<>, T1, T2>(
        
            this TMonad<T1> source1, TMonad<T2> source2) where TMonad<> : IMonad<TMonad<>> =>
            from value1 in source1
                from value2 in source2
                select (value1, value2);
            // source1.SelectMany(value1 => source2 (value1, value2) => value1.ValueTuple(value2));

            // Unit: Unit -> TMonad<Unit>
            public static TMonad<Unit> Unit<TMonad<>>(
                Unit unit = default(Unit)) where TMonad<> : IMonad<TMonad<>> => unit.Wrap();
        }
    }

    // Cannot be compiled.
    public static partial class MonadExtensions // (SelectMany, Wrap) implements (Multiply, Unit).
    {
        // Multiply: (TMonad<T1>, TMonad<T2>) => TMonad<(T1, T2)>
        public static TMonad<(T1, T2)> Multiply<TMonad<>, T1, T2>(
    
            this TMonad<T1> source1, TMonad<T2> source2) where TMonad<> : IMonad<TMonad<>> =>
                from value1 in source1
                from value2 in source2
                select (value1, value2);
        // source1.SelectMany(value1 => source2 (value1, value2) => value1.ValueTuple(value2));

        // Unit: Unit -> TMonad<Unit>
        public static TMonad<Unit> Unit<TMonad<>>(
            Unit unit = default(Unit)) where TMonad<> : IMonad<TMonad<>> => unit.Wrap();
    }

    // Cannot be compiled.
    public static partial class MonadExtensions // (SelectMany, Wrap) implements (Apply, Wrap).
    {
        // Apply: (TMonad<TSource -> TResult>, TMonad<TSource>) -> TMonad<TResult>
        public static TMonad<TResult> Apply<TMonad<>, TSource, TResult>(
    
            this TMonad<Func<TSource, TResult>> selectorWrapper,
            TMonad<TSource> source) where TMonad<> : IMonad<TMonad<>> =>
                from selector in selectorWrapper
                from value in source
                select selector(value);
        // selectorWrapper.SelectMany(selector => source, (selector, value) => selector(value));

        // Monad's Wrap is identical to applicative functor's Wrap.
    }

    // Cannot be compiled.
    public static class MonadExtensions // Monad (Multiply, Unit) implements monoidal functor (Multiply, Unit).
    {
        // Multiply: (TMonad<T1>, TMonad<T2>) => TMonad<(T1, T2)>
        public static TMonad<(T1, T2)> Multiply<TMonad<>, T1, T2>(
    
            this TMonad<T1> source1, TMonad<T2> source2) where TMonad<> : IMonad<TMonad<>> =>
                (from value1 in source1
                 select (from value2 in source2
                         select (value1, value2))).Multiply();
        // source1.Select(value1 => source2.Select(value2 => (value1, value2))).Multiply();

        // Unit: Unit -> TMonad<Unit>
        public static TMonad<Unit> Unit<TMonad>(Unit unit = default(Unit)) where TMonad<>: IMonad<TMonad<>> =>
            TMonad<Unit>.Unit<Unit>(unit);
    }

    // Cannot be compiled.
    public static partial class MonadExtensions // Monad (Multiply, Unit) implements applicative functor (Apply, Wrap).
    {
        // Apply: (TMonad<TSource -> TResult>, TMonad<TSource>) -> TMonad<TResult>
        public static TMonad<TResult> Apply<TMonad<>, TSource, TResult>(
    
            this TMonad<Func<TSource, TResult>> selectorWrapper,
            TMonad<TSource> source) where TMonad<> : IMonad<TMonad<>> =>
                (from selector in selectorWrapper
                 select (from value in source
                         select selector(value))).Multiply();
        // selectorWrapper.Select(selector => source.Select(value => selector(value))).Multiply();

        // Wrap: TSource -> TMonad<TSource>
        public static TMonad<TSource> Wrap<TMonad<>, TSource>(
    
            this TSource value) where TMonad<>: IMonad<TMonad<>> => TMonad<TSource>.Unit<TSource>(value);
    }

    // Cannot be compiled.
    public partial interface IMonad<TMonad<>> : IMonoidalFunctor<TMonad<>>, IApplicativeFunctor<TMonad<>>
{
}
}