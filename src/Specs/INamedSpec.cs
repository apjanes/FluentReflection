namespace FluentReflection.Specs
{
    public interface INamedSpec<T, TThatSpec> where TThatSpec : IThatSpec<T>
    {
        TThatSpec Named(string name);
    }
}