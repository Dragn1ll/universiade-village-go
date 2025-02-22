namespace DIContainer;

public class DiContainer
{
    private readonly Dictionary<Type, Implementation> _registrations = new();

    public void RegisterTransient<TInterface, TImplementation>() where TImplementation : TInterface
    {
        _registrations[typeof(TInterface)] = new Implementation(typeof(TImplementation), Lifetime.Transient);
    }

    public object Resolve(Type type)
    {
        if (!_registrations.TryGetValue(type, out var implementation))
        {
            throw new InvalidOperationException($"No registration for type {type}");
        }

        switch (implementation.Lifetime)
        {
            case Lifetime.Transient:
                return ResolveTransient(implementation);
            default:
                throw new InvalidOperationException($"No lifetime for type {type}");
        }
    }

    private object ResolveTransient(Implementation implementation)
    {
        var constructor = implementation.Type.GetConstructors().First();
        var parameters = constructor.GetParameters();
        
        var resolvedParameters = parameters.Select(p => Resolve(p.ParameterType)).ToArray();
        
        return constructor.Invoke(resolvedParameters);
    }
}