using System;
using FluentAssertions;
using NUnit.Framework;

namespace FluentReflection.Tests
{
    [TestFixture]
    public class PropertySpecFixture
    {
        private TestClass _testClass;

        [SetUp]
        public void SetUp()
        {
            _testClass = new TestClass("Aaron", "Janes");
        }

        [Test]
        public void ExistsShouldReturnFalseForAnNonExistingProperty()
        {
            typeof(TestClass).Property("xxxx").Exists.Should().BeFalse();
        }

        [Test]
        public void ExistsShouldReturnTrueForAnExistingProperty()
        {
            new TestClass("", "").Property("FirstName").Exists.Should().BeTrue();
        }

        [Test]
        public void GetValueShouldReturnTheCorrectValueForAnInstanceNonPublicFieldByDefault()
        {
            _testClass.Property("Dob").GetValue<DateTime>().Should().Be(new DateTime(1978, 03, 31));
        }

        [Test]
        public void GetValueShouldReturnTheCorrectValueForAnIndexedProperty()
        {
            _testClass.Property("Item").That(Is.Public).GetValue<string>("FN").Should().Be(_testClass.FirstName);
        }

        [Test]
        public void GetValueShouldReturnTheCorrectValueForAnIndexedPropertyNamedThis()
        {
            _testClass.Property("this").That(Is.Public).GetValue<string>("FN").Should().Be(_testClass.FirstName);
        }

        [Test]
        public void GetValueShouldReturnTheCorrectValueForAnInstancePublicFieldWhenIsPublic()
        {
            _testClass.Property("Age").That(Is.Public).GetValue<int>().Should().Be(33);
        }

        [Test]
        public void SetValueShouldSetTheValueCorrectly()
        {
            var dateTime = new DateTime(2011,01,01);
            _testClass.Property("Dob").SetValue(dateTime);
            _testClass.SetDob.Should().Be(dateTime);
        }
        
        [Test]
        public void SetValueShouldSetTheValueCorrectlyForAnIndexedProperty()
        {
            _testClass.Property("Item").That(Is.Public).SetValue("Blah", "LN");
            _testClass.LastName.Should().Be("Blah");
        }
        
        [Test]
        public void SetValueShouldSetTheValueCorrectlyForAnIndexedPropertyCalledName()
        {
            _testClass.Property("this").That(Is.Public).SetValue("Blah", "LN");
            _testClass.LastName.Should().Be("Blah");
        }

        [Test]
        public void ItShouldThrowAPropertyNotFoundExceptionWhenGettingAnInvalidProperty()
        {
            var action = new Action(()=> _testClass.Property("boo").GetValue<string>());
            action.ShouldThrow<PropertyNotFoundException>().Where(x => x.MemberName == "boo" && x.MemberType == _testClass.GetType());
        }

        [Test]
        public void ItShouldThrowAPropertyNotFoundExceptionWhenSettingAnInvalidProperty()
        {
            var action = new Action(()=> _testClass.Property("boo").SetValue("x"));
            action.ShouldThrow<PropertyNotFoundException>().Where(x => x.MemberName == "boo" && x.MemberType == _testClass.GetType());
        }
        
        [Test]
        public void ItShouldThrowAnInvalidCastForAInvalidType()
        {
            var action = new Action(() => _testClass.Property("Dob").GetValue<int>());
            action.ShouldThrow<InvalidCastException>();
        }
    }
}