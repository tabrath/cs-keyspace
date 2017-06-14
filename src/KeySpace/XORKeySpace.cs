using System;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;

namespace KeySpace
{
    public class XORKeySpace : IKeySpace
    {
        public static readonly XORKeySpace Instance = new XORKeySpace();

        private readonly HashAlgorithm _sha256;

        protected XORKeySpace()
        {
            _sha256 = SHA256.Create();
        }

        public Key Key(byte[] b) => new Key(this, _sha256.ComputeHash(b), b);
        public bool Equal(Key a, Key b) => a.Bytes.SequenceEqual(b.Bytes);
        public int Compare(Key a, Key b)
        {
            var ab = a.Bytes;
            var bb = b.Bytes;
            for (var i = 0; i < ab.Length; i++)
            {
                if (ab[i] > bb[i])
                    return 1;
                if (ab[i] < bb[i])
                    return -1;
            }

            return 0;
        }

        public BigInteger Distance(Key a, Key b) => ToBigInt(XOR(a.Bytes, b.Bytes));

        public static BigInteger ToBigInt(byte[] bytes)
        {
            Array.Reverse(bytes);
            if ((bytes[bytes.Length - 1] & 0x80) > 0)
            {
                var tmp = new byte[bytes.Length];
                Array.Copy(bytes, tmp, bytes.Length);
                bytes = new byte[tmp.Length + 1];
                Array.Copy(tmp, bytes, tmp.Length);
            }
            return new BigInteger(bytes);
        }

        public static byte[] XOR(byte[] a, byte[] b)
        {
            var c = new byte[a.Length];
            for (var i = 0; i < a.Length; i++)
                c[i] = (byte)(a[i] ^ b[i]);
            //Array.Reverse(c);
            return c;
        }

        public static int ZeroPrefixLength(byte[] id)
        {
            for (var i = 0; i < id.Length; i++)
            {
                for (var j = 0; j < 8; j++)
                {
                    if ((id[i] >> (7 - j) & 0x1) != 0)
                        return i * 8 + j;
                }
            }
            return id.Length * 8;
        }
    }
}