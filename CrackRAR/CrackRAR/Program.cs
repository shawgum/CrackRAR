using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static CrackRAR.UnRAR;

namespace CrackRAR
{
    class Program
    {
        static void Main(string[] args)
        {

            UnRAR.RAROpenArchiveDataEx openData = new UnRAR.RAROpenArchiveDataEx
            {
                ArcName = null,
                ArcNameW = "password1234.rar" + '\0',
                OpenMode = 0,
                Callback = null,
                UserData = new IntPtr(123)
            };

            var handle = RAROpenArchiveEx(ref openData);

            Console.ReadKey();
        }
    }
}
