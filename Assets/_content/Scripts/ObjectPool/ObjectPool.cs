using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : class
{
    private List<T> pool;

    public readonly IBuilder<T> builder;

    private int instanceCount;

    private int maxInstances;

    public ObjectPool(IBuilder<T> builder) : this(builder, int.MaxValue)
    {

    }

    public ObjectPool(IBuilder<T> builder, int maxInstances)
    {
        this.builder = builder;
        this.instanceCount = 0;
        this.maxInstances = maxInstances;
        this.pool = new List<T>();
    }

    public int Size => pool.Count;

    public int InstanceCount => instanceCount;

    public int MaxInstances
    {
        get => maxInstances;
        set { maxInstances = value; }
    }

    public T GetObject()
    {
        T thisObject = RemoveObject();
        if (thisObject != null)
            return thisObject;

        if (InstanceCount < MaxInstances)
            return CreateObject();

        return null;
    }

    private T RemoveObject()
    {
        if (pool.Count == 0)
            return null;
        T refObject = pool[0];
        pool.RemoveAt(0);
        instanceCount--;
        return refObject;
        //var refThis = (System.WeakReference)pool[pool.Count - 1];
    }

    private T CreateObject()
    {
        T newObject = builder.Build();
        instanceCount++;
        return newObject;
    }

    public void Release(T obj)
    {
        if (obj == null)
            throw new System.NullReferenceException();
        pool.Add(obj);
    }

}
