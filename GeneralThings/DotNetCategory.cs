using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GeneralThings
{
    //public partial class DotNetCategory : ICategory<Type, Delegate>
    //{
    //    public IEnumerable<Type> Objects =>
    //        SelfAndReferences(typeof(DotNetCategory).GetTypeInfo().Assembly)
    //            .SelectMany(assembly => assembly.GetExportedTypes());

    //    public Delegate Compose(Delegate morphism2, Delegate morphism1) =>
    //        // return (Func<TSource, TResult>)Functions.Compose<TSource, TMiddle, TResult>(
    //        //    (Func<TMiddle, TResult>)morphism2, (Func<TSource, TMiddle>)morphism1);
    //        (Delegate)typeof(Linq.FuncExtensions).GetTypeInfo().GetMethod(nameof(Linq.FuncExtensions.o))
    //            .MakeGenericMethod( // TSource, TMiddle, TResult.
    //                morphism1.GetMethodInfo().GetParameters().Single().ParameterType,
    //                morphism1.GetMethodInfo().ReturnType,
    //                morphism2.GetMethodInfo().ReturnType)
    //            .Invoke(null, new object[] { morphism2, morphism1 });

    //    public Delegate Id(Type @object) => // Functions.Id<TSource>
    //        typeof(Functions).GetTypeInfo().GetMethod(nameof(Functions.Id)).MakeGenericMethod(@object)
    //            .CreateDelegate(typeof(Func<,>).MakeGenericType(@object, @object));

    //    private static IEnumerable<Assembly> SelfAndReferences(
    //        Assembly self, HashSet<Assembly> selfAndReferences = null)
    //    {
    //        selfAndReferences = selfAndReferences ?? new HashSet<Assembly>();
    //        if (selfAndReferences.Add(self))
    //        {
    //            self.GetReferencedAssemblies().ForEach(reference =>
    //                SelfAndReferences(Assembly.Load(reference), selfAndReferences));
    //            return selfAndReferences;
    //        }
    //        return Enumerable.Empty<Assembly>(); // Circular or duplicate reference.
    //    }
    //}
}