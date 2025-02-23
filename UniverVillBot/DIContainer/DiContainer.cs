namespace DIContainer;

public class DiContainer
{
    private readonly Dictionary<Type, Implementation> _registrations = new();
    private readonly Dictionary<Type, object?> _singletons = new();

    public void RegisterTransient<TInterface, TImplementation>() where TImplementation : TInterface
    {
        _registrations[typeof(TInterface)] = new Implementation(typeof(TImplementation), Lifetime.Transient);
    }

    public void RegisterSingleton<TInterface, TImplementation>() where TImplementation : TInterface
    {
        _registrations[typeof(TInterface)] = new Implementation(typeof(TImplementation), Lifetime.Singleton);
        _singletons[typeof(TImplementation)] = null;
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
                return ResolveTransient(implementation.Type);
            case Lifetime.Singleton:
                return ResolveSingleton(implementation.Type);
            default:
                throw new InvalidOperationException($"No lifetime for type {type}");
        }
    }

    private object ResolveTransient(Type type)
    {
        var constructor = type.GetConstructors().First();
        var parameters = constructor.GetParameters();
        
        var resolvedParameters = parameters.Select(p => Resolve(p.ParameterType)).ToArray();
        
        return constructor.Invoke(resolvedParameters);
    }

    private object ResolveSingleton(Type type)
    {
        if (!_singletons.TryGetValue(type, out var implementation))
        {
            throw new InvalidOperationException($"No singleton for type {type}");
        }

        if (implementation != null)
        {
            return implementation;
        }
        
        var instance = ResolveTransient(type);
        _singletons[type] = instance;
        
        return instance;
    }
}