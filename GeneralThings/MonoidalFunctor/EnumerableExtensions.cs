using System;
using System.Collections.Generic;
using System.Linq;
using GeneralThings.Monoids;

namespace GeneralThings.MonoidalFunctor
{
    public static partial class EnumerableExtensions // IEnumerable<T> : IMonoidalFunctor<IEnumerable<>>
    {
        // Multiply: IEnumerable<T1> x IEnumerable<T2> -> IEnumerable<T1 x T2>
        // Multiply: ValueTuple<IEnumerable<T1>, IEnumerable<T2>> -> IEnumerable<ValueTuple<T1, T2>>
        // Multiply: (IEnumerable<T1>, IEnumerable<T2>) -> IEnumerable<(T1, T2)>
        public static IEnumerable<Tuple<T1, T2>> Multiply<T1, T2>(
            this IEnumerable<T1> source1, IEnumerable<T2> source2) // Implicit tuple.
        {
            foreach (T1 value1 in source1)
            {
                foreach (T2 value2 in source2)
                {
                    yield return new Tuple<T1, T2>(value1, value2);
                }
            }
        }

        // Unit: Unit -> IEnumerable<Unit>
        public static IEnumerable<Unit> Unit(Unit unit = default(Unit))
        {
            yield return unit;
        }

        //// using static Dixin.Linq.CategoryTheory.DotNetCategory;
        //internal static void MonoidalFunctorLaws()
        //{
        //    IEnumerable<Unit> unit = Unit();
        //    IEnumerable<int> source1 = new int[] { 0, 1 };
        //    IEnumerable<char> source2 = new char[] { '@', '#' };
        //    IEnumerable<bool> source3 = new bool[] { true, false };
        //    IEnumerable<int> source = new int[] { 0, 1, 2, 3, 4 };

        //    // Associativity preservation: source1.Multiply(source2).Multiply(source3).Select(Associator) == source1.Multiply(source2.Multiply(source3)).
        //    source1.Multiply(source2).Multiply(source3).Select(Associator).WriteLines();
        //    // (0, (@, True)) (0, (@, False)) (0, (#, True)) (0, (#, False))
        //    // (1, (@, True)) (1, (@, False)) (1, (#, True)) (1, (#, False))
        //    source1.Multiply(source2.Multiply(source3)).WriteLines();
        //    // (0, (@, True)) (0, (@, False)) (0, (#, True)) (0, (#, False))
        //    // (1, (@, True)) (1, (@, False)) (1, (#, True)) (1, (#, False))
        //    // Left unit preservation: unit.Multiply(source).Select(LeftUnitor) == source.
        //    unit.Multiply(source).Select(LeftUnitor).WriteLines(); // 0 1 2 3 4
        //                                                           // Right unit preservation: source == source.Multiply(unit).Select(RightUnitor).
        //    source.Multiply(unit).Select(RightUnitor).WriteLines(); // 0 1 2 3 4
        //}

        internal static void Selector1Arity(IEnumerable<int> xs)
        {
            Func<int, bool> selector = x => x > 0;
            // Apply selector with xs.
            IEnumerable<bool> applyWithXs = xs.Select(selector);
        }

        internal static void SelectorNArity(IEnumerable<int> xs, IEnumerable<long> ys, IEnumerable<double> zs)
        {
            Func<int, long, double, bool> selector = (x, y, z) => x + y + z > 0;

            // Curry selector.
            Func<int, Func<long, Func<double, bool>>> curriedSelector =
                selector.Curry(); // 1 arity: x => (y => z => x + y + z > 0)
                                  // Partially apply selector with xs.
            IEnumerable<Func<long, Func<double, bool>>> applyWithXs = xs.Select(curriedSelector);

            // Partially apply selector with ys.
            IEnumerable<(Func<long, Func<double, bool>>, long)> multiplyWithYs = applyWithXs.Multiply(ys);
            IEnumerable<Func<double, bool>> applyWithYs = multiplyWithYs.Select(product =>
            {
                Func<long, Func<double, bool>> partialAppliedSelector = product.Item1;
                long y = product.Item2;
                return partialAppliedSelector(y);
            });

            // Partially apply selector with zs.
            IEnumerable<(Func<double, bool>, double)> multiplyWithZs = applyWithYs.Multiply(zs);
            IEnumerable<bool> applyWithZs = multiplyWithZs.Select(product =>
            {
                Func<double, bool> partialAppliedSelector = product.Item1;
                double z = product.Item2;
                return partialAppliedSelector(z);
            });
        }

        // Apply: (IEnumerable<TSource -> TResult>, IEnumerable<TSource>) -> IEnumerable<TResult>
        public static IEnumerable<TResult> Apply<TSource, TResult>(
            this IEnumerable<Func<TSource, TResult>> selectorWrapper, IEnumerable<TSource> source) =>
                selectorWrapper.Multiply(source).Select(product => product.Item1(product.Item2));

        internal static void Apply(IEnumerable<int> xs, IEnumerable<long> ys, IEnumerable<double> zs)
        {
            Func<int, long, double, bool> selector = (x, y, z) => x + y + z > 0;
            // Partially apply selector with xs.
            IEnumerable<Func<long, Func<double, bool>>> applyWithXs = xs.Select(selector.Curry());
            // Partially apply selector with ys.
            IEnumerable<Func<double, bool>> applyWithYs = applyWithXs.Apply(ys);
            // Partially apply selector with zs.
            IEnumerable<bool> applyWithZs = applyWithYs.Apply(zs);
        }
    }
}