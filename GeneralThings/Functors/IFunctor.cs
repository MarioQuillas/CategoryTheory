using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GeneralThings.Functors
{
    /// <summary>
    /// Unfortunately, the behind interface cannot be compiled, because C#/.NET do not support higher-kinded polymorphism for types.
    /// 
    /// Kind is the meta type of a type (*)
    /// 
    /// C#/.NET doesn't suppport for type constructors (kind) such as IEnumerable<.> (* -> *)
    /// 
    /// ValueTuple&lt;,&gt; can accept 2 types of kind * (like string and bool), and return another closed type of kind * 
    /// (like ValueTuple&lt;string, bool&gt;) so ValueTuple&lt;,&gt; is a type constructor,
    ///  its kind is denoted (*, *) –&gt; *, or * –&gt; * –&gt; * in curried style.
    /// 
    /// In above IFunctor&lt;TFunctor&lt;&gt;&gt; generic type definition, its type parameter TFunctor&lt;&gt; 
    /// is an open generic type of kind * –&gt; *. As a result, IFunctor&lt;TFunctor&lt;&gt;&gt; 
    /// can be viewed as a type constructor, which works like a higher-order function,
    ///  accepting a TFunctor&lt;&gt; type constructor of kind * –&gt; *,
    ///  and returning a concrete type of kind *. 
    /// 
    /// So  IFunctor&lt;TFunctor&lt;&gt;&gt; is of kind (* –&gt; *) –&gt; *. 
    /// This is called a higher-kinded type, and not supported by .NET and C# compiler. 
    /// In another word, C# generic type definition does not support its type parameter to have type parameters. 
    /// 
    /// In C#, functor support is implemented by LINQ query comprehensions instead of type system.
    /// 
    /// IEnumerable<> is a built-in functor of DotNet category, 
    /// which can be viewed as virtually implementing above IFunctor<TFunctor<>>
    /// 
    /// </summary>
    /// <typeparam name="TFunctor"></typeparam>

    public interface IFunctor<TFunctor <>  > where TFunctor<> : IFunctor<TFunctor>
    {
        Func<TFunctor<TSource>, TFunctor<TResult>> Select<TSource, TResult>(Func<TSource, TResult> selector);
    }

    //public interface IEnumerable<T> : IFunctor<IEnumerable<>>, IEnumerable
    //{
    //    // Func<IEnumerable<TSource>, IEnumerable<TResult>> Select<TSource, TResult>(Func<TSource, TResult> selector);

    //    // Other members.
    //}


    ///  So its Select method is of type (TSource –&gt; TResult) –&gt; (IEnumerable&lt;TSource&gt; –&gt; IEnumerable&lt;TResult&gt;), 
    /// which can be uncurried to (TSource –&gt; TResult, IEnumerable&lt;TSource&gt;) –&gt; IEnumerable&lt;TResult&gt;:
    public interface IEnumerable<T> : IFunctor<IEnumerable<T>>, IEnumerable
    {
        // Func<IEnumerable<TSource>, IEnumerable<TResult>> Select<TSource, TResult>(Func<TSource, TResult> selector);
        // can be equivalently converted to:
        // IEnumerable<TResult> Select<TSource, TResult>(Func<TSource, TResult> selector, IEnumerable<TSource> source);

        // Other members.
    }

    /// <summary>
    /// Now swap the 2 parameters of the uncurried Select, 
    /// then its type becomes (IEnumerable&lt;TSource&gt;, TSource –&gt; TResult) –&gt; IEnumerable&lt;TResult&gt;:
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEnumerable<T> : IFunctor<IEnumerable<T>>, IEnumerable
    {
        // Func<IEnumerable<TSource>, IEnumerable<TResult>> Select<TSource, TResult>(Func<TSource, TResult> selector);
        // can be equivalently converted to:
        // IEnumerable<TResult> Select<TSource, TResult>(IEnumerable<TSource> source, Func<TSource, TResult> selector);

        // Other members.
    }

    /// <summary>
    /// In .NET, this equivalent version of Select is exactly the LINQ query method Select. The following is the comparison of functor Select method and LINQ Select method:
    /// </summary>
    public static partial class EnumerableExtensions // IEnumerable<T> : IFunctor<IEnumerable<>>
    {
        // Functor Select: (TSource -> TResult) -> (IEnumerable<TSource> -> IEnumerable<TResult>).
        public static Func<IEnumerable<TSource>, IEnumerable<TResult>> Select<TSource, TResult>(
            Func<TSource, TResult> selector) => source =>
                Select(source, selector);

        // 1. Uncurry to Select: (TSource -> TResult, IEnumerable<TSource>) -> IEnumerable<TResult>.
        // 2. Swap 2 parameters to Select: (IEnumerable<TSource>, TSource -> TResult) -> IEnumerable<TResult>.
        // 3. Define as LINQ extension method.
        public static IEnumerable<TResult> Select<TSource, TResult>(
            this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            foreach (TSource value in source)
            {
                yield return selector(value);
            }
        }
    }

    //And the above Select implementation satisfies the functor laws !!!!!!!!!!!!!!!!!!!!!!!!!


}
