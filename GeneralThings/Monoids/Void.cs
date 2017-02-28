using System;
using System.Runtime.InteropServices;

namespace GeneralThings.Monoids
{
    /// <summary>
    /// to be able to construct a monoid with a single element
    /// </summary>
    [ComVisible(true)]
    //[Serializable()]
    [StructLayout(LayoutKind.Sequential, Size = 1)]
    public struct Void
    {
    }

    [CompilationMapping(SourceConstructFlags.ObjectType)]
    [Serializable]
    public sealed class Unit : IComparable
    {
        internal Unit()
        {
        }

        public override int GetHashCode() => 0;

        public override bool Equals(object obj) =>
            obj == null || LanguagePrimitives.IntrinsicFunctions.TypeTestGeneric<Unit>(obj);

        int IComparable.CompareTo(object obj) => 0;
    }
}