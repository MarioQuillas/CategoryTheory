namespace GeneralThings.Monoids
{
    /// <summary>
    /// here Dixin's blog is consider monoids are sets or more likely as objects (an endofunctor is an object in the category of endofunctors,
    /// it is a monad if it is a monoid in that category).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMonoid<T>
    {
        T Multiply(T value1, T value2);

        T Unit();
    }
}