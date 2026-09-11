using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Component
{
    private List<T> pool = new List<T>();

    private GameObject aoeObject_;
    public ObjectPool(int initialSize, GameObject aoeObject)
    {
        aoeObject_ = aoeObject;
        for (int i = 0; i < initialSize; i++)
        {
            CreateNew();
        }
    }

    private T CreateNew()
    {
        T obj = Object.Instantiate(aoeObject_).GetComponent<T>();
        obj.gameObject.SetActive(false);
        pool.Add(obj);
        return obj;
    }

    public T Get()
    {
        foreach (var p in pool)
        {
            if (!p.gameObject.activeInHierarchy)
            {
                return p;
            }
        }

        // ‘«‚è‚È‚¯‚ê‚Î’Ç‰Á
        return CreateNew();
    }
}

