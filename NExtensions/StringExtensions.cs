using System;
using System.Collections.Generic;
using System.Linq;

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

        /// <summary>
        /// Returns the inverse of input.IsNullOrEmpty()
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool HasValue(this string input)
        {
            return !input.IsNullOrEmpty();
        }

        public static string FormatWith(this string format, params object[] args)
        {
            return string.Format(format, args);
        }

        public static string JoinWith<T>(this IEnumerable<T> values, string separator)
        {
            return string.Join(separator, values.EmptyIfNull());
        }

        public static string JoinWithComma<T>(this IEnumerable<T> values, StringJoinOptions options = StringJoinOptions.None)
        {
            var suffix = GetStringJoinSeperatorSuffix(options);
            return values.JoinWith(",".Append(suffix));
        }

        public static string JoinWithSemiColon<T>(this IEnumerable<T> values, StringJoinOptions options = StringJoinOptions.None)
        {
            var suffix = GetStringJoinSeperatorSuffix(options);
            return values.JoinWith(";".Append(suffix));
        }

        private static string GetStringJoinSeperatorSuffix(StringJoinOptions options)
        {
            var shouldAddSpace = (options & StringJoinOptions.AddSpaceSuffix) == StringJoinOptions.AddSpaceSuffix;
            return shouldAddSpace ? " " : string.Empty;
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

        public static IEnumerable<string> SplitBy(this string value, string delimiter, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries)
        {
            var splitValues = value.Split(new[] { delimiter }, System.StringSplitOptions.None);

            if ((options & StringSplitOptions.TrimWhiteSpaceFromEntries) == StringSplitOptions.TrimWhiteSpaceFromEntries ||
                (options & StringSplitOptions.TrimWhiteSpaceAndRemoveEmptyEntries) == StringSplitOptions.TrimWhiteSpaceAndRemoveEmptyEntries)
                splitValues = splitValues.Select(s => s.Trim()).ToArray();

            if ((options & StringSplitOptions.RemoveEmptyEntries) == StringSplitOptions.RemoveEmptyEntries ||
                (options & StringSplitOptions.TrimWhiteSpaceAndRemoveEmptyEntries) == StringSplitOptions.TrimWhiteSpaceAndRemoveEmptyEntries)
                splitValues = splitValues.Where(s => !s.IsNullOrEmpty()).ToArray();

            return splitValues;
        }

        public static IEnumerable<string> SplitByComma(this string value, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries)
        {
            return value.SplitBy(",", options);
        }

        public static IEnumerable<string> SplitBySemiColon(this string value, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries)
        {
            return value.SplitBy(";", options);
        }

        public static IEnumerable<string> SplitByNewLine(this string value, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries)
        {
            return value.SplitBy(Environment.NewLine, options);
        }

    }

    [Flags]
    public enum StringJoinOptions
    {
        None = 0, 
        AddSpaceSuffix = 1
    }

    [Flags]
    public enum StringSplitOptions
    {
        None = 0, 
        RemoveEmptyEntries = 1, 
        TrimWhiteSpaceFromEntries = 2, 
        TrimWhiteSpaceAndRemoveEmptyEntries = 4
    }
}