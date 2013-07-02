using System;
using FluentAssertions;
using NUnit.Framework;

namespace FluentReflection.Tests
{
    [TestFixture]
    public class MethodSpecFixture
    {
        [Test]
        public void ExistsShouldReturnFalseForAnNonExistingMethod()
        {
            typeof(TestClass).Method("xxxx").Exists.Should().BeFalse();
        }

        [Test]
        public void ExistsShouldReturnTrueForAnExistingMethod()
        {
            new TestClass("", "").Method("MyClassName").Exists.Should().BeTrue();
        }

        [Test]
        public void ItShouldInvokeStaticPrivateMethodWithoutParams()
        {
            typeof (TestClass).Method("ClassName").Invoke<string>().Should().Be("TestClass");
        }

        [Test]
        public void ItShouldInvokeInstancePrivateMethodWithoutParams()
        {
            var testClass = new TestClass("", "");
            testClass.Method("MyClassName").Invoke<string>().Should().Be("MyTestClass");
        }

        [Test]
        public void ItShouldInvokeInstancePrivateMethodWithoutParamsWhenInstanceIsCastAsAnObject()
        {
            var testClass = new TestClass("", "");
            ((object)testClass).Method("MyClassName").Invoke<string>().Should().Be("MyTestClass");
        }

        [Test]
        public void ItShouldInvokeStaticVoidPublicMethodWithParams()
        {
            try
            {
                var date = DateTime.Now;
                typeof(TestClass).Method("VoidCall").That(Is.Public).WithParam(date).Invoke();
                TestClass.VoidCalledAt.Should().Be(date);
            }
            finally
            {
                TestClass.VoidCalledAt = DateTime.MinValue;
            }
        }

        [Test]
        public void ItShouldInvokeStaticVoidMethodWithParams()
        {
            try
            {
                var date = DateTime.Now;
                typeof(TestClass).Method("VoidCall").WithParam(date).Invoke();
                TestClass.VoidCalledAt.Should().Be(date);
            }
            finally
            {
                TestClass.VoidCalledAt = DateTime.MinValue;
            }
        }

        [Test]
        public void ItShouldRaiseAMethodNotFoundExceptionIfTheMethodDoesNotExist()
        {
            Action action = () => typeof(TestClass).Method("Boo").Invoke();
            action.ShouldThrow<MethodNotFoundException>();
        }
    }
}