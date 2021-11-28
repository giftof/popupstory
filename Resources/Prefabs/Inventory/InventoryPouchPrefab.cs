using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Popup.Items;
using Popup.Inventory;
using Popup.Configs;
using Popup.Defines;



public class InventoryPouchPrefab : MonoBehaviour
{
    Inventory inventory = null;
    [SerializeField] GameObject frame;
    [SerializeField] CustomButtonPrefab takeAll;
    [SerializeField] CustomButtonPrefab close;

    void Start()
    {
        Initialize(Config.pouchSize);
        close.AddClickAction(() => gameObject.SetActive(false));
        takeAll.AddClickAction(() => Debug.Log($"Click Take All!"));
        MakeSlot();
    }

    public void Initialize(int size) => inventory = inventory ?? new WareHouse(size);
    public void Initialize(params Item[] array)
    {
        inventory = inventory ?? new WareHouse(array.Length);
        AddNew(array);
        CreateIcon();
    }

    private void MakeSlot()
    {
        foreach (GameObject slot in ObjectPool.Instance.Request(Popup.Defines.Prefab.ItemSlot, Config.pouchSize))
        {
            slot.transform.SetParent(frame.transform);
            slot.transform.localScale = Vector3.one;
            slot.gameObject.SetActive(true);
        }
    }

    private void AddNew(params Item[] array)
    {
        foreach (Item item in array)
        {
            Debug.Log($"add element = {item.Name}");
            item.SlotId = Config.unSlot;
            inventory.Add(item);
        }
    }

    private int EmptySlotIndex(int startIndex)
    {
        while (0 < frame.transform.GetChild(startIndex).childCount)
        {
            ++startIndex;
        }
        return startIndex;
    }

    private void CreateIcon()
    {
        int startIndex = 0;
        Transform parent;
        GameObject obj;
        Prefab type;

        foreach (Item item in inventory.UnSlotedList())
        {
            parent = frame.transform.GetChild(EmptySlotIndex(startIndex));
            //startIndex = EmptySlotIndex(startIndex);

            type = item.HaveAttribute(ItemCat.tool) ? Prefab.ItemTool : Prefab.ItemEquip;
            obj = ObjectPool.Instance.Request(type);

            obj.SetActive(true);
            obj.GetComponent<ItemBase>().Item = item;
            //obj.transform.SetParent(frame.transform.GetChild(startIndex));
            obj.transform.SetParent(parent);
            obj.transform.localScale = Vector3.one;
            obj.transform.localPosition = Vector3.zero;

            ItemBase itemBase = obj.GetComponent<ItemBase>();
            itemBase.Item = item;
            //itemBase.lastParent = frame.transform.GetChild(startIndex);
            itemBase.lastParent = parent;

            if (type is Prefab.ItemTool)
            {
                obj.GetComponent<ItemToolPrefab>().SetAmount(item.UseableCount);
            }
        }
    }


    /*
        [SerializeField]
        private CustomButtonPrefab[] items;
    */
}
