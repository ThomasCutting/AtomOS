using System;

namespace Atomix.Kernel_H.IO
{
    internal abstract class FSObject
    {
        internal readonly string Name;

        internal FSObject(string aName)
        {
            Name = aName;
        }
    }
}
