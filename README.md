# Project Description
Makes .NET reflection eminently easy through a usable, readable fluent interface.

Typically using .NET reflection tends to be a messy and rather inelegant process that I seldom find to be enjoyable. There's no reason this has to be the case and the FluentReflection project aims to rectify this situation. With its fluent interface, FluentReflection can change this:

```
var instance = new MyClass();
var type = typeof(MyClass);
var info = type.GetMethod("PrivateMethod", BindingFlags.Instance | BindingFlags.NonPublic, null, new[] {typeof(string)}, null);
var result = (DateTime) info.Invoke(instance, new object[] {"param"});
```

to this:

```
var instance = new MyClass();
var result = instance.Method("PrivateMethod").WithParam("param").Invoke<DateTime>();
```

Now I don't know about you, but I'd prefer the second example any day!