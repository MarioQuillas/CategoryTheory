using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralThings.Monads
{
    public static partial class TaskExtensions // Task<T> : IMonad<Task<>>
    {
        // Multiply: Task<Task<T> -> Task<T>
        public static Task<TResult> Multiply<TResult>(this Task<Task<TResult>> sourceWrapper) =>
            sourceWrapper.SelectMany(source => source, (source, value) => value); // Immediate execution, impure.

        // Unit: TSource -> Task<TSource>
        public static Task<TSource> Unit<TSource>(TSource value) => Task(value);

        // SelectMany: (Task<TSource>, TSource -> Task<TSelector>, (TSource, TSelector) -> TResult) -> Task<TResult>
        public static async Task<TResult> SelectMany<TSource, TSelector, TResult>(
            this Task<TSource> source,
            Func<TSource, Task<TSelector>> selector,
            Func<TSource, TSelector, TResult> resultSelector) =>
                resultSelector(await source, await selector(await source)); // Immediate execution, impure.


        internal static async Task WorkflowAsync(string uri)
        {
            Task<string> query = from response in new HttpClient().GetAsync(uri) // Return Task<HttpResponseMessage>.
                                 from stream in response.Content.ReadAsStreamAsync() // Return Task<Stream>.
                                 from text in new StreamReader(stream).ReadToEndAsync() // Return Task<string>.
                                 select text; // Define and execute query.
            string result = await query; // Query result.
        }
    }
}
