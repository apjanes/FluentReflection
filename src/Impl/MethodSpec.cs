using System;
using System.Reflection;
using FluentReflection.Specs;

namespace FluentReflection.Impl
{
    internal class MethodSpec: WithParamsSpec<IMethodResultSpec>, IMethodSpec
    {
        private readonly Type _type;
        private readonly string _name;
        private readonly object _instance;
        private BindingFlags _flags;

        public MethodSpec(Type type, string name, object instance)
        {
            if (type == null) throw new ArgumentNullException("type");
            _type = type;
            _name = name;
            _instance = instance;
            _flags = BindingFlags.Public | BindingFlags.NonPublic;
        }

        public bool Exists
        {
            get { return GetInfo() != null; }
        }

        public T Invoke<T>()
        {
            return (T) DoInvoke();
        }

        public void Invoke()
        {
            DoInvoke();
        }

        public IMethodParamSpec That(Is binding)
        {
            _flags = binding.Flags;
            return this;
        }

        private object DoInvoke()
        {
            var info = GetInfo();
            var instances = ParamInstances ?? new object[0];
            if(info == null)
                throw new MethodNotFoundException(_type, _name, _flags);
            return info.Invoke(_instance, instances);
        }

        private MethodInfo GetInfo()
        {
            var typeFlag = _instance == null ? BindingFlags.Static : BindingFlags.Instance;
            if (!_flags.HasFlag(typeFlag)) _flags = _flags | typeFlag;
            return _type.GetMethod(_name, _flags, null, ParamTypes, null); 
        }
    }
}