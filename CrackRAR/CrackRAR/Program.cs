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
                ArcNameW = @"C:\Users\shoa\Desktop\nopassword.rar",
                OpenMode = (uint)OpenMode.RAR_OM_EXTRACT,
                Callback = CrackPassword,
                UserData = Marshal.StringToHGlobalUni("123")
            };

            var handle = RAROpenArchiveEx(ref openData);
            OpenResult result = (OpenResult) RARProcessFileW(handle, (int) Operation.RAR_EXTRACT, null, null);

            Console.ReadKey();
        }
    }
}
