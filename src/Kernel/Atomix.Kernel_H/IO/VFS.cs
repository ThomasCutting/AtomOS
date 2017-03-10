using System;

using Atomix.Kernel_H.Lib;
using Atomix.Kernel_H.Core;

namespace Atomix.Kernel_H.IO
{
    internal class VFS : Directory
    {
        IList<FSObject> mEntries;

        internal VFS(string aName)
            :base(aName)
        {
            mEntries = new IList<FSObject>();
        }

        static VFS mRoot;
        internal static void Install()
        {
            mRoot = new VFS(string.Empty);
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

        internal static bool Mount(FSObject aObject, string aPath)
        {
            var paths = Marshal.Split(aPath, '/');

            Directory root = mRoot;
            int count = paths.Length;
            for (int i = 0; i < count; i++)
            {
                FSObject temp = root.Read(paths[i]);
                if (!(temp is Directory))
                    return false;
                root = (Directory)temp;
            }

            bool status = root.Create(aObject);

            Heap.Free(paths);
            return status;
        }
    }
}
