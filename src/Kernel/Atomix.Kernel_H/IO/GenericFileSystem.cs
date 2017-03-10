/*
* PROJECT:          Atomix Development
* LICENSE:          BSD 3-Clause (LICENSE.md)
* PURPOSE:          Generic File System
* PROGRAMMERS:      Aman Priyadarshi (aman.eureka@gmail.com)
*/

using System;

namespace Atomix.Kernel_H.IO
{
    internal abstract class GenericFileSystem : Directory
    {
        bool mIsValid;

        internal bool IsValid
        { get { return mIsValid; } }

        internal GenericFileSystem(string aName)
            :base(aName)
        {

        }

        internal abstract bool Detect();
    }
}
