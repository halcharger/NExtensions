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

Install
-------

NExtensions can be installed from nuget.org, the package is located over [here](https://www.nuget.org/packages/nextensions)

From the **Package Manager Console** in Visual Studio simply run 

`PM> Install-Package NExtensions`

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

Instead of:

```c#
string.Format("1 {0} 2 {1}", "one", "two") == "1 one 2 two";
```

We can write:

```c#
"1 {0} 2 {1}".FormatWith("one", "two") == "1 one 2 two";
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