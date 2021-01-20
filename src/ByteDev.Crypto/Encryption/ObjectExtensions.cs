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
        /// Indicates if the object has sensitive properties set.
        /// In other words the object has any public string properties with a
        /// <typeparamref name="ByteDev.Crypto.Encryption.EncryptAttribute" /> and their
        /// value set to something other than null or empty.
        /// </summary>
        /// <param name="source">Object to check.</param>
        /// <returns>True the object contains sensitive info set; otherwise false.</returns>
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