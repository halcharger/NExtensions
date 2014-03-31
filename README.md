NExtensions
===========

Adding .NET framework extension methods to basic types that you thought would have been there but aren't

Install
-------

NExtensions can be installed from nuget.org, the package is located over [here](https://www.nuget.org/packages/nextensions)

From the package-console in Visual Studio simple run `Install-Package NExtensions`

Some examples:

String extensions
-----------------

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

Enumerable extensions
---------------------

instead of:

```c#
foeach(var item in enumerable)
{
	DoSomething(item);
}
```

we can now write:

```c#
enumerable.Foreach(DoSomething);
```

Much more concise and expressive.