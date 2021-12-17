using System;
using System.Collections.Generic;
using UnityEngine;
using Popup.Defines;
using Popup.Configs;



public partial class ObjectPool : MonoBehaviour {

    public static ObjectPool Instance = null;

    private Dictionary<Prefab, Queue<GameObject>> pool;
    [SerializeField]
    private List<GameObject> prefabList;



    void Awake() {
        if (Instance != null) {
            Destroy(this);
            return;
        }
        Instance = this;

        BuildPrefabList();
        BuildContainer();
    }

    public GameObject Get(Prefab type) {
        if (pool[type].Count.Equals(0)) {
            return Instantiate(prefabList[(int)type], Vector3.zero, Quaternion.identity);
        }
        return pool[type].Dequeue();
    }

    public GameObject Get(Prefab type, Transform parent) {
        GameObject obj = Get(type);

        obj.SetActive(true);
        obj.transform.SetParent(parent);

        return obj;
    }

    public GameObject Get(Prefab type, Transform parent, Vector3 localPosition) {
        GameObject obj = Get(type, parent);
        obj.transform.localPosition = localPosition;

        return obj;
    }

    public GameObject Get(Prefab type, Transform parent, Vector3 localPosition, Vector2 sizeDelta)
    {
        GameObject obj = Get(type, parent, localPosition);
        obj.GetComponent<RectTransform>().sizeDelta = sizeDelta;

        return obj;
    }

    public void Release(Prefab type, GameObject obj) {
        if (pool.ContainsKey(type)) {
            pool[type].Enqueue(obj);
            obj.transform.SetParent(transform);
            obj.SetActive(false);
        }
    }
}


public partial class ObjectPool {

    private void BuildPrefabList() {
        prefabList = new List<GameObject>();

        foreach (string name in Enum.GetNames(typeof(Prefab))) {
            prefabList.Add(Resources.Load<GameObject>($"{Path.prefab}{name}"));
        }
    }

    private void BuildContainer() {
        pool = new Dictionary<Prefab, Queue<GameObject>>();

        foreach (Prefab value in Enum.GetValues(typeof(Prefab))) {
            pool.Add(value, new Queue<GameObject>());
        }
    }
}
