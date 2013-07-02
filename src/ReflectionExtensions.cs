using System;
using System.Reflection;
using FluentReflection.Impl;
using FluentReflection.Specs;

namespace FluentReflection
{
    public static class ReflectionExtensions
    {
        public static T As<T>(object subject)
        {
            if (subject == null) return (T)(object)null;
            if (subject is T) return (T) subject;
            throw new InvalidCastException("Type '" + subject.GetType() + "' cannot be cast to '" + typeof(T).FullName);
        }

        public static T Construct<T>(this Type type)
        {
            var spec = new ConstructorSpec(type);
            return spec.Construct<T>();
        }

        /// <summary>
        /// Access a constructor for the specified type.
        /// </summary>
        /// <param name="type">The type to find the constructor for.</param>
        /// <returns>
        /// A spec to enable the construction of an instance or 
        /// further modification of the specification.
        /// </returns>
        public static IConstructorSpec Constructor(this Type type)
        {
            return new ConstructorSpec(type);
        }

        public static IMethodSpec Method(this Type type, string name)
        {
            return new MethodSpec(type, name, null);
        }

        public static IMethodSpec Method<T>(this T obj, string name)
        {
            return new MethodSpec(obj.GetType(), name, obj);
        }

        public static IFieldSpec Field<T>(this T obj, string name)
        {
            return new FieldSpec(obj.GetType(), name, obj);
        }

        public static IPropertySpec Property<T>(this T obj, string name)
        {
            return new PropertySpec(obj.GetType(), name, obj);
        }

        internal static bool HasFlag(this BindingFlags flags, BindingFlags toCheck)
        {
            return (flags & toCheck) == toCheck;
        }
    }
}