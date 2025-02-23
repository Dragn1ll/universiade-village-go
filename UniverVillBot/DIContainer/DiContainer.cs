namespace DIContainer;

public class DiContainer
{
    private readonly Dictionary<Type, Implementation> _registrations = new();
    private readonly Dictionary<Type, object?> _singletons = new();
    private readonly object _singletonLock = new();

    public void RegisterTransient<TInterface, TImplementation>() where TImplementation : TInterface
    {
        _registrations[typeof(TInterface)] = new Implementation(typeof(TImplementation), Lifetime.Transient);
    }

    public void RegisterSingleton<TInterface, TImplementation>() where TImplementation : TInterface
    {
        _registrations[typeof(TInterface)] = new Implementation(typeof(TImplementation), Lifetime.Singleton);
        lock (_singletonLock)
        {
            _singletons[typeof(TImplementation)] = null;
        }
    }

    public void RegisterScoped<TInterface, TImplementation>() where TImplementation : TInterface
    {
        _registrations[typeof(TInterface)] = new Implementation(typeof(TImplementation), Lifetime.Scoped);
    }
    
    public TInterface Resolve<TInterface>(Scope? scope = null)
    {
        return (TInterface)Resolve(typeof(TInterface), scope);
    }

    private object Resolve(Type type, Scope? scope = null)
    {
        if (!_registrations.TryGetValue(type, out var implementation))
        {
            throw new InvalidOperationException($"No registration for type {type}");
        }

        return implementation.Lifetime switch
        {
            Lifetime.Transient => ResolveTransient(implementation.Type),
            Lifetime.Singleton => ResolveSingleton(implementation.Type),
            Lifetime.Scoped => ResolveScoped(implementation.Type, scope),
            _ => throw new InvalidOperationException($"No lifetime for type {type}")
        };
    }

    internal object ResolveTransient(Type type)
    {
        var constructor = type.GetConstructors().First();
        var parameters = constructor.GetParameters();
        
        var resolvedParameters = parameters.Select(p => Resolve(p.ParameterType)).ToArray();
        
        return constructor.Invoke(resolvedParameters);
    }

    private object ResolveSingleton(Type type)
    {
        lock (_singletonLock)
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

    private object ResolveScoped(Type type, Scope? scope)
    {
        if (scope == null)
        {
            throw new ArgumentException("No scope");
        }
        
        return scope.Resolve(type);
    }
    
    public Scope CreateScope()
    {
        return new Scope(this);
    }
}