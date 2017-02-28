using System.Collections.Generic;

namespace GeneralThings
{
    public interface ICategory<TObject, TMorphism>
    {
        IEnumerable<TObject> Objects { get; }

        TMorphism Compose(TMorphism morphism2, TMorphism morphism1);

        /// <summary>
        /// This is actually kind of a natural transformation. For any object, it will give us the identity map related to that object.
        /// </summary>
        /// <param name="object"></param>
        /// <returns></returns>
        TMorphism Id(TObject @object);
    }
}