using System;
using System.Numerics;

namespace KeySpace
{
    public class Key : IEquatable<Key>, IComparable<Key>
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

        public int CompareTo(Key other) => Space.Compare(this, other);
        public override string ToString() => $"<Key {Convert.ToBase64String(Bytes, Base64FormattingOptions.None)}>";
        public bool Equals(Key other) => ReferenceEquals(Space, other?.Space) && Space.Equal(this, other);
        public BigInteger Distance(Key other) => !ReferenceEquals(Space, other.Space) ? BigInteger.MinusOne : Space.Distance(this, other);
    }
}
