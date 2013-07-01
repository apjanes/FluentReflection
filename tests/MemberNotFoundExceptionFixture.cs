using System;
using System.Reflection;
using FluentAssertions;
using NUnit.Framework;

namespace FluentReflection.Tests
{
    [TestFixture]
    public class MemberNotFoundExceptionFixture
    {
        [TestCase("foobar", "FluentReflection.Is, FluentReflection", BindingFlags.Public | BindingFlags.Instance, "The requested public, instance member: 'foobar' for type: 'FluentReflection.Is' cannot be found.")]
        [TestCase("foobar", "FluentReflection.Is, FluentReflection", BindingFlags.Static, "The requested static member: 'foobar' for type: 'FluentReflection.Is' cannot be found.")]
        [TestCase("foobar", "FluentReflection.Is, FluentReflection", BindingFlags.NonPublic, "The requested non-public member: 'foobar' for type: 'FluentReflection.Is' cannot be found.")]
        [TestCase("foobar", null, BindingFlags.Default, "The requested member: 'foobar' cannot be found.")]
        [TestCase(null, "FluentReflection.Is, FluentReflection", BindingFlags.Default, "The requested member for type: 'FluentReflection.Is' cannot be found.")]
        [TestCase(null, null, BindingFlags.Default, "The requested member cannot be found.")]
        public void MessageShouldBeAsExpected(string member, string typeName, BindingFlags flags, string expectedMessage)
        {
            var type = typeName == null ? null : Type.GetType(typeName, true);
            var exception = new MemberNotFoundException(type, member, flags);
            exception.Message.Should().Be(expectedMessage);
        }

        [Test]
        public void MemberTypeShouldBeAsReceived()
        {
            var type = typeof (Is);
            var exception = new MemberNotFoundException(type, null, BindingFlags.Default);
            exception.MemberType.Should().Be(type);
        }

        [Test]
        public void MemberNameShouldBeAsReceived()
        {
            const string member = "blah";
            var exception = new MemberNotFoundException(null, member, BindingFlags.Default);
            exception.MemberName.Should().Be(member);
        }

        [Test]
        public void BindingFlagsShouldBeAsReceived()
        {
            const BindingFlags flags = BindingFlags.Static | BindingFlags.SetProperty;
            var exception = new MemberNotFoundException(null, "b", flags);
            exception.BindingFlags.Should().Be(flags);
        }
    }
}