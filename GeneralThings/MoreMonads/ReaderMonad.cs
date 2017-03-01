using System.Collections.Generic;
using System.Text;

namespace GeneralThings.MoreMonads
{
    public static class ReaderMonad
    {
        // Reader: TEnvironment -> T
        public delegate T Reader<in TEnvironment, out T>(TEnvironment environment);
    }
}
