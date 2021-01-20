using System.Linq;
using ByteDev.Crypto.Reflection;

namespace ByteDev.Crypto.Encryption
{
    /// <summary>
    /// Extension methods for <see cref="T:System.Object" />.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Indicates whether the object contains sensitive data and should be encrypted or not
        /// using the EncryptProperties method.
        /// </summary>
        /// <param name="source">Object to check.</param>
        /// <returns>True the object contains sensitive data; otherwise false.</returns>
        public static bool IsSensitive(this object source)
        {
            if (source == null)
                return false;
            
            return source
                .GetType()
                .GetEncryptableProperties()
                .Any(pi => !string.IsNullOrEmpty(pi.GetValueAsString(source)));
        }
    }
}