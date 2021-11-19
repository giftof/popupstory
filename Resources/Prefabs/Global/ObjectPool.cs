using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Popup.Defines;
using Popup.Framework;
using Popup.Configs;
using Popup.Library;



public class ObjectPool : MonoBehaviour
{
    Dictionary<ObjectType, Queue<object>> pool;
    Dictionary<ObjectType, Dictionary<int, object>> credit;

    public static ObjectPool Instance;

    [SerializeField]
    private GameObject[] prefab;

    //equipItem,
    //toolItem,
    //spell,
    //buff,
    //charactor,
    //inventory,
    //squad,
    //customButton,
    //testMesh,

    void Awake()
    {
        Instance = this;
        pool = new Dictionary<ObjectType, Queue<object>>();
        BuildContainer();
    }

    private bool Fill(Queue<object> dest, ObjectType type, uint amount)
    {
        if (dest.Count < amount)
            MakeExtra(dest, type, amount - (uint)dest.Count);

        return true;
    }

    private void MakeExtra(Queue<object> source, ObjectType type, uint amount)
    {
        while (0 < amount--)
        {
            GameObject obj = Instantiate(prefab[(int)type], Vector3.zero, Quaternion.identity);
            //GameObject obj = Instantiate(Resources.Load<GameObject>($"Prefabs/{type}"), Vector3.zero, Quaternion.identity);
            obj.SetActive(false);
            source.Enqueue(obj);
        }
    }

    //private void Spare(Queue<object> source, ObjectType type, uint amount)
    //{
    //    if (source.Count < amount + Configs.extraPoolSize)
    //    {
    //        Thread thread = new Thread(() => MakeExtra(source, type, Configs.extraPoolSize));
    //        thread.Start();
    //    }
    //}

    //private bool ToCredit(ObjectType type, object obj)
    //{
    //    if (credit.ContainsKey(type))
    //    {
    //        credit[type].Enqueue(obj);
    //        return true;
    //    }
    //    return false;
    //}

    private List<object> Pop(Queue<object> source, ObjectType type, uint amount)
    {
        Fill(source, type, amount);
        //Spare(source, type, amount);
        //return source.Where(e => e != null && 0 < amount-- && ToCredit(type, e)).ToList();
        return source.Where(e => e != null && 0 < amount--).ToList();
    }

    private void ClearContainer()
    {
        foreach (KeyValuePair<ObjectType, Queue<object>> pair in pool)
            pair.Value.Clear();
    }

    private void BuildContainer()
    {
        foreach (ObjectType element in Enum.GetValues(typeof(ObjectType)))
        {
            pool.Add(element, new Queue<object>());
        }
    }

    


    public void PrePooling()
    {
        ClearContainer();

        foreach (KeyValuePair<ObjectType, Queue<object>> pair in pool)
            Fill(pair.Value, pair.Key, Configs.extraPoolSize);
    }

    //public List<object> Request(ObjectType type, uint amount) => pool.ContainsKey(type) ? Pop(pool[type], type, amount) : null;
    //public object Request(ObjectType type) => pool.ContainsKey(type) ? Pop(pool[type], type, 1)[0] : null;

    public List<object> Request(ObjectType type, uint amount)
    {
        return pool.ContainsKey(type) ? Pop(pool[type], type, amount) : null;
    }
    public object Request(ObjectType type)
    {
        return pool.ContainsKey(type) ? Pop(pool[type], type, 1)[0] : null;
    }

    public void Return(ObjectType type, object obj)
    {
        if (pool.ContainsKey(type))
        {
            pool[type].Enqueue(obj);
        }
    }
}
