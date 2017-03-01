using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralThings.MoreMonads
{
    public struct Try<T>
    {
        private readonly Lazy<(T, Exception)> factory;

        public Try(Func<(T, Exception)> factory) =>
            this.factory = new Lazy<(T, Exception)>(() =>
            {
                try
                {
                    return factory();
                }
                catch (Exception exception)
                {
                    return (default(T), exception);
                }
            });

        public T Value
        {
            get
            {
                if (this.HasException)
                {
                    throw new InvalidOperationException($"{nameof(Try<T>)} object must have a value.");
                }
                return this.factory.Value.Item1;
            }
        }

        public Exception Exception => this.factory.Value.Item2;

        public bool HasException => this.Exception != null;

        public static implicit operator Try<T>(T value) => new Try<T>(() => (value, (Exception)null));
    }

}
