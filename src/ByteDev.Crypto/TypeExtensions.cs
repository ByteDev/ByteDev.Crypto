using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ByteDev.Crypto
{
    internal static class TypeExtensions
    {
        public static IEnumerable<PropertyInfo> GetPropertiesWithAttribute<TAttribute>(this Type source) where TAttribute : Attribute
        {
            return source
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(pi => pi.GetCustomAttributes(typeof(TAttribute), false).Length > 0);
        }
    }
}