namespace NExtensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Indicates whether the specified string is null or an <see cref="F:System.String.Empty"/> string.
        /// </summary>
        /// 
        /// <returns>
        /// true if the <paramref name="value"/> parameter is null or an empty string (""); otherwise, false.
        /// </returns>
        /// <param name="value">The string to test. </param><filterpriority>1</filterpriority>
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// Indicates whether a specified string is null, empty, or consists only of white-space characters.
        /// </summary>
        /// 
        /// <returns>
        /// true if the <paramref name="value"/> parameter is null or <see cref="F:System.String.Empty"/>, or if <paramref name="value"/> consists exclusively of white-space characters.
        /// </returns>
        /// <param name="value">The string to test.</param>
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static string FormatWith(this string format, params object[] args)
        {
            return string.Format(format, args);
        }
    }
}