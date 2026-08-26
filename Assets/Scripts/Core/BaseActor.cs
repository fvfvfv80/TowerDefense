using System.Collections.Generic;
using UnityEngine;

public class BaseActor : MonoBehaviour
{
    private Dictionary<string, Component> _actorComponents = new();

    private Dictionary<string, ComponentCacher> _transformComponents = new();

    public void RegisterActorComponent(string key, Component component)
    {
        _actorComponents[key] = component;
    }

    public T GetActorCompoent<T>(string key) where T : Component
    {
        _actorComponents.TryGetValue(key, out var component);

        if (component == null)
        {

        }

        return component as T;
    }

    public ComponentCacher RegisterTransformComponent(string transformName, Transform transform)
    {
        return _transformComponents[transformName] = new ComponentCacher(transform);
    }

    public ComponentCacher GetComponentCacher(string transformName)
    {
        _transformComponents.TryGetValue(transformName, out var cacher);

        return cacher;
    }

}
