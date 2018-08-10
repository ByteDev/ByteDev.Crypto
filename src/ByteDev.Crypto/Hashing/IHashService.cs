namespace ByteDev.Crypto.Hashing
{
    public interface IHashService
    {
        string Hash(string phrase);
        string Hash(string phrase, string salt);

        bool Verify(string phrase, string expectedHashedPhrase);
        bool Verify(string phrase, string salt, string expectedHashedPhrase);
    }
}