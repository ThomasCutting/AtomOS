using System;

namespace Atomix.Kernel_H.IO
{
    internal enum FileMode : int
    {
        Read = 1,
        Write = 2,
        Append = 4
    };
}
