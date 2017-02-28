using System;

namespace GeneralThings.Functors
{
    public static partial class NullableExtensions // Nullable<T> : IFunctor<Nullable<>>
    {
        // Functor Select: (TSource -> TResult) -> (Nullable<TSource> -> Nullable<TResult>)
        public static Func<TSource?, TResult?> Select2<TSource, TResult>(
            Func<TSource, TResult> selector) where TSource : struct where TResult : struct => source =>
            Select(source, selector); // Immediate execution.

        // LINQ Select: (Nullable<TSource>, TSource -> TResult) -> Nullable<TResult>
        public static TResult? Select<TSource, TResult>(
            this TSource? source, Func<TSource, TResult> selector) where TSource : struct where TResult : struct =>
            source.HasValue ? selector(source.Value) : default(TResult?); // Immediate execution.

        internal static void Map()
        {
            long? source1 = 1L;
            // Map int to string.
            Func<long, TimeSpan> selector = TimeSpan.FromTicks;
            // Map Nullable<int> to Nullable<TimeSpan>.
            TimeSpan? query1 = from value in source1
                select selector(value); // Define and execute query.
            TimeSpan result1 = query1.Value; // Query result.

            long? source2 = null;
            // Map Nullable<int> to Nullable<TimeSpan>.
            TimeSpan? query2 = from value in source2
                select selector(value); // Define and execute query.
            bool result2 = query2.HasValue; // Query result.
        }
    }
}