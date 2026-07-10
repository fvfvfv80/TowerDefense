using System.Collections.Generic;
using UnityEngine;

public class ComponentCacher
{
    private Transform _transform;
    private Dictionary<string, Component> _cachedComponents = new();

    public ComponentCacher(Transform transform)
    {
        _transform = transform;
    }


    public T GetCachedCompoent<T>() where T : Component
    {
        _cachedComponents.TryGetValue(nameof(T), out var component);

        if (component == null)
        {
            component = _transform.GetComponent<T>();
            _cachedComponents[nameof(T)] = component;
        }

        return component as T;
    }
}
