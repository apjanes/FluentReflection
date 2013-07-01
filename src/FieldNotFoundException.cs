using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace FluentReflection
{
    public class FieldNotFoundException: MemberNotFoundException
    {
        public FieldNotFoundException(Type type, string memberName, BindingFlags flags) : base(type, memberName, "field", flags)
        {
        }
    }
}