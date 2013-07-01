namespace FluentReflection.Specs
{
    public interface IThatSpec<T>
    {
        T That(Is binding);
    }
}