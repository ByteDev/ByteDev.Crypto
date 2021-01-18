using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ByteDev.Crypto.Encryption;

namespace ByteDev.Crypto.Reflection
{
    internal static class TypeExtensions
    {
        public static IEnumerable<PropertyInfo> GetEncryptableProperties(this Type source)
        {
            return source
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(pi => pi.HasAttribute<EncryptAttribute>() && 
                             pi.PropertyType == typeof(string));
        }
    }
}