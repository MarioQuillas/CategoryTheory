using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GeneralThings.Functors;
using GeneralThings.Monoids;

namespace GeneralThings.ApplicativeFunctor
{
    public static partial class LazyExtensions // Lazy<T> : IMonoidalFunctor<Lazy<>>
    {
        // Multiply: Lazy<T1> x Lazy<T2> -> Lazy<T1 x T2>
        // Multiply: (Lazy<T1>, Lazy<T2>) -> Lazy<(T1, T2)>
        public static Lazy<(T1, T2)> Multiply<T1, T2>(this Lazy<T1> source1, Lazy<T2> source2) =>
            new Lazy<(T1, T2)>(() => (source1.Value, source2.Value));

        // Unit: Unit -> Lazy<Unit>
        public static Lazy<Unit> Unit(Unit unit = default(Unit)) => new Lazy<Unit>(() => unit);
    }

    public static partial class LazyExtensions // Lazy<T> : IApplicativeFunctor<Lazy<>>
    {
        // Apply: (Lazy<TSource -> TResult>, Lazy<TSource>) -> Lazy<TResult>
        public static Lazy<TResult> Apply<TSource, TResult>(
            this Lazy<Func<TSource, TResult>> selectorWrapper, Lazy<TSource> source) =>
                selectorWrapper.Multiply(source).Select(product => product.Item1(product.Item2));

        // Wrap: TSource -> Lazy<TSource>
        public static Lazy<T> Lazy<T>(this T value) => Unit().Select(unit => value);
    }

    public static partial class FuncExtensions // Func<T> : IMonoidalFunctor<Func<>>
    {
        // Multiply: Func<T1> x Func<T2> -> Func<T1 x T2>
        // Multiply: (Func<T1>, Func<T2>) -> Func<(T1, T2)>
        public static Func<(T1, T2)> Multiply<T1, T2>(this Func<T1> source1, Func<T2> source2) =>
            () => (source1(), source2());

        // Unit: Unit -> Func<Unit>
        public static Func<Unit> Unit(Unit unit = default(Unit)) => () => unit;
    }

    public static partial class FuncExtensions // Func<T> : IApplicativeFunctor<Func<>>
    {
        // Apply: (Func<TSource -> TResult>, Func<TSource>) -> Func<TResult>
        public static Func<TResult> Apply<TSource, TResult>(
            this Func<Func<TSource, TResult>> selectorWrapper, Func<TSource> source) =>
                selectorWrapper.Multiply(source).Select(product => product.Item1(product.Item2));

        // Wrap: TSource -> Func<TSource>
        public static Func<T> Func<T>(this T value) => Unit().Select(unit => value);
    }

    public static partial class FuncExtensions // Func<T, TResult> : IMonoidalFunctor<Func<T,>>
    {
        // Multiply: Func<T, T1> x Func<T, T2> -> Func<T, T1 x T2>
        // Multiply: (Func<T, T1>, Func<T, T2>) -> Func<T, (T1, T2)>
        public static Func<T, (T1, T2)> Multiply<T, T1, T2>(this Func<T, T1> source1, Func<T, T2> source2) =>
            value => (source1(value), source2(value));

        // Unit: Unit -> Func<T, Unit>
        public static Func<T, Unit> Unit<T>(Unit unit = default(Unit)) => _ => unit;
    }

    public static partial class FuncExtensions // Func<T, TResult> : IApplicativeFunctor<Func<T,>>
    {
        // Apply: (Func<T, TSource -> TResult>, Func<T, TSource>) -> Func<T, TResult>
        public static Func<T, TResult> Apply<T, TSource, TResult>(
            this Func<T, Func<TSource, TResult>> selectorWrapper, Func<T, TSource> source) =>
                selectorWrapper.Multiply(source).Select(product => product.Item1(product.Item2));

        // Wrap: TSource -> Func<T, TSource>
        public static Func<T, TSource> Func<T, TSource>(this TSource value) => Unit<T>().Select(unit => value);
    }

