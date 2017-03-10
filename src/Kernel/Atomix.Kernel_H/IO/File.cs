/*
* PROJECT:          Atomix Development
* LICENSE:          BSD 3-Clause (LICENSE.md)
* PURPOSE:          VFS File
* PROGRAMMERS:      Aman Priyadarshi (aman.eureka@gmail.com)
*/

using System;

namespace Atomix.Kernel_H.IO
{
    internal abstract class File : FSObject
    {
        internal uint SizeInBytes;

        internal abstract Stream Open(FileMode aMode);

        internal File(string aName)
            :base(aName)
        {

        }
    }
}
