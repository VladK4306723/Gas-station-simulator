using System;
using System.Collections.Generic;

public sealed class ServiceContainer
{
    private readonly Dictionary<Type, object> _services = new();

    public void Register<T>(T instance) where T : class
    {
        _services[typeof(T)] = instance;
    }

    public T Resolve<T>() where T : class
    {
        return (T)_services[typeof(T)];
    }

    public bool TryResolve<T>(out T instance) where T : class
    {
        if (_services.TryGetValue(typeof(T), out var o))
        {
            instance = (T)o;
            return true;
        }
        instance = null;
        return false;
    }
}
