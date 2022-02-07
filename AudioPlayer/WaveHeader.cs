using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer
{
    internal class WaveHeader
    {
        public byte[] riff;
        public uint fileSize;
        public byte[] wav;
        public byte[] fmt;
        public uint fmtSize;
        public ushort format;
        public ushort channels;
        public uint samplingRate;
        public uint dataRate;
        public ushort dataBlockSize;
        public ushort bitsPerSample;
        public byte[] data;
        public uint dataSize;

        public override string ToString()
        {
            string channelsNumber;
            if (channels == 1)
                channelsNumber = "mono";
            else
                channelsNumber = "stereo";

            return "size of file in bytes: " + fileSize + "\n" +
                "channel: " + channelsNumber + "\n" +
                "sample rate: " + samplingRate + "\n" +
                "bytes per sample : " + bitsPerSample / 8 + "\n" +
                "data size in bytes " + dataSize + "\n";
        }
    }
}
