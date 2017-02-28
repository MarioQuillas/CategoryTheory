namespace GeneralThings.Monoids
{
    public class BooleanOrMonoid : IMonoid<bool>
    {
        public bool Multiply(bool value1, bool value2) => value1 || value2;

        public bool Unit() => false;
    }
}