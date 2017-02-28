using System;

namespace GeneralThings.Functors
{
    public static partial class OptionalExtensions // Optional<T> : IFunctor<Optional<>>
    {
        // Functor Select: (TSource -> TResult) -> (Optional<TSource> -> Optional<TResult>)
        public static Func<Optional<TSource>, Optional<TResult>> Select<TSource, TResult>(
            Func<TSource, TResult> selector) => source =>
            Select(source, selector);

        // LINQ Select: (Optional<TSource>, TSource -> TResult) -> Optional<TResult>
        public static Optional<TResult> Select<TSource, TResult>(
            this Optional<TSource> source, Func<TSource, TResult> selector) =>
            new Optional<TResult>(() => source.HasValue
                ? new Tuple<bool, TResult>(true, selector(source.Value))
                : new Tuple<bool, TResult>(false, default(TResult)));

        internal static void Map()
        {
            Optional<int> source1 = new Optional<int>(() => new Tuple<bool, int>(true, 1));
            // Map int to string.
            Func<int, string> selector = Convert.ToString;
            // Map Optional<int> to Optional<string>.
            Optional<string> query1 = from value in source1
                select selector(value); // Define query.
            if (query1.HasValue) // Execute query.
            {
                string result1 = query1.Value;
            }

            Optional<int> source2 = new Optional<int>();
            // Map Optional<int> to Optional<string>.
            Optional<string> query2 = from value in source2
                select selector(value); // Define query.
            if (query2.HasValue) // Execute query.
            {
                string result2 = query2.Value;
            }
        }
    }
}