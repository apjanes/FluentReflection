using System;
using System.Reflection;
using FluentAssertions;
using FluentReflection.Impl;
using NUnit.Framework;

namespace FluentReflection.Tests
{
    [TestFixture]
    public class ConstructorSpecFixture
    {
        [Test]
        public void ConstructorShouldRequireType()
        {
            Action action = () => new ConstructorSpec(null);
            action.ShouldThrow<ArgumentNullException>().Where(x => x.ParamName == "type");
        }

        [Test]
        public void ExistsShouldReturnFalseForAnNonExistingConstructor()
        {
            typeof (TestClass).Constructor().WithParam(Guid.NewGuid()).Exists.Should().BeFalse();
        }

        [Test]
        public void ExistsShouldReturnTrueForAnExistingConstructor()
        {
            typeof (TestClass).Constructor().Exists.Should().BeTrue();
        }

        [Test]
        public void ItShouldConstructFromParmeterlessPrivateConstructor()
        {
            typeof (TestClass).Construct<TestClass>().Should().NotBeNull();
        }
        
        [Test]
        public void ItShouldConstructFromParmeteretizedPublicConstructor()
        {
            var obj = typeof (TestClass).Constructor()
                                        .That(Is.Public)
                                        .WithParams("Aaron", "Janes")
                                        .Construct<TestClass>();
            obj.Should().NotBeNull();
            obj.FirstName.Should().Be("Aaron");
            obj.LastName.Should().Be("Janes");
        }

        [Test]
        public void ItShouldRaiseAConstructorNotFoundExceptionForInvalidConstructor()
        {
            Action action = () => typeof (TestClass).Constructor().WithParam(Guid.NewGuid()).Construct<TestClass>();
            action.ShouldThrow<ConstructorNotFoundException>();
        }
    }
}