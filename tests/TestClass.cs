using System;

namespace FluentReflection.Tests
{
    public class TestClass
    {
// ReSharper disable FieldCanBeMadeReadOnly.Local
        private string _firstName;
// ReSharper restore FieldCanBeMadeReadOnly.Local
        public string LastName;
        private DateTime? _dob;

        public TestClass(string firstName, string lastName)
        {
            _firstName = firstName;
            LastName = lastName;
        }

        private TestClass()
        {
            
        }

        public static DateTime VoidCalledAt { get; set; }

        public string this[string key]
        {
            get
            {
                switch(key)
                {
                    case "FN":
                        return _firstName;
                    case "LN":
                        return LastName;
                }
                return string.Empty;
            }

            set
            {
                switch (key)
                {
                    case "FN":
                        _firstName = value;
                        break;
                    case "LN":
                        LastName = value;
                        break;
                }
            }
        }

        public string FirstName
        {
            get { return _firstName; }
        }

        public DateTime? SetDob { get { return _dob; } }

        private DateTime Dob
        {
            get { return _dob ?? new DateTime(1978,03,31);}
            set { _dob = value; }
        }
        
        private static string ClassName()
        {
            return "TestClass";
        }
        
        private string MyClassName()
        {
            return "MyTestClass";
        }

        public static void VoidCall(DateTime at)
        {
            VoidCalledAt = at;
        }

        public int Age
        {
            get { return 33; }
        }
    }
}