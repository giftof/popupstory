using System;
using System.Collections.Generic;
using UnityEngine;
using Popup.Defines;
using Popup.Configs;
using Popup.Framework;


public class ObjectPool : MonoBehaviour
{

    public static ObjectPool Instance = null;
    private readonly Dictionary<Prefab, Queue<GameObject>> pool = new Dictionary<Prefab, Queue<GameObject>>();
    [SerializeField] private List<GameObject> prefabList = new List<GameObject>();

    void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else
        {
            Instance = this;
            foreach (string name in Enum.GetNames(typeof(Prefab)))
                prefabList.Add(Resources.Load<GameObject>($"{Path.prefab}{name}"));
            foreach (Prefab value in Enum.GetValues(typeof(Prefab)))
                pool.Add(value, new Queue<GameObject>());
        }
    }

    private T Get<T>(Prefab type) where T: MonoBehaviour
        => pool[type].Count.Equals(0)
        ? Instantiate(prefabList[(int)type], Vector3.zero, Quaternion.identity).GetComponent<T>()
        : pool[type].Dequeue().GetComponent<T>();

    public void Release(Prefab type, GameObject obj)
    {
        pool[type].Enqueue(obj);
        obj.transform.SetParent(transform);
        obj.SetActive(false);
    }

    /********************************/
    /* Get Variations               */
    /********************************/

    public T Get<T>(Prefab type, Transform parent) where T: MonoBehaviour
    {
        T obj = Get<T>(type);
        obj.gameObject.SetActive(true);
        obj.transform.SetParent(parent);
        obj.transform.localScale = Vector3.one;
        obj.transform.localPosition = Vector3.zero;
        return obj;
    }

    public T Get<T>(Prefab type, Transform parent, Vector3 localPosition) where T: MonoBehaviour
    {
        T obj = Get<T>(type, parent);
        obj.transform.localPosition = localPosition;
        return obj;
    }

    public T Get<T>(Prefab type, Transform parent, Vector3 localPosition, Vector2 sizeDelta) where T: MonoBehaviour
    {
        T obj = Get<T>(type, parent, localPosition);
        obj.GetComponent<RectTransform>().sizeDelta = sizeDelta;
        return obj;
    }
}