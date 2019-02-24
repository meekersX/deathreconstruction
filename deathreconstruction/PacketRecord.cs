using System.Collections.Generic;

namespace deathreconstruction
{
    class PacketRecord
    {
        public int index;
        public bool isSend;
        public uint tsSec;
        public uint tsUsec;
        public uint tsHigh;
        public uint tsLow;
        public string packetHeadersStr;
        public string packetTypeStr;
        public int optionalHeadersLen;
        public List<PacketOpcode> opcodes = new List<PacketOpcode>();
        public string extraInfo;

        public byte[] data;
        public List<BlobFrag> frags = new List<BlobFrag>();

        public uint Seq;
        public uint Queue;
        public uint Iteration;
        public uint ServerPort;
    }
}
