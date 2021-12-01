using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Popup.Items;
using Popup.Inventory;
using Popup.Configs;
using Popup.Defines;



public abstract partial class InventoryBase : MonoBehaviour
{
    protected Inventory inventory = null;
    [SerializeField]
    protected GameObject frame;
    [SerializeField]
    protected int size;
    public CustomButtonPrefab takeAll;
    public CustomButtonPrefab close;



    void Awake() => inventory = inventory ?? new WareHouse(size);


    public void Insert(params Item[] array)
    {
        AddNew(array);

        if (isActiveAndEnabled)
        {
            CreateNegativeSlotItemIcon();
        }
    }

    private void OnEnable()
    {
        Debug.Log("On ENABLE");
        CreateNegativeSlotItemIcon();
    }

    protected void MakeInventory() => inventory = inventory ?? new WareHouse(size);

    protected void ButtonAction()
    {
        close.AddClickAction(() => {
            gameObject.SetActive(false);
            DEBUG_TEST_SHOW_CONTENTS();
        });
    }

    protected void MakeSlot()
    {
        int slotId = 0;

        foreach (GameObject slot in ObjectPool.Instance.Request(Popup.Defines.Prefab.ItemSlot, Config.pouchSize))
        {
            slot.SetActive(true);
            slot.transform.SetParent(frame.transform);
            slot.transform.localScale = Vector3.one;

            ItemSlotPrefab prefab = slot.GetComponent<ItemSlotPrefab>();
            prefab.slotId = slotId++;
            prefab.AddInsertAction((Item item) => {
                inventory.Insert(item);
            });
            prefab.AddRemoveAction((Item item) => {
                inventory.Remove(item);
            });
        }
    }
}



public abstract partial class InventoryBase
{
    private void AddNew(params Item[] array)
    {
        foreach (Item item in array)
        {
            Debug.Log($"add element = {item.Name}");
            item.SlotId = Config.unSlot;
            if (!inventory.Add(item))
                return;
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

    private void CreateNegativeSlotItemIcon()
    {
        int beginIndex = 0;

        foreach (Item item in inventory.UnslotedList())
        {
            beginIndex = EmptySlotIndex(beginIndex);

            Transform parent = frame.transform.GetChild(beginIndex);
            item.SlotId = beginIndex;

            GameObject obj = ObjectPool.Instance.Request(Type(item));
            obj.SetActive(true);

            SetParent(obj.transform, parent);
            SetPrefabData(obj.GetComponent<ItemBase>(), item, parent);
        }
    }

    private Prefab Type(Item item) => item.HaveAttribute(ItemCat.tool) ? Prefab.ItemTool : Prefab.ItemEquip;

    private void SetPrefabData(ItemBase itemBase, Item item, Transform parent)
    {
        itemBase.Item = item;
        itemBase.lastParent = parent;
        itemBase.SetAmount(item.UseableCount);
    }

    private void SetParent(Transform child, Transform parent)
    {
        child.SetParent(parent);
        child.localScale = Vector3.one;
        child.localPosition = Vector3.zero;
    }
}



public abstract partial class InventoryBase
{
    public void DEBUG_TEST_SHOW_CONTENTS() => inventory.DEBUG_ShowAllItems();
}
