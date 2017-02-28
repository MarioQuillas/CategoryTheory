using System;
using GeneralThings.Categories;
using GeneralThings.Functors;

namespace GeneralThings.NaturalTransformations
{
    public static partial class NaturalTransformations
    {
        // ToLazy: Func<> -> Lazy<>
        public static Lazy<T> ToLazy<T>(this Func<T> function) => new Lazy<T>(function);

        //internal static void Naturality()
        //{
        //    Func<int, string> selector = int32 => Math.Sqrt(int32).ToString("0.00");

        //    // Naturality square:
        //    // ToFunc<string>.o(LazyExtensions.Select(selector)) == FuncExtensions.Select(selector).o(ToFunc<int>)
        //    Func<Func<string>, Lazy<string>> funcStringToLazyString = ToLazy<string>;
        //    Func<Func<int>, Func<string>> funcInt32ToFuncString = FuncExtensions.Select(selector);
        //    Func<Func<int>, Lazy<string>> leftComposition = funcStringToLazyString.o(funcInt32ToFuncString);
        //    Func<Lazy<int>, Lazy<string>> lazyInt32ToLazyString = LazyExtensions.Select(selector);
        //    Func<Func<int>, Lazy<int>> funcInt32ToLazyInt32 = ToLazy<int>;
        //    Func<Func<int>, Lazy<string>> rightComposition = lazyInt32ToLazyString.o(funcInt32ToLazyInt32);

        //    Func<int> funcInt32 = () => 2;
        //    Lazy<string> lazyString = leftComposition(funcInt32);
        //    lazyString.Value.WriteLine(); // 1.41
        //    lazyString = rightComposition(funcInt32);
        //    lazyString.Value.WriteLine(); // 1.41
        //}

        // ToFunc: Lazy<T> -> Func<T>
        public static Func<T> ToFunc<T>(this Lazy<T> lazy) => () => lazy.Value;

        // ToEnumerable: Func<T> -> IEnumerable<T>
        public static System.Collections.Generic.IEnumerable<T> ToEnumerable<T>(this Func<T> function)
        {
            yield return function();
        }

        // ToEnumerable: Lazy<T> -> IEnumerable<T>
        public static System.Collections.Generic.IEnumerable<T> ToEnumerable<T>(this Lazy<T> lazy)
        {
            yield return lazy.Value;
        }

        // ToOptional: Func<T> -> Optional<T>
        public static Optional<T> ToOptional<T>(this Func<T> function) =>
            new Optional<T>(() => new Tuple<bool, T>(true, function()));

        // ToOptional: Lazy<T> -> Optional<T>
        public static Optional<T> ToOptional<T>(this Lazy<T> lazy) =>
            // new Func<Func<T>, Optional<T>>(ToOptional).o(new Func<Lazy<T>, Func<T>>(ToFunc))(lazy);
            lazy.ToFunc().ToOptional();
    }
}