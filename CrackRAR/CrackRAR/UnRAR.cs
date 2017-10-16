using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CrackRAR
{
    public class UnRAR
    {

        public enum OpenMode : uint
        {
            RAR_OM_LIST = 0,
            RAR_OM_EXTRACT = 1,
            RAR_OM_LIST_INCSPELIT = 2
        }

        public enum CallbackMessages : uint
        {
            UCM_CHANGEVOLUME,
            UCM_PROCESSDATA,
            UCM_NEEDPASSWORD,
            UCM_CHANGEVOLUMEW,
            UCM_NEEDPASSWORDW
        };

        //public enum RARError : uint
        //{

        //}

        public enum Operation : int
        {
            RAR_SKIP = 0,
            RAR_TEST = 1,
            RAR_EXTRACT = 2
        }

        public enum OpenResult : uint
        {
            SUCCESS = 0,
            ERAR_END_ARCHIVE = 10,
            ERAR_NO_MEMORY = 11,
            ERAR_BAD_DATA = 12,
            ERAR_BAD_ARCHIVE = 13,
            ERAR_UNKNOWN_FORMAT = 14,
            ERAR_EOPEN = 15,
            ERAR_ECREATE = 16,
            ERAR_ECLOSE = 17,
            ERAR_EREAD = 18,
            ERAR_EWRITE = 19,
            ERAR_SMALL_BUF = 20,
            ERAR_UNKNOWN = 21,
            ERAR_MISSING_PASSWORD = 22,
            ERAR_EREFERENCE = 23,
            ERAR_BAD_PASSWORD = 24
        }

        [DllImport("UnRAR64.dll")]
        public static extern IntPtr RAROpenArchiveEx(ref RAROpenArchiveDataEx archiveData);

        [DllImport("UnRAR64.dll")]
        public static extern int RARCloseArchive(IntPtr hArcData);

        [DllImport("UnRAR64.dll")]
        public static extern int RARReadHeaderEx(IntPtr hArcData, RARHeaderDataEx HeaderData);

        [DllImport("UnRAR64.dll")]
        public static extern int RARProcessFileW(IntPtr hArcData, int Operation, [MarshalAs(UnmanagedType.LPStr)] string DestPath, [MarshalAs(UnmanagedType.LPStr)] string DestName);

        //[DllImport("UnRAR64.dll")]
        //public static extern int CallbackProc(uint msg, IntPtr UserData, IntPtr P1, IntPtr P2);

        public delegate int UnRARCallBack(uint msg, IntPtr UserData, IntPtr P1, IntPtr P2);

        public static int CrackPassword(uint msg, IntPtr UserData, IntPtr P1, IntPtr P2)
        {
            Console.WriteLine("calling callback func");
            CallbackMessages callbackMsg = (CallbackMessages) msg;
            switch (callbackMsg)
            {
                case CallbackMessages.UCM_CHANGEVOLUME:
                    throw new NotImplementedException("Change volume not implemented.");
                    break;

                case CallbackMessages.UCM_CHANGEVOLUMEW:
                    throw new NotImplementedException("Change volume W not implemented.");
                    break;

                case CallbackMessages.UCM_PROCESSDATA:
                    Console.WriteLine("process data");
                    return 1;
                    break;


                case CallbackMessages.UCM_NEEDPASSWORD:
                case CallbackMessages.UCM_NEEDPASSWORDW:
                    Console.WriteLine("Need password");
                    P1 = UserData;
                    P2 = (IntPtr) Marshal.PtrToStringUni(P1).Length;
                    return 1;
                    break;

                default:
                    Console.WriteLine("default case");
                    break;
            }
            Console.WriteLine("End of callback function");
            return -1;
        }

        public struct RAROpenArchiveDataEx
        {
            public string ArcName;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string ArcNameW;
            public uint OpenMode;
            public uint OpenResult;
            public string CmtBuf;
            public uint CmtBufSize;
            public uint CmtSize;
            public uint CmtState;
            public uint Flags;
            public UnRARCallBack Callback;
            public IntPtr UserData;
            public uint[] Reserved;
        };

        public struct RARHeaderDataEx
        {
            string ArcName;
            string ArcNameW;
            char FileName;
            string FileNameW;
            uint Flags;
            uint PackSize;
            uint PackSizeHigh;
            uint UnpSize;
            uint UnpSizeHigh;
            uint HostOS;
            uint FileCRC;
            uint FileTime;
            uint UnpVer;
            uint Method;
            uint FileAttr;
            string CmtBuf;
            uint CmtBufSize;
            uint CmtSize;
            uint CmtState;
            uint DictSize;
            uint HashType;
            string Hash;
            uint RedirType;
            string RedirName;
            uint RedirNameSize;
            uint DirTarget;
            uint MtimeLow;
            uint MtimeHigh;
            uint CtimeLow;
            uint CtimeHigh;
            uint AtimeLow;
            uint AtimeHigh;
            uint[] Reserved;
        };
    }
}
