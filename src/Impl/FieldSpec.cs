using System;
using System.Reflection;
using FluentReflection.Specs;

namespace FluentReflection.Impl
{
    internal class FieldSpec: IFieldSpec
    {
        private readonly string _name;
        private readonly object _instance;
        private BindingFlags _flags;
        private Type _type;

        internal FieldSpec(Type type, string name, object instance)
        {
            _name = name;
            _instance = instance;
            _flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            _type = type;
        }

        public bool Exists
        {
            get { return GetInfo() != null; }
        }

        public IFieldResultSpec That(Is binding)
        {
            _flags = binding.Flags;
            return this;
        }

        public TResult GetValue<TResult>()
        {
            return (TResult) GetValue();
        }

        public object GetValue()
        {
            var info = GetInfo();
            if(info == null) throw new FieldNotFoundException(_type, _name, BindingFlags.Default);
            return info.GetValue(_instance);
        }

        public void SetValue(object value)
        {
            var info = GetInfo();
            if(info == null) throw new FieldNotFoundException(_type, _name, BindingFlags.Default);
            info.SetValue(_instance, value);
        }

        private FieldInfo GetInfo()
        {
            if (!_flags.HasFlag(BindingFlags.Instance) && !_flags.HasFlag(BindingFlags.Static))
                _flags = _flags | BindingFlags.Instance;
            var info = _type.GetField(_name, _flags);
            return info;
        }
    }
} 