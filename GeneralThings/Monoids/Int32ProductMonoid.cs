namespace GeneralThings.Monoids
{
    public class Int32ProductMonoid : IMonoid<int>
    {
        public int Multiply(int value1, int value2) => value1 * value2;

        public int Unit() => 1;
    }
}