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

instead of:

```c#
if (string.IsNullOrEmpty(user.Username))
{
	//do something...
}
```

we can now write:

```c#
if (user.Username.IsNullOrEmpty())
{
	//do something...
}
```

Which is a little more fluent and readable.

###Enumerable extensions

instead of:

```c#
foeach(var item in enumerable)
{
	DoSomething(item);
}
```

we can now write:

```c#
enumerable.ForEach(DoSomething);
```

Much more concise and expressive.

###Enum extensions

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