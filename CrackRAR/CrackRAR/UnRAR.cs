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
            RAR_OM_LIST,
            RAR_OM_EXTRACT,
            RAR_OM_LIST_INCSPELIT
        }

        public enum OpenResult : uint
        {
            SUCCESS = 0,
            ERAR_NO_MEMORY,
            ERAR_BAD_DATA,
            ERAR_UNKNOWN_FORMAT,
            ERAR_EOPEN,
            ERAR_BAD_PASSWORD
        }

        [DllImport("UnRAR64.dll")]
        public static extern IntPtr RAROpenArchiveEx(ref RAROpenArchiveDataEx archiveData);

        [DllImport("UnRAR64.dll")]
        public static extern int RARCloseArchive(IntPtr hArcData);

        [DllImport("UnRAR64.dll")]
        public static extern int RARReadHeaderEx(IntPtr hArcData, RARHeaderDataEx HeaderData);

        [DllImport("UnRAR64.dll")]
        public static extern int RARProcessFileW(IntPtr hArcData, int Operation, string DestPath, string DestName);

        //[DllImport("UnRAR64.dll")]
        //public static extern int CallbackProc(uint msg, IntPtr UserData, IntPtr P1, IntPtr P2);

        public delegate int UnRARCallBack(uint msg, IntPtr UserData, IntPtr P1, IntPtr P2);

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
