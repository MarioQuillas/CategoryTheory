namespace GeneralThings.Monoids
{
    public class VoidMonoid : IMonoid<Void>
    {
        public Void Multiply(Void value1, Void value2) => default(Void);

        public Void Unit() => default(Void);
    }
}