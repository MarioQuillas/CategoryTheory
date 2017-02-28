using System.Collections.Generic;
using System.Linq;

namespace GeneralThings.Monoids
{
    public class EnumerableConcatMonoid<T> : IMonoid<IEnumerable<T>>
    {
        public IEnumerable<T> Multiply(IEnumerable<T> value1, IEnumerable<T> value2) => value1.Concat(value2);

        public IEnumerable<T> Unit() => Enumerable.Empty<T>();
    }
}