using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Popup.Defines;
using Popup.Framework;
using Popup.Configs;
using Popup.Library;



public partial class ObjectPool : MonoBehaviour
{
    Dictionary<Prefab, Queue<GameObject>> pool;
    //Dictionary<Prefab, Dictionary<int, GameObject>> credit;

    public static ObjectPool Instance = null;

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
        if (Instance != null)
            Destroy(this);
        Instance = this;
        pool = new Dictionary<Prefab, Queue<GameObject>>();
        BuildContainer();
        PrePooling();
    }

    public List<GameObject> Request(Prefab type, uint amount)
    {
        return pool.ContainsKey(type) ? Pop(pool[type], type, amount) : null;
    }

    public GameObject Request(Prefab type)
    {
        return pool.ContainsKey(type) ? Pop(pool[type], type, 1)[0] : null;
    }

    public void Return(Prefab type, GameObject obj)
    {
        if (pool.ContainsKey(type))
        {
            pool[type].Enqueue(obj);
            obj.transform.SetParent(transform);
            obj.SetActive(false);
        }
    }
}


public partial class ObjectPool
{
    public void ClearContainer()
    {
        foreach (KeyValuePair<Prefab, Queue<GameObject>> pair in pool)
            pair.Value.Clear();
    }

    private void PrePooling()
    {
        //ClearContainer();

        foreach (KeyValuePair<Prefab, Queue<GameObject>> pair in pool)
            Fill(pair.Value, pair.Key, Config.extraPoolSize);
    }

    private List<GameObject> Pop(Queue<GameObject> source, Prefab type, uint amount)
    {
        List<GameObject> list = new List<GameObject>();

        Fill(source, type, amount);
        while (0 < amount--) list.Add(source.Dequeue());
        Spare(source, type, amount);
        return list;
    }

    private void BuildContainer()
    {
        foreach (Prefab element in Enum.GetValues(typeof(Prefab)))
        {
            pool.Add(element, new Queue<GameObject>());
        }
    }

    private bool Fill(Queue<GameObject> dest, Prefab type, uint amount)
    {
        Debug.Log("Fill");
        if (dest.Count < amount)
            MakeExtra(dest, type, amount - (uint)dest.Count);

        return true;
    }

    private void MakeExtra(Queue<GameObject> source, Prefab type, uint amount)
    {
        while (0 < amount--)
        {
            GameObject obj = Instantiate(prefab[(int)type], Vector3.zero, Quaternion.identity);
            obj.transform.SetParent(transform);
            obj.SetActive(false);
            source.Enqueue(obj);
        }
    }

    private void Spare(Queue<GameObject> source, Prefab type, uint amount)
    {
        if (source.Count < amount + Config.extraPoolSize)
        {
            StartCoroutine(MakeSlowly(source, type, amount));
        }
    }

    IEnumerator MakeSlowly(Queue<GameObject> source, Prefab type, uint amount)
    {
        while (0 < amount--)
        {
            yield return null;
            GameObject obj = Instantiate(prefab[(int)type], Vector3.zero, Quaternion.identity);
            obj.transform.SetParent(transform);
            obj.SetActive(false);
            source.Enqueue(obj);
        }
    }

    //private bool ToCredit(ObjectType type, object obj)
    //{
    //    if (credit.ContainsKey(type))
    //    {
    //        credit[type].Enqueue(obj);
    //        return true;
    //    }
    //    return false;
    //}
}
