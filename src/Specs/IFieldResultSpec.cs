namespace FluentReflection.Specs
{
    public interface IFieldResultSpec
    {
        TResult GetValue<TResult>();
        object GetValue();
        void SetValue(object value);
        bool Exists { get; }
    }
}