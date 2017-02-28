using System;
using System.Threading.Tasks;

namespace GeneralThings.Functors
{
    /// <summary>
    /// ValueTuple<> with 1 type parameter simply wraps a value. 
    /// It is the eager version of Lazy<>, and it is also functor, with the following Select method:
    /// 
    /// Unlike all the previous Select, here ValueTuple&lt;&gt;’s Select query method cannot implement deferred execution. 
    /// To construct a ValueTuple&lt;TResult&gt; 
    /// instance and return, selector must be called immediately to evaluate the result value.
    /// </summary>
    //public static partial class ValueTupleExtensions // ValueTuple<T> : IFunctor<ValueTuple<>>
    //{
    //    // Functor Select: (TSource -> TResult) -> (ValueTuple<TSource> -> ValueTuple<TResult>)
    //    public static Func<ValueTuple<TSource>, ValueTuple<TResult>> Select<TSource, TResult>(
    //        Func<TSource, TResult> selector) => source =>
    //            Select(source, selector); // Immediate execution.

    //    // LINQ Select: (ValueTuple<TSource>, TSource -> TResult) -> ValueTuple<TResult>
    //    public static ValueTuple<TResult> Select<TSource, TResult>(
    //        this ValueTuple<TSource> source, Func<TSource, TResult> selector) =>
    //            new ValueTuple<TResult>(selector(source.Item1)); // Immediate execution.

    //    internal static void Map()
    //    {
    //        ValueTuple<int> source = new ValueTuple<int>(1);
    //        // Map int to string.
    //        Func<int, string> selector = int32 =>
    //        {
    //            $"{nameof(selector)} is called with {int32}.".WriteLine();
    //            return Convert.ToString(int32);
    //        };
    //        // Map ValueTuple<int> to ValueTuple<string>.
    //        ValueTuple<string> query = from value in source // Define and execute query.
    //                                   select selector(value); // selector is called with 1.
    //        string result = query.Item1; // Query result.
    //    }

    //    internal class ValueTuple<T> : Tuple<T, T>
    //    {
    //        public ValueTuple(T item1, T item2) : base(item1, item2)
    //        {
    //        }
    //    }
    //}

    public static partial class TaskExtensions // Task<T> : IFunctor<Task<>>
    {
        // Functor Select: (TSource -> TResult) -> (Task<TSource> -> Task<TResult>)
        public static Func<Task<TSource>, Task<TResult>> Select<TSource, TResult>(
            Func<TSource, TResult> selector) => source =>
            Select(source, selector); // Immediate execution, impure.

        // LINQ Select: (Task<TSource>, TSource -> TResult) -> Task<TResult>
        public static async Task<TResult> Select<TSource, TResult>(
            this Task<TSource> source, Func<TSource, TResult> selector) =>
            selector(await source); // Immediate execution, impure.

        internal static async Task MapAsync()
        {
            Task<int> source = System.Threading.Tasks.Task.FromResult(1);
            // Map int to string.
            Func<int, string> selector = Convert.ToString;
            // Map Task<int> to Task<string>.
            Task<string> query = from value in source
                select selector(value); // Define and execute query.
            string result = await query; // Query result.
        }
    }
}