using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : CompositeRoot where T : Component
{
    [SerializeField] private T _object;
    [SerializeField] private int _amount;
    [SerializeField] private Transform _container;

    private List<T> _pooledObjects = new();

    public override void Compose()
    {
        for (int i = 0; i < _amount; i++)
            IncreasePool();
    }

    public T GetPooledObject()
    {
        T result = _pooledObjects.Find(item => item.gameObject.activeInHierarchy == false);
        if (result != null)
            return result;

        return null;
    }

    private T IncreasePool()
    {
        T newObject = Instantiate(_object, _container);
        newObject.gameObject.SetActive(false);
        _pooledObjects.Add(newObject);
        return newObject;
    }
}