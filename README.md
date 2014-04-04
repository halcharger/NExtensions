NExtensions
===========

Adding .NET framework extension methods to basic types that you thought would have been there but aren't

About
-----

NExtensions is a zero-dependency lightweight library containing small simple yet indispensable extension methods for .NET framework base types. These extension methods make your code more concise, more readable and more fluent.

Install
-------

NExtensions can be installed from nuget.org, the package is located over [here](https://www.nuget.org/packages/nextensions)

From the **Package Manager Console** in Visual Studio simply run 

`PM> Install-Package NExtensions`

The symbols have also been pushed along with the nuget package so you should be able to step into the source if your Visual Studio is configured correctly. To configure your Visual Studio go to **Debug > Options and Settings** and make the following changes:

* Under General, turn **off** “Enable Just My Code”
* Under General, turn **on** “Enable source server support”. You may have to Ok a security warning.
* Under Symbols, add “http://srv.symbolsource.org/pdb/Public” to the list. 

Extensions Summary
------------------

#####[Enumerable extentions](https://github.com/halcharger/NExtensions/wiki/Enumerable-extensions)

* ContainsAll
* ContainsNone
* EmptyIfNull
* ForEach
* GetDuplicates
* IsNullOrEmpty
* None
* ToEnumerable

#####[Enum extentions](https://github.com/halcharger/NExtensions/wiki/Enum-extensions)

* GetDescription
* GetValues
* ToEnum

#####[Exception extentions](https://github.com/halcharger/NExtensions/wiki/Exception-extensions)

* GetBaseException

#####[Numeric extentions](https://github.com/halcharger/NExtensions/wiki/Numeric-extensions)

* Absolute

#####[String extentions](https://github.com/halcharger/NExtensions/wiki/String-extensions)

* Append
* AppendNewLine
* ContainsAll
* ContainsAny
* FormatWith
* HasValue
* IsNullOrEmpty
* IsNullOrWhiteSpace
* JoinWith
* JoinWithComma
* JoinWithNewLine
* JoinWithSemiColon
* Remove
* SplitBy
* SplitByComma
* SplitByNewLine
* SplitBySemiColon
* ToBoolean
* ToDecimal
* ToInteger

Detailed Usage
--------------

For details usage see the [Wiki](https://github.com/halcharger/NExtensions/wiki) or read the tests.
