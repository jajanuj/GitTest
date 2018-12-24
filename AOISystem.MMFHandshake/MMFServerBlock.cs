using System;
using System.Runtime.InteropServices;

namespace AOISystem.MMFHandshake
{
    [Serializable]
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct MMFServerBlock
    {
        public bool ServerToClientRequest;

        public bool ClientToServerReply;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MMFDefine.BLOCK_FIELD_STRING_SIZE)]
        public string Message;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MMFDefine.BLOCK_FIELD_ARRAY_SIZE)]
        public byte[] Buffer;
    }
}
