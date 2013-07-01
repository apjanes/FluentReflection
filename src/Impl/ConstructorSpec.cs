using System;
using System.Linq;
using System.Reflection;
using FluentReflection.Specs;

namespace FluentReflection.Impl
{
    internal class ConstructorSpec: WithParamsSpec<IConstructorResultSpec>, IConstructorSpec
    {
        private readonly Type _type;
        private BindingFlags _flags;

        public ConstructorSpec(Type type)
        {
            if (type == null) throw new ArgumentNullException("type");
            _type = type;
            _flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        }


        public IConstructorParamSpec That(Is binding)
        {
            _flags = binding.Flags;
            return this;
        }

        public bool Exists
        {
            get
            {
                return GetInfo() != null; 
            }
        }

        public T Construct<T>()
        {
            var instances = ParamInstances ?? new object[0];
            var constructor = GetInfo();
            if (constructor == null)
                throw new ConstructorNotFoundException(_type, null, _flags);
            return (T)constructor.Invoke(instances);
        }

        private ConstructorInfo GetInfo()
        {
            if (!_flags.HasFlag(BindingFlags.Instance) && !_flags.HasFlag(BindingFlags.Static))
                _flags = _flags | BindingFlags.Instance;
            var types = ParamTypes ?? new Type[0];
            var constructor = _type.GetConstructor(_flags, null, types, null);
            return constructor;
        }

    }
}