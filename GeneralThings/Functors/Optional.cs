using System;

namespace GeneralThings.Functors
{
    public struct Optional<T>
    {
        private readonly Lazy<Tuple<bool, T>> factory;

        public Optional(Func<Tuple<bool, T>> factory = null)
        {
            this.factory = factory == null ? null : new Lazy<Tuple<bool, T>>(factory);
        }

        public bool HasValue => this.factory?.Value.Item1 ?? false;

        public T Value
        {
            get
            {
                if (!this.HasValue)
                {
                    throw new InvalidOperationException($"{nameof(Optional<T>)} object must have a value.");
                }
                return this.factory.Value.Item2;
            }
        }

        //internal static void OptionalTest()
        //{
        //    int int32 = 1;
        //    Func<int, string> function = Convert.ToString;

        //    Nullable<int> nullableInt32 = new Nullable<int>(int32);
        //    Nullable<Func<int, string>> nullableFunction = new Nullable<Func<int, string>>(function); // Cannot be compiled.
        //    Nullable<string> nullableString = new Nullable<string>(); // Cannot be compiled.

        //    Optional<int> optionalInt32 = new Optional<int>(() => (true, int32));
        //    Optional<Func<int, string>> optionalFunction = new Optional<Func<int, string>>(() => true, function));
        //    Optional<string> optionalString = new Optional<string>(); // Equivalent to: new Optional<string>(() => false, default(string)));
        //}
    }
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
                    ? (true, selector(source.Value)) : (false, default(TResult)));

        internal static void Map()
        {
            Optional<int> source1 = new Optional<int>(() => (true, 1));
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