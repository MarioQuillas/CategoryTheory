using System;
using System.Collections.Generic;
using System.Text;
using GeneralThings.Monoids;

namespace GeneralThings.MoreMonads
{
    public abstract class WriterBase<TContent, T>
    {
        private readonly Lazy<Tuple<TContent, T>> lazy;

        protected WriterBase(Func<Tuple<TContent, T>> writer, IMonoid<TContent> monoid)
        {
            this.lazy = new Lazy<Tuple<TContent, T>>(writer);
            this.Monoid = monoid;
        }

        public TContent Content => this.lazy.Value.Item1;

        public T Value => this.lazy.Value.Item2;

        public IMonoid<TContent> Monoid { get; }
    }

    public class Writer<TEntry, T> : WriterBase<IEnumerable<TEntry>, T>
    {
        private static readonly IMonoid<IEnumerable<TEntry>> ContentMonoid =
            new EnumerableConcatMonoid<TEntry>();

        public Writer(Func<Tuple<IEnumerable<TEntry>, T>> writer) : base(writer, ContentMonoid)
        {
        }

        public Writer(T value) : base(() => new Tuple<IEnumerable<TEntry>, T>(ContentMonoid.Unit(), value), ContentMonoid)
        {
        }
    }
}
