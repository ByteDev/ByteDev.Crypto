namespace ByteDev.Crypto.Hashing.Algorithms
{
    public interface IHashAlgorithm
    {
        byte[] Hash(byte[] data);
    }
}