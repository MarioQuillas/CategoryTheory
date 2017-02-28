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
}