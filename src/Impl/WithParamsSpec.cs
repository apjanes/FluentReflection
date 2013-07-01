using System;
using System.Linq;
using FluentReflection.Specs;

namespace FluentReflection.Impl
{
    internal abstract class WithParamsSpec<T>
    {
        protected Type[] ParamTypes { get; set; }
        protected object[] ParamInstances { get; set; }

        protected WithParamsSpec()
        {
            ParamTypes = new Type[0];
            ParamInstances = new object[0];
        }
        public T WithParams(Type[] types, object[] parameters)
        {
            ParamTypes = types;
            ParamInstances = parameters;
            return (T)(object)this;
        }

        public T WithParams(params object[] parameters)
        {
            return WithParams(parameters.Select(x => x.GetType()).ToArray(), parameters);
        }

        public T WithParam<TParam>(TParam parameter)
        {
            return WithParams(new[] { typeof(TParam) }, new object[] { parameter });
        }

        public T WithParams<TOne, TTwo>(TOne paramOne, TTwo paramTwo)
        {
            return WithParams(new[] { typeof(TOne), typeof(TTwo) }, new object[] { paramOne, paramTwo });
        }

        public T WithParams<TOne, TTwo, TThree>(TOne paramOne, TTwo paramTwo, TThree paramThree)
        {
            return WithParams(new[] { typeof(TOne), typeof(TTwo), typeof(TThree) }, new object[] { paramOne, paramTwo, paramThree });
        }
    }
}