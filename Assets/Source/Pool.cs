using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creates objects on demand, filled at least to Configuration.Instance.ObjectsPoolBufferSize.
/// </summary>
/// <typeparam name="T"></typeparam>
public class Pool<T> where T : Component
{
    private T prefab;

    private Queue<T> store = new Queue<T>();

    public Pool(T prefab)
    {
        if (!prefab)
        {
            Debug.LogError("Pool.Pool(): prefab is null");
            return;
        }

        this.prefab = prefab;
    }

    public void Put(T obj)
    {
        store.Enqueue(obj);
        //Debug.Log("Pool.Put() count = " + store.Count);
    }

    /// <summary>
    /// Returns an object from Pool if there is at least Configuration.Instance.ObjectsCacheBufferSize
    /// objects in it or creates a new object.
    /// </summary>
    /// <returns></returns>
    public T Get()
    {
        //Debug.Log("Pool.Get() count = " + store.Count);
        if (store.Count > Configuration.Instance.ObjectsPoolBufferSize)
        {
            return store.Dequeue();
        }
        else
        {
            var component = GameObject.Instantiate<T>(prefab);
            Debug.LogWarning("Pool.Get(): a new object created");
            return component;
        }
    }

    public void Clear()
    {
        foreach (var item in store)
        {
            Object.Destroy(item.gameObject);
        }

        store.Clear();
    }
}
