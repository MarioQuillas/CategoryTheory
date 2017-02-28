using System;

namespace GeneralThings.Functors
{
    public static partial class LazyExtensions // Lazy<T> : IFunctor<Lazy<>>
    {
        // Functor Select: (TSource -> TResult) -> (Lazy<TSource> -> Lazy<TResult>)
        public static Func<Lazy<TSource>, Lazy<TResult>> Select<TSource, TResult>(
            Func<TSource, TResult> selector) => source =>
            Select(source, selector);

        // LINQ Select: (Lazy<TSource>, TSource -> TResult) -> Lazy<TResult>
        public static Lazy<TResult> Select<TSource, TResult>(
            this Lazy<TSource> source, Func<TSource, TResult> selector) =>
            new Lazy<TResult>(() => selector(source.Value));

        internal static void Map()
        {
            Lazy<int> source = new Lazy<int>(() => 1);
            // Map int to string.
            Func<int, string> selector = Convert.ToString;
            // Map Lazy<int> to Lazy<string>.
            Lazy<string> query = from value in source
                select selector(value); // Define query.
            string result = query.Value; // Execute query.
        }
    }
}