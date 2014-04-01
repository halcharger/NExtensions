NExtensions
===========

Adding .NET framework extension methods to basic types that you thought would have been there but aren't

About
-----

NExtensions is a zero-dependency lightweight library containing small simple yet indispensable extension methods for .NET framework base types. These extension methods make your code more concise, more readable and more fluent.

Table of contents
-----------------

* [Install](https://github.com/halcharger/NExtensions#install)
* [Usage](https://github.com/halcharger/NExtensions#usage)
* [String extensions](https://github.com/halcharger/NExtensions#string-extensions)
* [Enumerable extensions](https://github.com/halcharger/NExtensions#enumerable-extensions)
* [Enum extensions](https://github.com/halcharger/NExtensions#enum-extensions)
* [Exception extensions](https://github.com/halcharger/NExtensions#exception-extensions)

Install
-------

NExtensions can be installed from nuget.org, the package is located over [here](https://www.nuget.org/packages/nextensions)

From the **Package Manager Console** in Visual Studio simply run 

`PM> Install-Package NExtensions`

The symbols have also been pushed along with the nuget package so you should be able to step into the source if your Visual Studio is configured correctly. To configure your Visual Studio go to **Debug > Options and Settings** and make the following changes:

* Under General, turn **off** “Enable Just My Code”
* Under General, turn **on** “Enable source server support”. You may have to Ok a security warning.
* Under Symbols, add “http://srv.symbolsource.org/pdb/Public” to the list. 

Usage
-----

###String extensions

#####StringExtensions.IsNullOrEmpty

Instead of:

```c#
if (string.IsNullOrEmpty(user.Username))
{
	//do something...
}
```

We can now write:

```c#
if (user.Username.IsNullOrEmpty())
{
	//do something...
}
```

Which is a little more fluent and readable.

#####StringExtensions.FormatWith

Given:

```c#
var template = "1 {0} 2 {1}";
```

Instead of:

```c#
string.Format(template, "one", "two") == "1 one 2 two";
```

We can write:

```c#
template.FormatWith("one", "two") == "1 one 2 two";
```

#####StringExtensions.JoinWith and variants

Given:

```c#
var values = new []{"one", "two", "three"};
```

Instead of:

```c#
string.Join(values, "/") == "one/two/three";
```

We can write:

```c#
values.JoinWith("/") == "one/two/three";
```

And some shortcut variants:

```c#
values.JoinWithComma() == "one,two,three";

values.JoinWithComma(StringJoinOptions.AddSpace) == "one, two, three";

values.JoinWithSemiColon() == "one;two;three";

values.JoinWithSemiColon(StringJoinOptions.AddSpace) == "one; two; three";

values.JoinWithNewLine() == "one\r\ntwo\r\nthree";
```

#####StringExtensions.SplitBy and variants

Given:

```c#
var value = "one/ two/three/ ";
```

Instead of:

```c#
var values = string.Split(new{"/"}, StringSplitOptions.RemoveEmptyEntries);
```

We can write:

```c#
var values = value.SplitBy("/");
```

Which both result in:

```c#
values.Length == 4
values[0] == "one"
values[1] == " two"
values[2] == "three"
values[3] == " "
```

But with the SplitBy extension method we can also write this:

```c#
var values = value.SplitBy("/", StringSplitOptions.TrimWhiteSpaceAndRemoveEmptyEntries);
```

Which then results in this:

```c#
values.Length == 3
values[0] == "one"
values[1] == "two"
values[2] == "three"
```

Other variants which are hopefully self explanatory are:

```c#
var values = value.SplitByComma();

var values = value.SplitBySemiColon();

var values = value.SpliyByNewLine();
```

All the above variants also accept an optional options parameter with which we can specify whether to remove empty entries and / or to trim the split string values.

#####StringExtensions.Append

Given:

```c#
string one = "one";
string two = "two";
```

Instead of:

```c#
string.Concat(one, two) == "onetwo";
```

We can write:

```c#
one.Append(two) == "onetwo";
```

#####StringExtensions.Remove

Given:

```c#
var value = "onetwothree";
```

Instead of:

```c#
value.Replace("two", string.Empty) == "onethree";
```

We can write:

```c#
value.Remove("two") == "onethree";
```

###Enumerable extensions

#####EnumerableExtensions.ForEach

Instead of:

```c#
foeach(var item in enumerable)
{
	DoSomething(item);
}
```

We can now write:

```c#
enumerable.ForEach(DoSomething);
```

Much more concise and expressive.

#####EnumerableExtensions.None

Instead of:

```c#
if (!enumerable.Any())
{
	//do something...
}
```

We can write:

```c#
if (enumerable.None())
{
	//do something...
}
```

Instead of:

```c#
if (!users.Any(u => u.Username == "user123"))
{
	//do something...
}
```

We can write:

```c#
if (users.None(u => u.Username == "user123"))
{
	//do something...
}
```

###Enum extensions

#####Enums.GetDescription

In many instances we want to display a friendly name for an enum value. For example given the following enum:

```c#
public enum Status
{
	Pending
	RunFailed
	RunSucceeded
}
```

When displaying these enum values in a UI we ideally would want to display the values "Run Failed" instead of "RunFailed" and so on. We can apply the `System.ComponentModel.DescriptionAttribute` like this:

```c#
public enum Status
{
	Pending
	[Description("Run Failed")]
	RunFailed
	[Description("Run Suceeded")]
	RunSucceeded
}
```
And then access the descriptive text of an enum value like this:

```c#
Status.Pending.GetDescription() == "Pending"

Status.RunFailed.GetDescription() == "Run Failed"

Status.RunSucceeded.GetDescription() == "Run Succeeded"
```

You'll notice that when a `DescriptionAttribute` is not applied the standard enum ToString value is returned.

#####Enums.GetValues

In order to get a list of enum values for display in the UI or comparision we can write:

```c#
var enumValues = Enums.GetValues<Status>().ToList();

enumValues[0] == Status.Pending
enumValues[1] == Status.RunFailed
enumValues[2] == Status.RunSucceeded
```

If you don't like using the above syntax you can also write the following:

```c#
var enumValues = typeof(Status).GetValues<Status>();
```

Personally it's not my favourite syntax but it's there if you want to use it.

#####Enums.ToEnum

Given the string value of an enum we can quickly convert this back to the enum value by writing the following:

```c#
"Pending".ToEnum<Status>() == Status.Pending;
```

You can also use the string description value of an enum value, like this:

```c#
"Run Failed".ToEnum<Status>() == Status.RunFailed;
```

###Exception extensions

#####ExceptionExtensions.GetBaseException

Often when handling exceptions you are only interested in the original exception thrown in the stack. In a multi-level exception stack Exception.InnerException only returns the Exception one level down in the stack. you could use Exception.InnerException.InnerException... but this requires you knowing exactly how deep the Exception stack is. Now you can write the following:

```c#
catch(Exception ex)
{
	logger.Error(ex.GetBaseException());
}
```

**For more extensive documentation on what extension methods are available and how to use them see the tests.**