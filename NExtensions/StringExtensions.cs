using System;
using System.Collections.Generic;

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

        public static string JoinWith<T>(this IEnumerable<T> values, string separator)
        {
            return string.Join(separator, values.EmptyIfNull());
        }

        public static string JoinWithComma<T>(this IEnumerable<T> values, StringJoinOptions options = null)
        {
            options = options ?? new StringJoinOptions();
            return values.JoinWith(",".Append(options.SeperatorSuffix));
        }

        public static string JoinWithSemiColon<T>(this IEnumerable<T> values, StringJoinOptions options = null)
        {
            options = options ?? new StringJoinOptions();
            return values.JoinWith(";".Append(options.SeperatorSuffix));
        }

        public static string JoinWithNewLine<T>(this IEnumerable<T> values)
        {
            return values.JoinWith(Environment.NewLine);
        }

        public static string Append(this string value, string valueToAppend)
        {
            if (value == null) return value;

            return string.Concat(value, valueToAppend);
        }

        public static string Remove(this string input, string toRemove)
        {
            return input.Replace(toRemove, string.Empty);
        }

    }

    public class StringJoinOptions
    {
        public StringJoinOptions()
        {
            SeperatorSuffix = string.Empty;
        }

        public string SeperatorSuffix { get; set; }

        public static StringJoinOptions AddSpace { get { return new StringJoinOptions { SeperatorSuffix = " " }; } }
    }
}