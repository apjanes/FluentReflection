using System;

namespace FluentReflection.Specs
{
    /// <summary>
    /// A specification for adding parameters to the current specification.
    /// Addition of paramters allows the invokation of constructors or
    /// methods that have parameters specified.
    /// </summary>
    /// <typeparam name="T">The type to return after adding parameters.</typeparam>
    public interface IParamSpec<T>
    {
        /// <summary>
        /// Add an arbitary number of parameters.  The parameter types
        /// are taken from the specific types in the array.  If the 
        /// parameter types required are not specifically those in the
        /// array, use one of the generic WithParams methods 
        /// (eg. <see cref="WithParams&lt;TParam&gt;(TParam)"/>) or 
        /// <see cref="WithParams(Type[], object[])"/>.
        /// </summary>
        /// <param name="types">The types of the parameters in the method or constructor.</param>
        /// <param name="parameters">The instances to pass to the method or constructor.</param>
        /// <returns>Another spec specified by T.</returns>
        T WithParams(Type[] types, object[] parameters);
        T WithParams(params object[] parameters);
        T WithParam<TParam>(TParam parameter);
        T WithParams<TOne, TTwo>(TOne paramOne, TTwo paramTwo);
        T WithParams<TOne, TTwo, TThree>(TOne paramOne, TTwo paramTwo, TThree paramThree);
    }
}