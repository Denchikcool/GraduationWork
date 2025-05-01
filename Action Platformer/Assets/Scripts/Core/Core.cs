using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Core : MonoBehaviour
{
    private readonly List<CoreComponent> _coreComponents = new List<CoreComponent>();

    private void Awake()
    {
        
    }

    public void UpdateLogic()
    {
        foreach (CoreComponent component in _coreComponents)
        {
            component.UpdateLogic();
        }
    }

    public void AddComponent(CoreComponent component)
    {
        if (!_coreComponents.Contains(component))
        {
            _coreComponents.Add(component);
        } 
    }

    public T GetCoreComponent<T>() where T : CoreComponent
    {
        var component = _coreComponents.OfType<T>().FirstOrDefault();

        if (component == null)
        {
            component = GetComponentInChildren<T>();
        }

        if (component == null)
        {
            Debug.LogWarning($"{typeof(T)} not found on {transform.parent.name}");
        }

        return component;
    }

    public T GetCoreComponent<T>(ref T value) where T : CoreComponent
    {
        value = GetCoreComponent<T>();
        return value;
    }
}
