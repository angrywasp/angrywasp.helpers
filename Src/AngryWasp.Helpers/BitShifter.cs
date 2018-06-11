using System;

namespace AngryWasp.Helpers
{
    public static class BitShifter
    {
        #region ToByte

        public static byte[] ToByte(short value)
        {
            return new byte[]
            {
                (byte)value,
                (byte)(value >> 8)
            };
        }

        public static byte[] ToByte(ushort value)
        {
            return new byte[]
            {
                (byte)value,
                (byte)(value >> 8)
            };
        }

        public static byte[] ToByte(int value)
        {
            return new byte[]
            {
                (byte)value,
                (byte)(value >> 8),
                (byte)(value >> 16),
                (byte)(value >> 24)
            };
        }

        public static byte[] ToByte(uint value)
        {
            return new byte[]
            {
                (byte)value,
                (byte)(value >> 8),
                (byte)(value >> 16),
                (byte)(value >> 24)
            };
        }

        public static byte[] ToByte(long value)
        {
            return new byte[]
            {
                (byte)value,
                (byte)(value >> 8),
                (byte)(value >> 16),
                (byte)(value >> 24),
                (byte)(value >> 32),
                (byte)(value >> 40),
                (byte)(value >> 48),
                (byte)(value >> 56)
            };
        }

        public static byte[] ToByte(ulong value)
        {
            return new byte[]
            {
                (byte)value,
                (byte)(value >> 8),
                (byte)(value >> 16),
                (byte)(value >> 24),
                (byte)(value >> 32),
                (byte)(value >> 40),
                (byte)(value >> 48),
                (byte)(value >> 56)
            };
        }

        public static unsafe byte[] ToByte(float value)
        {
            uint num = *(uint*)(&value);
            return new byte[]
            {
                (byte)num,
                (byte)(num >> 8),
                (byte)(num >> 16),
                (byte)(num >> 24)
            };
        }

        public static unsafe byte[] ToByte(double value)
        {
            ulong num = *(ulong*)(&value);
            return new byte[]
            {
                (byte)num,
                (byte)(num >> 8),
                (byte)(num >> 16),
                (byte)(num >> 24),
                (byte)(num >> 32),
                (byte)(num >> 40),
                (byte)(num >> 48),
                (byte)(num >> 56),
            };
        }

        #endregion

        #region FromByte

        public static short ToShort(byte[] value)
        {
            return (short)((int)value[0] | (int)value[1] << 8);
        }

        public static short ToShort(byte[] value, ref int start)
        {
            return (short)((int)value[start++] | (int)value[start++] << 8);
        }

        public static ushort ToUShort(byte[] value)
        {
            return (ushort)((int)value[0] | (int)value[1] << 8);
        }

        public static ushort ToUShort(byte[] value, ref int start)
        {
            return (ushort)((int)value[start++] | (int)value[start++] << 8);
        }

        public static int ToInt(byte[] value)
        {
            return (int)value[0] | (int)value[1] << 8 | (int)value[2] << 16 | (int)value[3] << 24;
        }

        public static int ToInt(byte[] value, ref int start)
        {
            return (int)value[start++] | (int)value[start++] << 8 | (int)value[start++] << 16 | (int)value[start++] << 24;
        }

        public static uint ToUInt(byte[] value)
        {
            return (uint)((int)value[0] | (int)value[1] << 8 | (int)value[2] << 16 | (int)value[3] << 24);
        }

        public static uint ToUInt(byte[] value, ref int start)
        {
            return (uint)((int)value[start++] | (int)value[start++] << 8 | (int)value[start++] << 16 | (int)value[start++] << 24);
        }

        public static long ToLong(byte[] value)
        {
            uint num = (uint)((int)value[0] | (int)value[1] << 8 | (int)value[2] << 16 | (int)value[3] << 24);
            uint num2 = (uint)((int)value[4] | (int)value[5] << 8 | (int)value[6] << 16 | (int)value[7] << 24);
            return (long)((ulong)num2 << 32 | (ulong)num);
        }

        public static long ToLong(byte[] value, ref int start)
        {
            uint num = (uint)((int)value[start++] | (int)value[start++] << 8 | (int)value[start++] << 16 | (int)value[start++] << 24);
            uint num2 = (uint)((int)value[start++] | (int)value[start++] << 8 | (int)value[start++] << 16 | (int)value[start++] << 24);
            return (long)((ulong)num2 << 32 | (ulong)num);
        }

