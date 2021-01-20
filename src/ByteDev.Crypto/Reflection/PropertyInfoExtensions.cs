using System;
using System.Reflection;

namespace ByteDev.Crypto.Reflection
{
    internal static class PropertyInfoExtensions
    {
        public static bool HasAttribute<TAttribute>(this PropertyInfo source) where TAttribute : Attribute
        {
            return source.GetCustomAttributes(typeof(TAttribute), false).Length > 0;
        }

        public static string GetValueAsString(this PropertyInfo source, object obj)
        {
            return source.GetValue(obj)?.ToString();
        }
    }
}