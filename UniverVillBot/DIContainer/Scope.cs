namespace DIContainer;

public class Scope(DiContainer container) : IDisposable
{
    private readonly Dictionary<Type, object> _scopedInstance = new();
    private readonly object _lock = new();
    private bool _disposed;

    internal object Resolve(Type type)
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(Scope), "Scope has been disposed.");
        }

        lock (_lock)
        {
            if (!_scopedInstance.ContainsKey(type))
            {
                _scopedInstance[type] = container.ResolveTransient(type);
            }
        
            return _scopedInstance[type];
        }
    }

    public void Dispose()
    {
        if (_disposed) return;
        lock (_lock)
        {
            foreach (var instance in _scopedInstance)
            {
                if (instance.Value is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }
            _scopedInstance.Clear();
            _disposed = true;
        }
    }
}