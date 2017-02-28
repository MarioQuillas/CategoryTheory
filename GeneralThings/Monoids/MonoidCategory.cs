using System;
using System.Collections.Generic;
using GeneralThings.Categories;

namespace GeneralThings.Monoids
{
    /// <summary>
    /// I think there is an error here, since T is TMorphism type.
    /// No there is no error, the morphismes are actually the values of the set M.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MonoidCategory<T> : ICategory<Type, T>
    {
        private readonly IMonoid<T> monoid;

        public MonoidCategory(IMonoid<T> monoid)
        {
            this.monoid = monoid;
        }

        public IEnumerable<Type> Objects { get { yield return typeof(T); } }

        public T Compose(T morphism2, T morphism1) => this.monoid.Multiply(morphism1, morphism2);

        public T Id(Type @object) => this.monoid.Unit();
    }
}