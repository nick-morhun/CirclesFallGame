using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creates objects on demand, filled at least to Configuration.Instance.ObjectsCacheBufferSize.
/// </summary>
/// <typeparam name="T"></typeparam>
public class Pull<T> where T : Component
{
    private T prefab;

    private Queue<T> store = new Queue<T>();

    public Pull(T prefab)
    {
        if (!prefab)
        {
            Debug.LogError("Pull.Pull(): prefab is null");
            return;
        }

        this.prefab = prefab;
    }

    public void Put(T obj)
    {
        store.Enqueue(obj);
        //Debug.Log("Pull.Put() count = " + store.Count);
    }

    /// <summary>
    /// Returns an object from pull if there is at least Configuration.Instance.ObjectsCacheBufferSize
    /// objects in it or creates a new object.
    /// </summary>
    /// <returns></returns>
    public T Get()
    {
        //Debug.Log("Pull.Get() count = " + store.Count);
        if (store.Count > Configuration.Instance.ObjectsCacheBufferSize)
        {
            return store.Dequeue();
        }
        else
        {
            var component = GameObject.Instantiate<T>(prefab);
            Debug.LogWarning("Pull.Get(): a new object created");
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
