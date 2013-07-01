using System;
using System.Reflection;

namespace FluentReflection
{
    public class ConstructorNotFoundException:MemberNotFoundException
    {
        public ConstructorNotFoundException(Type type, string memberName, BindingFlags flags)
            : base(type, memberName, "constructor", flags)
        {
        }
    }
}