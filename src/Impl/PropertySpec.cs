using System;
using System.Reflection;
using FluentReflection.Specs;

namespace FluentReflection.Impl
{
    internal class PropertySpec: IPropertySpec
    {
        private readonly string _name;
        private readonly object _instance;
        private BindingFlags _flags;
        private Type _type;

        internal PropertySpec(Type type, string name, object instance)
        {
            _name = name;
            _instance = instance;
            _type = type;
            _flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        }

        public bool Exists
        {
            get { return GetInfo() != null; }
        }

        public IPropertyResultSpec That(Is binding)
        {
            _flags = binding.Flags;
            return this;
        }

        public object GetValue()
        {
            return GetValue(new object[0]);
        }

        public TResult GetValue<TResult>()
        {
            return GetValue<TResult>(new object[0]);
        }

        public void SetValue(object value)
        {
            SetValue(value, new object[0]);
        }

        public TResult GetValue<TResult>(params object[] index)
        {
            return (TResult)GetValue(index);
        }

        public object GetValue(params object[] index)
        {
            var info = GetInfo();
            if (info == null) throw new PropertyNotFoundException(_type, _name, _flags);
            return info.GetValue(_instance, index);
        }

        public void SetValue(object value, params object[] index)
        {
            var info = GetInfo();
            if (info == null) throw new PropertyNotFoundException(_type, _name, _flags);
            info.SetValue(_instance, value, index);
        }

        private PropertyInfo GetInfo()
        {
            if (!_flags.HasFlag(BindingFlags.Instance) && !_flags.HasFlag(BindingFlags.Static))
                _flags = _flags | BindingFlags.Instance;
            var name = _name == "this" ? "Item" : _name;
            var info = _type.GetProperty(name, _flags);
            return info;
        }
    }
}