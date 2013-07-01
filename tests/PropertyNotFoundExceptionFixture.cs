using System;
using System.Reflection;
using FluentAssertions;
using NUnit.Framework;

namespace FluentReflection.Tests
{
    [TestFixture]
    public class PropertyNotFoundExceptionFixture
    {
        [TestCase("foobar", "FluentReflection.Is, FluentReflection", "The requested property: 'foobar' for type: 'FluentReflection.Is' cannot be found.")]
        [TestCase("foobar", null, "The requested property: 'foobar' cannot be found.")]
        [TestCase(null, "FluentReflection.Is, FluentReflection", "The requested property for type: 'FluentReflection.Is' cannot be found.")]
        [TestCase(null, null, "The requested property cannot be found.")]
        public void MessageShouldBeAsExpected(string field, string typeName, string expectedMessage)
        {
            var type = typeName == null ? null : Type.GetType(typeName, true);
            var exception = new PropertyNotFoundException(type, field, BindingFlags.Default);
            exception.Message.Should().Be(expectedMessage);
        }
    }
}