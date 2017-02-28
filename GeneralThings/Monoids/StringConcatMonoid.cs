namespace GeneralThings.Monoids
{
    public class StringConcatMonoid : IMonoid<string>
    {
        public string Multiply(string value1, string value2) => string.Concat(value1, value2);

        public string Unit() => string.Empty;
    }
}