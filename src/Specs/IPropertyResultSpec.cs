namespace FluentReflection.Specs
{
    public interface IPropertyResultSpec: IFieldResultSpec
    {
        TResult GetValue<TResult>(params object[] index);
        object GetValue(params object[] index);
        void SetValue(object value, params object[] index);
    }
}