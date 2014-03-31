NExtensions
===========

Adding .NET framework extension methods to basic types that you thought would have been there but aren't

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
