namespace ByteDev.Crypto.Hashing
{
    public interface ISaltFactory
    {
        HashSalt Create(int saltLength);
    }
}