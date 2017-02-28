namespace GeneralThings.Monoids
{
    public class Int32SumMonoid : IMonoid<int>
    {
        public int Multiply(int value1, int value2) => value1 + value2;

        public int Unit() => 0;
    }
}