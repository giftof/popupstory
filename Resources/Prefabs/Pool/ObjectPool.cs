using System;
using System.Collections.Generic;
using UnityEngine;
using Popup.Defines;
using Popup.Configs;



public partial class ObjectPool : MonoBehaviour {

    public static ObjectPool Instance = null;
    private readonly Dictionary<Prefab, Queue<GameObject>> pool = new Dictionary<Prefab, Queue<GameObject>>();
    private readonly List<GameObject> prefabList = new List<GameObject>();

    void Awake() {
        if (Instance != null)
            Destroy(this);
        else {
            Instance = this;
            foreach (string name in Enum.GetNames(typeof(Prefab)))
                prefabList.Add(Resources.Load<GameObject>($"{Path.prefab}{name}"));
            foreach (Prefab value in Enum.GetValues(typeof(Prefab)))
                pool.Add(value, new Queue<GameObject>());
        }
    }

    private GameObject Get(Prefab type) => pool[type].Count.Equals(0)
        ? Instantiate(prefabList[(int)type], Vector3.zero, Quaternion.identity)
        : pool[type].Dequeue();

    public void Release(Prefab type, GameObject obj) {
        pool[type].Enqueue(obj);
        obj.transform.SetParent(transform);
        obj.SetActive(false);
    }
}



/* get variation */
public partial class ObjectPool
{
    public GameObject Get(Prefab type, Transform parent) {
        GameObject obj = Get(type);
        obj.SetActive(true);
        obj.transform.SetParent(parent);
        obj.transform.localScale = Vector3.one;
        obj.transform.localPosition = Vector3.zero;
        return obj;
    }

    public GameObject Get(Prefab type, Transform parent, Vector3 localPosition) {
        GameObject obj = Get(type, parent);
        obj.transform.localPosition = localPosition;
        return obj;
    }

    public GameObject Get(Prefab type, Transform parent, Vector3 localPosition, Vector2 sizeDelta) {
        GameObject obj = Get(type, parent, localPosition);
        obj.GetComponent<RectTransform>().sizeDelta = sizeDelta;
        return obj;
    }
}