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
    //private string[] prefabPath = {
    //    "Prefabs/Common/CustomButtonPrefab",
    //    "Prefabs/Common/TextMeshPrefab",
    //    "Prefabs/Inventory/ItemSlotPrefab",
    //    "Prefabs/Item/ItemToolPrefab",
    //    "Prefabs/Item/ItemEquipPrefab"
    //};

    //  CustomButton,
    //  TextMesh,
    //  ItemSlot,
    //  ItemTool,
    //  ItemEquip,

    //  //Spell,
    //  //Buff,
    //  //Charactor,
    //  //Inventory,
    //  //Squad,


    void Awake() {
        if (Instance != null)
            Destroy(this);
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
            Debug.Log($"{Path.prefab}{name}");
            prefabList.Add(Resources.Load<GameObject>($"{Path.prefab}{name}"));
        }

        //foreach (string path in prefabPath) {
        //    prefabList.Add(Resources.Load<GameObject>(path));
        //}
    }

    private void BuildContainer() {
        pool = new Dictionary<Prefab, Queue<GameObject>>();

        foreach (Prefab element in Enum.GetValues(typeof(Prefab))) {
            pool.Add(element, new Queue<GameObject>());
        }
    }
}
