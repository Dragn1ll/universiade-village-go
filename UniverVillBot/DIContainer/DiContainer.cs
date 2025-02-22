namespace DIContainer;

public class DiContainer
{
    private readonly Dictionary<Type, Type> _registrations = new();

    public void Register<TInterface, TImplementation>() where TImplementation : TInterface
    {
        _registrations[typeof(TInterface)] = typeof(TImplementation);
    }

    public object Resolve(Type type)
    {
        if (!_registrations.TryGetValue(type, out var implementationType))
        {
            throw new InvalidOperationException($"No registration for type {type}");
        }

        var constructor = implementationType.GetConstructors().First();
        var parameters = constructor.GetParameters();
        
        var resolvedParameters = parameters.Select(p => Resolve(p.ParameterType)).ToArray();
        
        return constructor.Invoke(resolvedParameters);
    }
}