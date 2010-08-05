namespace Nammedia.Medboss.Utils
{
    using System;

    internal class BinHexConverter
    {
        public static byte[] Bin2HexText(byte[] bin, int start, int binlen)
        {
            byte[] buffer = new byte[binlen * 2];
            int index = start;
            for (int i = 0; index < binlen; i++)
            {
                buffer[i] = (byte) getHexText(bin[index] / 0x10);
                buffer[++i] = (byte) getHexText(bin[index] % 0x10);
                index++;
            }
            return buffer;
        }

        private static int getHexText(int i)
        {
            if ((i >= 0) && (i <= 9))
            {
                return (i + 0x30);
            }
            if ((i > 9) && (i < 0x10))
            {
                return ((i - 10) + 0x41);
            }
            return -1;
        }

        private static int getHexVal(int i)
        {
            if ((i >= 0x30) && (i <= 0x39))
            {
                return (i -= 0x30);
            }
            if ((i >= 0x61) && (i <= 0x66))
            {
                return (i -= 0x57);
            }
            if ((i >= 0x41) && (i <= 70))
            {
                return (i -= 0x37);
            }
            return -1;
        }

        public static byte[] HexText2Bin(byte[] hex, int start, int hexlen)
        {
            byte[] buffer = new byte[hexlen / 2];
            int index = start;
            for (int i = 0; index < hexlen; i++)
            {
                buffer[i] = (byte) ((getHexVal(hex[index]) * 0x10) + getHexVal(hex[index + 1]));
                index += 2;
            }
            return buffer;
        }
    }
}
