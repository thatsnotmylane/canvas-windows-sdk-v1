using System;

namespace Canvas.v1.Extensions
{
    internal static class NullCheckExtensions
    {
        /// <summary>
        /// Checks if the object is null 
        /// </summary>
        /// <typeparam name="T">Type of the object being checked</typeparam>
        /// <param name="param"></param>
        /// <param name="name"></param>
        internal static T ThrowIfNull<T>(this T param, string name) where T : class
        {
            if (param == null)
                throw new ArgumentNullException(name);

            return param;
        }

        /// <summary>
        /// Checks if a string is null or whitespace
        /// </summary>
        /// <param name="value"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        internal static string ThrowIfNullOrWhiteSpace(this string value, string name)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Required field cannot be null or whitespace", name);

            return value;
        }

        /// <summary>
        /// Checks if a non-null string is too short
        /// </summary>
        /// <param name="value"></param>
        /// <param name="minLength"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        internal static string ThrowIfNotNullAndShorterThanLength(this string value, int minLength, string name)
        {
            if (!string.IsNullOrWhiteSpace(value) && value.Length < minLength) 
                throw new ArgumentOutOfRangeException(name, "value has minimum length of " + minLength);

            return value;
        }


        /// <summary>
        /// Checks if a numeric has a zero-value
        /// </summary>
        /// <param name="value"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        internal static long ThrowIfUnassigned(this long value, string name)
        {
            if (value == 0)
                throw new ArgumentException("Required field must have a non-zero value", name);
            return value;
        }

        /// <summary>
        /// Checks if a numeric has a zero-value
        /// </summary>
        /// <param name="value"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        internal static int ThrowIfUnassigned(this int value, string name)
        {
            if (value == 0)
                throw new ArgumentException("Required field must have a non-zero value", name);
            return value;
        }
    }
}
