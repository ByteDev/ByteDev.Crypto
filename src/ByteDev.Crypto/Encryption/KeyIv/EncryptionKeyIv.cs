namespace ByteDev.Crypto.Encryption.KeyIv
{
    public class EncryptionKeyIv
    {
        public byte[] Key { get; }
        public byte[] Iv { get; }

        public EncryptionKeyIv(byte[] key, byte[] iv)
        {
            Key = key;
            Iv = iv;
        }
    }
}