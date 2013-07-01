using System;
using FluentAssertions;
using NUnit.Framework;

namespace FluentReflection.Tests
{
    [TestFixture]
    public class FieldSpecFixture
    {
        private TestClass _testClass;

        [SetUp]
        public void SetUp()
        {
            _testClass = new TestClass("Aaron", "Janes");
        }


        [Test]
        public void ExistsShouldReturnFalseForAnNonExistingField()
        {
            typeof(TestClass).Field("xxxx").Exists.Should().BeFalse();
        }

        [Test]
        public void ExistsShouldReturnTrueForAnExistingField()
        {
            new TestClass("", "").Field("_firstName").Exists.Should().BeTrue();
        }


        [Test]
        public void GetValueShouldReturnTheCorrectValueForAnInstanceNonPublicFieldByDefault()
        {
            _testClass.Field("_firstName").GetValue<string>().Should().Be("Aaron");
        }
        
        [Test]
        public void GetValueShouldReturnTheCorrectValueForAnInstancePublicFieldByDefault()
        {
            _testClass.Field("LastName").GetValue<string>().Should().Be("Janes");
        }

        [Test]
        public void GetValueShouldReturnTheCorrectValueForAnInstancePublicFieldWhenIsPublic()
        {
            _testClass.Field("LastName").That(Is.Public).GetValue<string>().Should().Be("Janes");
        }

        [Test]
        public void GetValueShouldReturnTheCorrectValueForANonPublicInstanceFieldInASuperClass()
        {
            var subTestClass = new SubTestClass("Boba", "Fett");
            subTestClass.Field<TestClass>("_firstName").GetValue<string>().Should().Be("Boba");
        }


        [Test]
        public void SetValueShouldSetTheValueCorrectly()
        {
            _testClass.Field("_firstName").SetValue("Caroline");
            _testClass.FirstName.Should().Be("Caroline");
        }

        [Test]
        public void ItShouldThrowAFieldNotFoundExceptionWhenGettingAnInvalidField()
        {
            var action = new Action(()=> _testClass.Field("boo").GetValue<string>());
            action.ShouldThrow<FieldNotFoundException>().Where(x => x.MemberName == "boo" && x.MemberType == _testClass.GetType());
        }

        [Test]
        public void ItShouldThrowAFieldNotFoundExceptionWhenSettingAnInvalidField()
        {
            var action = new Action(()=> _testClass.Field("boo").SetValue(""));
            action.ShouldThrow<FieldNotFoundException>().Where(x => x.MemberName == "boo" && x.MemberType == _testClass.GetType());
        }

        [Test]
        public void ItShouldThrowAnInvalidCastForAInvalidType()
        {
            var action = new Action(() => _testClass.Field("_firstName").GetValue<int>());
            action.ShouldThrow<InvalidCastException>();
        }


    }
}