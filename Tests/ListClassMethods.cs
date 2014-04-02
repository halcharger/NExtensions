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
        [Test]
        public void ListStringExtensionMethods()
        {
            PrintOutClassMethods("Enumerable extentions", typeof(EnumerableExtensions));
            PrintOutClassMethods("Enum extentions", typeof(Enums));
            PrintOutClassMethods("Exception extentions", typeof(ExceptionExtensions));
            PrintOutClassMethods("String extentions", typeof(StringExtensions));
        }

        private void PrintOutClassMethods(string heading, Type classType)
        {
            Console.WriteLine("#####" + heading);
            Console.WriteLine(Environment.NewLine);
            classType.GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Select(mi => mi.Name)
                .Distinct()
                .OrderBy(n => n)
                .ForEach(n => Console.WriteLine("* " + n));
            Console.WriteLine(Environment.NewLine);

        }
    }
}