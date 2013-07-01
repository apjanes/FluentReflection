namespace FluentReflection.Specs
{
    public interface IMethodResultSpec
    {
        T Invoke<T>();
        void Invoke();
        bool Exists { get; }
    }
}