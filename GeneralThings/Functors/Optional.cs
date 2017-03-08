using System;

namespace GeneralThings.Functors
{
    public struct Optional<T>
    {
        private readonly Lazy<Tuple<bool, T>> factory;

        public Optional(Func<Tuple<bool, T>> factory = null)
        {
            this.factory = factory == null ? null : new Lazy<Tuple<bool, T>>(factory);
        }

        public bool HasValue => this.factory?.Value.Item1 ?? false;

        public T Value
        {
            get
            {
                //Tuple<bool, string> tt = null;// new Tuple<bool, string>(true, "qdf");
                //var toto = tt?.Item1 ;//.Value.Item1;
                //var ttt = this.factory?.Value;

                //int? rrr ;//= 2;
                //var qsdf = rrr ?.ToString() ;

                //string? ns = "qsd";
                //string s;

                //Tuple<bool, bool>? t ;//= new Tuple<bool, bool>(true, false);
                //var tto = t?.Item1;


                if (!this.HasValue)
                {
                    throw new InvalidOperationException($"{nameof(Optional<T>)} object must have a value.");
                }
                return this.factory.Value.Item2;
            }
        }

        //internal static void OptionalTest()
        //{
        //    int int32 = 1;
        //    Func<int, string> function = Convert.ToString;

        //    Nullable<int> nullableInt32 = new Nullable<int>(int32);
        //    Nullable<Func<int, string>> nullableFunction = new Nullable<Func<int, string>>(function); // Cannot be compiled.
        //    Nullable<string> nullableString = new Nullable<string>(); // Cannot be compiled.

        //    Optional<int> optionalInt32 = new Optional<int>(() => (true, int32));
        //    Optional<Func<int, string>> optionalFunction = new Optional<Func<int, string>>(() => true, function));
        //    Optional<string> optionalString = new Optional<string>(); // Equivalent to: new Optional<string>(() => false, default(string)));
        //}
    }
}