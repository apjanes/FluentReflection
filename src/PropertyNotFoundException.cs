using System;
using System.Reflection;

namespace FluentReflection
{
    public class PropertyNotFoundException: MemberNotFoundException
    {
        public PropertyNotFoundException(Type type, string memberName, BindingFlags flags)
            : base(type, memberName, "property", flags)
        {
        }
    }
}