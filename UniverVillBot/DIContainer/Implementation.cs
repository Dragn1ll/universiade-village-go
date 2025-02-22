namespace DIContainer;

internal class Implementation(Type implementationType, Lifetime lifetime)
{
    internal Type Type { get; } = implementationType;
    internal Lifetime Lifetime { get; } = lifetime;
}