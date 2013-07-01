namespace FluentReflection.Specs
{
    public interface IConstructorResultSpec: IResultSpec
    {
        T Construct<T>();
    }
}