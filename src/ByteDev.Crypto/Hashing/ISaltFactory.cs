namespace ByteDev.Crypto.Hashing
{
    public interface ISaltFactory
    {
        byte[] Create(int saltLength);
        string CreateAsBase64(int saltLength);
        string CreateAsHex(int saltLength);
    }
}