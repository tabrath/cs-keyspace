using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace KeySpace
{
    public class Key : IEquatable<Key>
    {
        public IKeySpace Space { get; }
        public byte[] Original { get; }
        public byte[] Bytes { get; }

        public Key(IKeySpace space, byte[] bytes, byte[] original = null)
        {
            Space = space;
            Original = original;
            Bytes = bytes;
        }

        public override string ToString() => $"<Key {Convert.ToBase64String(Bytes)}>";
        public bool Equals(Key other) => ReferenceEquals(Space, other.Space) && Space.Equal(this, other);
        public BigInteger Distance(Key other) => !ReferenceEquals(Space, other.Space) ? BigInteger.MinusOne : Space.Distance(this, other);
    }
}
