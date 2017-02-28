using System;
using GeneralThings.Categories;

namespace GeneralThings.Functors
{
    public static partial class FuncExtensions // Func<T> : IFunctor<Func<>>
    {
        // Functor Select: (TSource -> TResult) -> (Func<TSource> -> Func<TResult>)
        public static Func<Func<TSource>, Func<TResult>> Select<TSource, TResult>(
            Func<TSource, TResult> selector) => source =>
            Select(source, selector);

        // LINQ Select: (Func<TSource>, TSource -> TResult) -> Func<TResult>
        public static Func<TResult> Select<TSource, TResult>(
            this Func<TSource> source, Func<TSource, TResult> selector) =>
            () => selector(source());

        internal static void Map()
        {
            Func<int> source = () => 1;
            // Map int to string.
            Func<int, string> selector = Convert.ToString;
            // Map Func<int> to Func<string>.
            Func<string> query = from value in source
                select selector(value); // Define query.
            string result = query(); // Execute query.
        }


    }

    public static partial class FuncExtensions // Func<T, TResult> : IFunctor<Func<T,>>
    {
        // Functor Select: (TSource -> TResult) -> (Func<T, TSource> -> Func<T, TResult>)
        public static Func<Func<T, TSource>, Func<T, TResult>> Select<T, TSource, TResult>(
            Func<TSource, TResult> selector) => source =>
                Select(source, selector);

        // LINQ Select: (Func<T, TSource>, TSource -> TResult) -> Func<T, TResult>
        public static Func<T, TResult> Select<T, TSource, TResult>(
            this Func<T, TSource> source, Func<TSource, TResult> selector) =>
                value => selector(source(value)); // selector.o(source);

        internal static void Map<T>(T input)
        {
            Func<T, string> source = value => value.ToString();
            // Map string to bool.
            Func<string, bool> selector = string.IsNullOrWhiteSpace;
            // Map Func<T, string> to Func<T, bool>.
            Func<T, bool> query = from value in source
                                  select selector(value); // Define query.
            bool result = query(input); // Execute query.

            // Equivalent to:
            Func<T, string> function1 = source;
            Func<string, bool> function2 = selector;
            Func<T, bool> composition = function2.o(function1);
            result = composition(input);
        }

    }
}