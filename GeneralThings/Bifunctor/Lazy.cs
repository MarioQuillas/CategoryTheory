using System;

namespace GeneralThings.Bifunctor
{
    public class Lazy<T1, T2>
    {
        private readonly Lazy<Tuple<T1, T2>> lazy;

        public Lazy(Func<Tuple<T1, T2>> factory)
        {
            this.lazy = new Lazy<Tuple<T1, T2>>(factory);
        }

        public T1 Value1 => this.lazy.Value.Item1;

        public T2 Value2 => this.lazy.Value.Item2;

        public override string ToString() => this.lazy.Value.ToString();
    }
}