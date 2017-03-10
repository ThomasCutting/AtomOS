using System;

namespace Atomix.Kernel_H.IO
{
    internal abstract class Directory : FSObject
    {
        internal Directory(string aName)
            :base(aName)
        {

        }

        internal abstract FSObject Read(string aName);
        internal abstract bool Create(FSObject aObject);
    }
}
