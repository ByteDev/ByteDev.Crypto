using System;

namespace ByteDev.Crypto.Encryption
{
    /// <summary>
    /// Indicates that a property should be encrypted.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class EncryptAttribute : Attribute
    {
    }
}