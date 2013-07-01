using System;
using System.Reflection;

namespace FluentReflection
{
    public class MethodNotFoundException: MemberNotFoundException
    {
        public MethodNotFoundException(Type type, string memberName, BindingFlags flags) : base(type, memberName, "method", flags)
        {
        }

    }
}