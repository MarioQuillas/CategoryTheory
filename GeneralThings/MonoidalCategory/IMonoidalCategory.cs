using GeneralThings.Categories;
using GeneralThings.Monoids;

namespace GeneralThings.MonoidalCategory
{
    public interface IMonoidalCategory<TObject, TMorphism> : ICategory<TObject, TMorphism>, IMonoid<TObject>
    {
    }
}