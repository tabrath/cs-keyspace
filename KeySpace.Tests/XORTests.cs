using System;
using System.Linq;
using System.Numerics;
using NUnit.Framework;

namespace KeySpace.Tests
{
    [TestFixture]
    public class XORTests
    {
        [TestCase(new byte[] { 0x00, 0x00, 0x00, 0x80, 0x00, 0x00, 0x00 }, 24)]
        [TestCase(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }, 56)]
        [TestCase(new byte[] { 0x00, 0x58, 0xFF, 0x80, 0x00, 0x00, 0xF0 }, 9)]
        public void TestPrefixLength(byte[] c, int len)
        {
            Assert.That(XORKeySpace.ZeroPrefixLength(c), Is.EqualTo(len));
        }

        [Test]
        public void TestXorKeySpace()
        {
            var ids = new[]
            {
                new byte[]{0xFF, 0xFF, 0xFF, 0xFF},
                new byte[]{0x00, 0x00, 0x00, 0x00},
                new byte[]{0xFF, 0xFF, 0xFF, 0xF0},
            };

            var ks = new[]
            {
                new[] { XORKeySpace.Instance.Key(ids[0]), XORKeySpace.Instance.Key(ids[0]) },
                new[] { XORKeySpace.Instance.Key(ids[1]), XORKeySpace.Instance.Key(ids[1]) },
                new[] { XORKeySpace.Instance.Key(ids[2]), XORKeySpace.Instance.Key(ids[2]) },
            };

            var i = 0;
            foreach (var set in ks)
            {
                Assert.That(set[0].Equals(set[1]));
                Assert.That(set[0].Bytes, Is.EqualTo(set[1].Bytes));
                Assert.That(set[0].Original, Is.EqualTo(ids[i]));
                Assert.That(set[0].Bytes.Length, Is.EqualTo(32));
                i++;
            }

            for (i = 1; i < ks.Length; i++)
            {
                Assert.That(ks[i][0].Distance(ks[i-1][0]).CompareTo(ks[i-1][0].Distance(ks[i][0])), Is.EqualTo(0));
                Assert.That(ks[i][0].Equals(ks[i-1][0]), Is.False);
            }
        }

        [Test]
        public void TestDistancesAndCenterSorting()
        {
            var adjs = new[]
            {
                new byte[] {173, 149, 19, 27, 192, 183, 153, 192, 177, 175, 71, 127, 177, 79, 207, 38, 166, 169, 247, 96, 121, 228, 139, 240, 144, 172, 183, 232, 54, 123, 253, 14},
                new byte[] {223, 63, 97, 152, 4, 169, 47, 219, 64, 87, 25, 45, 196, 61, 215, 72, 234, 119, 138, 220, 82, 188, 73, 140, 232, 5, 36, 192, 20, 184, 17, 25},
                new byte[] {73, 176, 221, 176, 149, 143, 22, 42, 129, 124, 213, 114, 232, 95, 189, 154, 18, 3, 122, 132, 32, 199, 53, 185, 58, 157, 117, 78, 52, 146, 157, 127},
                new byte[] {73, 176, 221, 176, 149, 143, 22, 42, 129, 124, 213, 114, 232, 95, 189, 154, 18, 3, 122, 132, 32, 199, 53, 185, 58, 157, 117, 78, 52, 146, 157, 127},
                new byte[] {73, 176, 221, 176, 149, 143, 22, 42, 129, 124, 213, 114, 232, 95, 189, 154, 18, 3, 122, 132, 32, 199, 53, 185, 58, 157, 117, 78, 52, 146, 157, 126},
                new byte[] {73, 0, 221, 176, 149, 143, 22, 42, 129, 124, 213, 114, 232, 95, 189, 154, 18, 3, 122, 132, 32, 199, 53, 185, 58, 157, 117, 78, 52, 146, 157, 127},
            };

            var keys = adjs.Select(a => new Key(XORKeySpace.Instance, a)).ToArray();
            Func<long, BigInteger, int> cmp = (a, b) => new BigInteger(a).CompareTo(b);

            Assert.That(cmp(0, keys[2].Distance(keys[3])), Is.EqualTo(0));
            Assert.That(cmp(1, keys[2].Distance(keys[4])), Is.EqualTo(0));

            var d1 = keys[2].Distance(keys[5]);
            var d2 = XORKeySpace.XOR(keys[2].Bytes, keys[5].Bytes);
            d2 = d2.Skip(keys[2].Bytes.Length - d1.ToByteArray().Length).ToArray();
            Assert.That(d1.ToByteArray().Reverse().ToArray(), Is.EqualTo(d2));

            Assert.That(cmp(2<<32, keys[2].Distance(keys[5])), Is.EqualTo(-1));

            var keys2 = keys.SortByDistance(keys[2]);
            var order = new[] {2,3,4,5,1,0};
            var i = 0;
            foreach (var o in order)
            {
                Assert.That(keys[o].Bytes, Is.EqualTo(keys2[i++].Bytes));
            }
        }
    }
}
