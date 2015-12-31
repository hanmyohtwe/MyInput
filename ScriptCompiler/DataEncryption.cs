using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace Data_Compression_and_Encryption
{
    class DataEncryption
    {
        private static byte[] ConvertToBytes(string text)
        {
            return System.Text.Encoding.Unicode.GetBytes(text);
        }
        private static string ConvertToText(byte[] ByteAarry)
        {
            return System.Text.Encoding.Unicode.GetString(ByteAarry);
        }
        private static BitArray ToBits(byte[] Bytes)
        {
            BitArray bits = new BitArray(Bytes);
            return bits;
        }
        private static BitArray SubBits(BitArray Bits, int Start, int Length)
        {
            BitArray half = new BitArray(Length);
            for (int i = 0; i < half.Length; i++)
                half[i] = Bits[i+Start];
            return half;
        }
        private static BitArray ConcateBits(BitArray LHH, BitArray RHH)
        {
            BitArray bits = new BitArray(LHH.Length + RHH.Length);
            for (int i = 0; i < LHH.Length; i++)
                bits[i] = LHH[i];
            for (int i = 0; i < RHH.Length; i++)
                bits[i+LHH.Length] = RHH[i];
            return bits;
        }
        
        public static byte[] Encrypt(byte[] ordinary)
        {
            BitArray bits = ToBits(ordinary);
            BitArray LHH=SubBits(bits,0,bits.Length/2);
            BitArray RHH = SubBits(bits, bits.Length / 2, bits.Length / 2);
            BitArray XorH = LHH.Xor(RHH);
            RHH = RHH.Not();
            XorH = XorH.Not();
            BitArray encr = ConcateBits(XorH, RHH);
            byte[] b = new byte[encr.Length/8];
            encr.CopyTo(b, 0);
            return b;
        }
        public static byte[] Decrypt(byte[] Encrypted)
        {
            BitArray enc = ToBits(Encrypted);
            BitArray XorH = SubBits(enc, 0, enc.Length / 2);
            XorH = XorH.Not();
            BitArray RHH = SubBits(enc, enc.Length / 2, enc.Length/2 );
            RHH = RHH.Not();
            BitArray LHH = XorH.Xor(RHH);
            BitArray bits = ConcateBits(LHH, RHH);
            byte[] decr= new byte[bits.Length / 8];
            bits.CopyTo(decr, 0);
            return decr;
        }
        public static string Encrypt(string Text)
        {
            byte[] b = DataEncryption.ConvertToBytes(Text);
            b = DataEncryption.Encrypt(b);
            return DataEncryption.ConvertToText(b);
        }
        public static string Decrypt(string EncryptedText)
        {
            byte[] b = DataEncryption.ConvertToBytes(EncryptedText);
            b = DataEncryption.Decrypt(b);
            return DataEncryption.ConvertToText(b);
        }
    }
}
