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

        public static string Copy(this string input)
        {
            if (input == null) return null;
            return string.Copy(input);
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

        public static string Append(this string input, string value, int times  = 1)
        {
            if (input == null || times == 0) return input;

            var newValue = value;

            if (times > 1)
                Enumerable.Range(1, times-1).ForEach(i => newValue = newValue.Append(value));

            return string.Concat(input, newValue);
        }

        public static string Append(this string input, IEnumerable<string> values)
        {
            return input.Append(values.ToArray());
        }

        public static string Append(this string input, params string[] values)
        {
            return input.Append(values.JoinWith(string.Empty));
        }

        public static string AppendNewLine(this string input, int times = 1)
        {
            return input.Append(Environment.NewLine, times);
        }

        public static string Remove(this string input, params string[] toRemove)
        {
            toRemove.ForEach(r => input = input.Replace(r, string.Empty));
            return input;
        }

        public static bool ContainsAny(this string input, params string[] contains)
        {
            if (input == null) return false;
            return contains.Any(input.Contains);
        }

        public static bool ContainsAll(this string input, params string[] contains)
        {
            if (input == null) return false;
            return contains.All(input.Contains);
        }

        public static IEnumerable<string> SplitBy(this string value, string delimiter, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries)
        {
            var splitValues = value.Split(new[] { delimiter }, System.StringSplitOptions.None);

            if (options.HasFlag(StringSplitOptions.TrimWhiteSpaceFromEntries) ||
                options.HasFlag(StringSplitOptions.TrimWhiteSpaceAndRemoveEmptyEntries))
                splitValues = splitValues.Select(s => s.Trim()).ToArray();

            if (options.HasFlag(StringSplitOptions.RemoveEmptyEntries) ||
                options.HasFlag(StringSplitOptions.TrimWhiteSpaceAndRemoveEmptyEntries))
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

        public static bool ToBoolean(this string input)
        {
            var trueValues = new[] { "true", "on", "1" };
            var falseValues = new[] { "false", "off", "0" };

            if (trueValues.Contains(input.ToLower())) return true;
            if (falseValues.Contains(input.ToLower())) return false;

            return Convert.ToBoolean(input);
        }

        public static decimal ToDecimal(this string input)
        {
            if (input == null) throw new ArgumentException("Cannot convert empty value to a Decimal");

            string s = input.Remove("(", ")", ",");

            if (s.IsNullOrEmpty()) throw new ArgumentException("Cannot convert Empty String to Decimal");

            if (s == "-") return 0M;

            bool percent = false;
            if (s.EndsWith("%"))
            {
                s = s.Remove("%");
                percent = true;
            }

            decimal result;

            // detect scientific notation, and convert to double
            if (s.ContainsAny("E", "e"))
            {
                try
                {
                    result = (decimal)double.Parse(s);
                }
                catch (FormatException)
                {
                    throw new FormatException("Couldn't convert value '{0}' to a Decimal".FormatWith(s));
                }
            }
            else
            {
                if (!decimal.TryParse(s, out result)) throw new FormatException("Couldn't convert value '{0}' to a Decimal".FormatWith(s));
            }

            return percent ? result / 100M : result;
        }

        public static int ToInteger(this string input)
        {
            if (input == null) throw new ArgumentException("Cannot convert empty value to Integer");

            var s = input.Remove("(", ")", ",");

            if (s.IsNullOrWhiteSpace()) throw new ArgumentException("Cannot convert empty value to Integer");

            if (s == "-") return 0;

            return (int) Convert.ToDecimal(s);
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