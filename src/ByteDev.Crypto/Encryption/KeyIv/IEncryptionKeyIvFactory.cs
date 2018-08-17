namespace ByteDev.Crypto.Encryption.KeyIv
{
    public interface IEncryptionKeyIvFactory
    {
        EncryptionKeyIv Create(string password, byte[] salt);
    }
}