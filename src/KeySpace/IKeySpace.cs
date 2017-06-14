using System.Numerics;

namespace KeySpace
{
    public interface IKeySpace
    {
        Key Key(byte[] b);
        bool Equal(Key a, Key b);
        int Compare(Key a, Key b);
        BigInteger Distance(Key a, Key b);
    }
}