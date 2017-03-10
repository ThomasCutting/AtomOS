/*
* PROJECT:          Atomix Development
* LICENSE:          BSD 3-Clause (LICENSE.md)
* PURPOSE:          Virtual File Node
* PROGRAMMERS:      Aman Priyadarshi (aman.eureka@gmail.com)
*/

using System;

using Atomix.Kernel_H.Lib;

namespace Atomix.Kernel_H.IO
{
    internal class VFN : Directory
    {
        IList<FSObject> mEntries;

        internal VFN(string aName)
            : base(aName)
        {
            mEntries = new IList<FSObject>();
        }

        internal override FSObject Read(string aName)
        {
            IList<FSObject> list = mEntries;
            int count = list.Count;
            for (int index = 0; index < count; index++)
                if (list[index].Name == aName)
                    return list[index];
            return null;
        }

        internal override bool Create(FSObject aObject)
        {
            mEntries.Add(aObject);
            return true;
        }
    }
}
