using System;
using System.IO;
using System.Text;

namespace GeneralThings.MoreMonads
{
    public static partial class CpsExtensions
    {
        // SelectMany: (Cps<TContinuation, TSource>, TSource -> Cps<TContinuation, TSelector>, (TSource, TSelector) -> TResult) -> Cps<TContinuation, TResult>
        public static Cps<TContinuation, TResult> SelectMany<TContinuation, TSource, TSelector, TResult>(
            this Cps<TContinuation, TSource> source,
            Func<TSource, Cps<TContinuation, TSelector>> selector,
            Func<TSource, TSelector, TResult> resultSelector) =>
            continuation => source(value =>
                selector(value)(result =>
                    continuation(resultSelector(value, result))));

        // Wrap: TSource -> Cps<TContinuation, TSource>
        public static Cps<TContinuation, TSource> Cps<TContinuation, TSource>(this TSource value) =>
            continuation => continuation(value);

        // Select: (Cps<TContinuation, TSource>, TSource -> TResult) -> Cps<TContinuation, TResult>
        public static Cps<TContinuation, TResult> Select<TContinuation, TSource, TResult>(
            this Cps<TContinuation, TSource> source, Func<TSource, TResult> selector) =>
            source.SelectMany(value => selector(value).Cps<TContinuation, TResult>(), (value, result) => result);

        // Equivalent to:
        // continuation => source(value => continuation(selector(value)));
        // Or:
        // continuation => source(continuation.o(selector));

        // SquareCps: int -> Cps<TContinuation, int>
        internal static Cps<TContinuation, int> SquareCps<TContinuation>(int x) =>
            continuation => continuation(x * x);

        // SumCps: (int, int) -> Cps<TContinuation, int>
        internal static Cps<TContinuation, int> SumCps<TContinuation>(int x, int y) =>
            continuation => continuation(x + y);

        // SumOfSquaresCps: (int, int) -> Cps<TContinuation, int>
        internal static Cps<TContinuation, int> SumOfSquaresCps<TContinuation>(int a, int b) =>
            continuation =>
                SquareCps<TContinuation>(a)(squareOfA =>
                    SquareCps<TContinuation>(b)(squareOfB =>
                        SumCps<TContinuation>(squareOfA, squareOfB)(continuation)));

        internal static Cps<TContinuation, int> SumOfSquaresCpsLinq<TContinuation>(int a, int b) =>
            from squareOfA in SquareCps<TContinuation>(a)
            // Cps<TContinuation, int>.
            from squareOfB in SquareCps<TContinuation>(b)
            // Cps<TContinuation, int>.
            from sum in SumCps<TContinuation>(squareOfA, squareOfB)
            // Cps<TContinuation, int>.
            select sum;

        internal static Cps<TContinuation, uint> FibonacciCps<TContinuation>(uint uInt32) =>
            uInt32 > 1
                ? (from a in FibonacciCps<TContinuation>(uInt32 - 1U)
                    from b in FibonacciCps<TContinuation>(uInt32 - 2U)
                    select a + b)
                : uInt32.Cps<TContinuation, uint>();

        // Equivalent to:
        // continuation => uInt32 > 1U
        //    ? continuation(FibonacciCps<int>(uInt32 - 1U)(Id) + FibonacciCps<int>(uInt32 - 2U)(Id))
        //    : continuation(uInt32);


        public static Cps<TContinuation, T> Cps<TContinuation, T>(Func<T> function) =>
            continuation => continuation(function());

        internal static void Workflow<TContinuation>(Func<string, TContinuation> continuation)
        {
            Cps<TContinuation, string> query =
                from filePath in Cps<TContinuation, string>(Console.ReadLine)
                // Cps<TContinuation, string>.
                from encodingName in Cps<TContinuation, string>(Console.ReadLine)
                // Cps<TContinuation, string>.
                from encoding in Cps<TContinuation, Encoding>(() => Encoding.GetEncoding(encodingName))
                // Cps<TContinuation, Encoding>.
                from fileContent in Cps<TContinuation, string>(() => File.ReadAllText(filePath, encoding))
                // Cps<TContinuation, string>.
                select fileContent; // Define query.
            TContinuation result = query(continuation); // Execute query.
        }
    }
}