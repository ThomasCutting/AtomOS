/*
* PROJECT:          Atomix Development
* LICENSE:          BSD 3-Clause (LICENSE.md)
* PURPOSE:          VFS Directory
* PROGRAMMERS:      Aman Priyadarshi (aman.eureka@gmail.com)
*/

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
