using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralThings.MoreMonads
{
    public static partial class CpsExtensions
    {
        // Sqrt: int -> double
        internal static double Sqrt(int int32) => Math.Sqrt(int32);

        // SqrtWithCallback: (int, double -> TContinuation) -> TContinuation
        internal static TContinuation SqrtWithCallback<TContinuation>(
            int int32, Func<double, TContinuation> continuation) =>
                continuation(Math.Sqrt(int32));

        // SqrtWithCallback: int -> (double -> TContinuation) -> TContinuation
        internal static Func<Func<double, TContinuation>, TContinuation> SqrtWithCallback<TContinuation>(int int32) =>
            continuation => continuation(Math.Sqrt(int32));

        // Cps: (T -> TContinuation>) -> TContinuation
        public delegate TContinuation Cps<TContinuation, out T>(Func<T, TContinuation> continuation);

        // SqrtCps: int -> Cps<TContinuation, double>
        internal static Cps<TContinuation, double> SqrtCps<TContinuation>(int int32) =>
            continuation => continuation(Math.Sqrt(int32));

    }
}
