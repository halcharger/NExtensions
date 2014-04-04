using System;
using System.Linq;
using System.Reflection;
using NExtensions;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class ListClassMethods
    {
        /// <summary>
        /// Utility method to help generate reademe and wiki markdown for documentation
        /// </summary>
        [Test]
        public void ListStringExtensionMethods()
        {
            PrintOutClassMethods("[DateTime extentions](https://github.com/halcharger/NExtensions/wiki/DateTime-extensions)", typeof(DateTimeExtensions));
            PrintOutClassMethods("[Enumerable extentions](https://github.com/halcharger/NExtensions/wiki/Enumerable-extensions)", typeof(EnumerableExtensions));
            PrintOutClassMethods("[Enum extentions](https://github.com/halcharger/NExtensions/wiki/Enum-extensions)", typeof(Enums));
            PrintOutClassMethods("[Exception extentions](https://github.com/halcharger/NExtensions/wiki/Exception-extensions)", typeof(ExceptionExtensions));
            PrintOutClassMethods("[Numeric extentions](https://github.com/halcharger/NExtensions/wiki/Numeric-extensions)", typeof(NumericExtensions));
            PrintOutClassMethods("[Object extentions](https://github.com/halcharger/NExtensions/wiki/Object-extensions)", typeof(ObjectExtensions));
            PrintOutClassMethods("[String extentions](https://github.com/halcharger/NExtensions/wiki/String-extensions)", typeof(StringExtensions));
        }

        private void PrintOutClassMethods(string heading, Type classType)
        {
            Console.WriteLine("#####" + heading);
            Console.WriteLine(" ");
            Console.WriteLine(Environment.NewLine);
            classType.GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Select(mi => mi.Name)
                .Distinct()
                .OrderBy(n => n)
                .ForEach(n => Console.WriteLine("* " + n));
            Console.WriteLine(" ");
            Console.WriteLine(Environment.NewLine);

        }
    }
}