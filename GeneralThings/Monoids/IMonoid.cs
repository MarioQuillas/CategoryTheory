namespace GeneralThings.Monoids
{
    /// <summary>
    /// here Dixin's blog is consider monoids are sets.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMonoid<T>
    {
        T Multiply(T value1, T value2);

        T Unit();
    }
}