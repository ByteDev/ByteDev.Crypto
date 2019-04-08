namespace ByteDev.Crypto.Hashing
{
    public interface IHashService
    {
        string Hash(HashPhrase phrase);

        bool Verify(HashPhrase phrase, string expectedHashedPhrase);
    }
}