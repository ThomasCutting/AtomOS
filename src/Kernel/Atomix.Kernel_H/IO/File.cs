using System;

namespace Atomix.Kernel_H.IO
{
    internal abstract class File : FSObject
    {
        internal uint Size;

        internal abstract Stream Open(FileMode aMode);

        internal File(string aName)
            :base(aName)
        {

        }
    }
}