        public static ulong ToULong(byte[] value)
        {
            uint num = (uint)((int)value[0] | (int)value[1] << 8 | (int)value[2] << 16 | (int)value[3] << 24);
            uint num2 = (uint)((int)value[4] | (int)value[5] << 8 | (int)value[6] << 16 | (int)value[7] << 24);
            return (ulong)num2 << 32 | (ulong)num;
        }

        public static ulong ToULong(byte[] value, ref int start)
        {
            uint num = (uint)((int)value[start++] | (int)value[start++] << 8 | (int)value[start++] << 16 | (int)value[start++] << 24);
            uint num2 = (uint)((int)value[start++] | (int)value[start++] << 8 | (int)value[start++] << 16 | (int)value[start++] << 24);
            return (ulong)num2 << 32 | (ulong)num;
        }

        public static unsafe float ToFloat(byte[] value)
        {
            uint num = (uint)((int)value[0] | (int)value[1] << 8 | (int)value[2] << 16 | (int)value[3] << 24);
            return *(float*)(&num);
        }

        public static unsafe float ToFloat(byte[] value, ref int start)
        {
            uint num = (uint)((int)value[start++] | (int)value[start++] << 8 | (int)value[start++] << 16 | (int)value[start++] << 24);
            return *(float*)(&num);
        }

        public static unsafe double ToDouble(byte[] value)
        {
            uint num = (uint)((int)value[0] | (int)value[1] << 8 | (int)value[2] << 16 | (int)value[3] << 24);
            uint num2 = (uint)((int)value[4] | (int)value[5] << 8 | (int)value[6] << 16 | (int)value[7] << 24);
            ulong num3 = (ulong)num2 << 32 | (ulong)num;
            return *(double*)(&num3);
        }

        public static unsafe double ToDouble(byte[] value, ref int start)
        {
            uint num = (uint)((int)value[start++] | (int)value[start++] << 8 | (int)value[start++] << 16 | (int)value[start++] << 24);
            uint num2 = (uint)((int)value[start++] | (int)value[start++] << 8 | (int)value[start++] << 16 | (int)value[start++] << 24);
            ulong num3 = (ulong)num2 << 32 | (ulong)num;
            return *(double*)(&num3);
        }

        #endregion

        #region Bit manipulation

        public static bool GetBit(byte b, int index)
        {
            int bitIndex = index % 8;
            byte mask = (byte)(1 << bitIndex);

            return (b & mask) != 0;
        }

        public static bool[] GetBits(byte b)
        {
            bool[] ret = new bool[8];

            for (int i = 0; i < 8; i++)
                ret[i] = GetBit(b, i);

            return ret;
        }

        public static bool[] GetBits(byte[] b)
        {
            bool[] ret = new bool[b.Length * 8];

            int x = 0;
            for (int y = 0; y < b.Length; y++)
                for (int z = 0; z < 8; z++)
                    ret[x++] = GetBit(b[y], z);

            return ret;
        }

        public static void SetBit(ref byte b, int index, bool value)
        {
            int bitIndex = index % 8;
            byte mask = (byte)(1 << bitIndex);

            b = (byte)(value ? (b | mask) : (b & ~mask));
        }

        public static void SetBits(ref byte b, bool[] bits)
        {
            for (int i = 0; i < bits.Length; i++)
                SetBit(ref b, i, bits[i]);
        }

        public static byte ToggleBit(byte b, int index)
        {
            int bitIndex = index % 8;
            byte mask = (byte)(1 << bitIndex);

            return b ^= mask;
        }

        #endregion

        /// <remarks>Only works when copying smaller to larger arrays</remarks>
        public static void Copy(byte[] source, byte[] destination, int offset)
        {
            for (int i = 0; i < source.Length; i++)
                destination[i + offset] = source[i];
        }

        /// <remarks>Only works when copying smaller to larger arrays</remarks>
        public static void Copy(byte[] source, byte[] destination, ref int offset)
        {
            for (int i = 0; i < source.Length; i++)
                destination[i + offset] = source[i];

            offset += source.Length;
        }

        /// <remarks>Only works when copying smaller to larger arrays</remarks>
        public static void BlockCopy(byte[] source, byte[] destination, ref int offset)
        {
            Buffer.BlockCopy(source, 0, destination, offset, source.Length);
            offset += source.Length;
        }
    }
}