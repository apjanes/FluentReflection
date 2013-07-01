using System;
using FluentAssertions;
using NUnit.Framework;

namespace FluentReflection.Tests
{
    [TestFixture]
    public class ReflectionExtensionsFixture
    {
        [Test]
        public void AsShouldReturnCastVersionWhenTypesAreRight()
        {
            var aaron = new Aaron();
            var result = ReflectionExtensions.As<Person>(aaron);
            result.Should().Be(aaron);
        }
        
        [Test]
        public void AsShouldReturnNullWhenNullIsReceived()
        {
            ReflectionExtensions.As<Person>(null).Should().BeNull();
        }
        
        [Test]
        public void AsShouldThrowInvalidCastExceptionWhenTypesAreRight()
        {
            Action action = () => ReflectionExtensions.As<double>(1);
            action.ShouldThrow<InvalidCastException>();
        }

        private class Aaron: Person
        {
            
        }

        private class Person
        {
            
        }
    }
}