    public static partial class OptionalExtensions // Optional<T> : IMonoidalFunctor<Optional<>>
    {
        // Multiply: Optional<T1> x Optional<T2> -> Optional<T1 x T2>
        // Multiply: (Optional<T1>, Optional<T2>) -> Optional<(T1, T2)>
        public static Optional<(T1, T2)> Multiply<T1, T2>(this Optional<T1> source1, Optional<T2> source2) =>
            new Optional<(T1, T2)>(() => source1.HasValue && source2.HasValue
                ? (true, (source1.Value, source2.Value))
                : (false, (default(T1), default(T2))));

        // Unit: Unit -> Optional<Unit>
        public static Optional<Unit> Unit(Unit unit = default(Unit)) =>
            new Optional<Unit>(() => (true, unit));
    }

    public static partial class OptionalExtensions // Optional<T> : IApplicativeFunctor<Optional<>>
    {
        // Apply: (Optional<TSource -> TResult>, Optional<TSource>) -> Optional<TResult>
        public static Optional<TResult> Apply<TSource, TResult>(
            this Optional<Func<TSource, TResult>> selectorWrapper, Optional<TSource> source) =>
                selectorWrapper.Multiply(source).Select(product => product.Item1(product.Item2));

        // Wrap: TSource -> Optional<TSource>
        public static Optional<T> Optional<T>(this T value) => Unit().Select(unit => value);
    }

    public static partial class ValueTupleExtensions // ValueTuple<T> : IMonoidalFunctor<ValueTuple<>>
    {
        // Multiply: ValueTuple<T1> x ValueTuple<T2> -> ValueTuple<T1 x T2>
        // Multiply: (ValueTuple<T1>, ValueTuple<T2>) -> ValueTuple<(T1, T2)>
        public static ValueTuple<(T1, T2)> Multiply<T1, T2>(this ValueTuple<T1> source1, ValueTuple<T2> source2) =>
            new ValueTuple<(T1, T2)>((source1.Item1, source2.Item1)); // Immediate execution.

        // Unit: Unit -> ValueTuple<Unit>
        public static ValueTuple<Unit> Unit(Unit unit = default(Unit)) => new ValueTuple<Unit>(unit);
    }

    public static partial class ValueTupleExtensions // ValueTuple<T> : IApplicativeFunctor<ValueTuple<>>
    {
        // Apply: (ValueTuple<TSource -> TResult>, ValueTuple<TSource>) -> ValueTuple<TResult>
        public static ValueTuple<TResult> Apply<TSource, TResult>(
            this ValueTuple<Func<TSource, TResult>> selectorWrapper, ValueTuple<TSource> source) =>
                selectorWrapper.Multiply(source).Select(product => product.Item1(product.Item2)); // Immediate execution.

        // Wrap: TSource -> ValueTuple<TSource>
        public static ValueTuple<T> ValueTuple<T>(this T value) => Unit().Select(unit => value);
    }

    public static partial class TaskExtensions // Task<T> : IMonoidalFunctor<Task<>>
    {
        // Multiply: Task<T1> x Task<T2> -> Task<T1 x T2>
        // Multiply: (Task<T1>, Task<T2>) -> Task<(T1, T2)>
        public static async Task<(T1, T2)> Multiply<T1, T2>(this Task<T1> source1, Task<T2> source2) =>
            ((await source1), (await source2)); // Immediate execution, impure.

        // Unit: Unit -> Task<Unit>
        public static Task<Unit> Unit(Unit unit = default(Unit)) => System.Threading.Tasks.Task.FromResult(unit);
    }

    public static partial class TaskExtensions // Task<T> : IApplicativeFunctor<Task<>>
    {
        // Apply: (Task<TSource -> TResult>, Task<TSource>) -> Task<TResult>
        public static Task<TResult> Apply<TSource, TResult>(
            this Task<Func<TSource, TResult>> selectorWrapper, Task<TSource> source) =>
                selectorWrapper.Multiply(source).Select(product => product.Item1(product.Item2)); // Immediate execution, impure.

        // Wrap: TSource -> Task<TSource>
        public static Task<T> Task<T>(this T value) => Unit().Select(unit => value);
    }
}
