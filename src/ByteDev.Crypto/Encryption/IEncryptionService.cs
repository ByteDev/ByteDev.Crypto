namespace ByteDev.Crypto.Encryption
{
    public interface IEncryptionService
    {
        byte[] Encrypt(string clearText);

        string Decrypt(byte[] cipher);
    }
}