using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Popup.Defines;
using Popup.Framework;
using Popup.Configs;



public class ObjectPools : MonoBehaviour
{
    Dictionary<ObjectType, LinkedList<object>> pool;
    

    // Start is called before the first frame update
    void Start()
    {
        pool = new Dictionary<ObjectType, LinkedList<object>>();
    }

    public LinkedList<object> Request(ObjectType type, uint amount)
    {
        // List<object> request = new List<object>();

        if (pool.ContainsKey(type))
            return Pop(pool[type], type, amount);
        
        return Pop(pool[type], type, amount);
    }

    private void Fill(LinkedList<object> dest, ObjectType type, uint amount)
    {
        if (dest.Count < amount)
        {
            for (int i = dest.Count; i < amount + Configs.extraPoolSize; ++i)
            {
                GameObject obj = Instantiate(Resources.Load<GameObject>($"Prefabs/{Enum.GetName(typeof(ObjectType), type)}"));
                obj.SetActive(false);
                dest.AddFirst(obj);
                // dest.Add(Instantiate(Resources.Load<GameObject>($"Prefabs/{Enum.GetName(typeof(ObjectType), type)}")).SetActive(false));
            }
        }
    }

    private LinkedList<object> Pop(LinkedList<object> source, ObjectType type, uint amount)
    {
        LinkedList<object> pop = new LinkedList<object>();

        Fill(source, type, amount);

        while (0 < amount--)
        {
            pop.AddFirst(source.First);
            source.RemoveFirst();
        }
        
        return pop;
    }

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
