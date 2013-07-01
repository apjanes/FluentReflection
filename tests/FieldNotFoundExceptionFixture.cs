using System;
using System.Reflection;
using FluentAssertions;
using NUnit.Framework;

namespace FluentReflection.Tests
{
    [TestFixture]
    public class FieldNotFoundExceptionFixture
    {
        [TestCase("foobar", "FluentReflection.Is, FluentReflection", "The requested field: 'foobar' for type: 'FluentReflection.Is' cannot be found.")]
        [TestCase("foobar", null, "The requested field: 'foobar' cannot be found.")]
        [TestCase(null, "FluentReflection.Is, FluentReflection", "The requested field for type: 'FluentReflection.Is' cannot be found.")]
        [TestCase(null, null, "The requested field cannot be found.")]
        public void MessageShouldBeAsExpected(string field, string typeName, string expectedMessage)
        {
            var type = typeName == null ? null : Type.GetType(typeName, true);
            var exception = new FieldNotFoundException(type, field, BindingFlags.Default);
            exception.Message.Should().Be(expectedMessage);
        }
    }
}