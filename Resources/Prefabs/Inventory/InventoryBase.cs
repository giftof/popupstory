using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Popup.Items;
using Popup.Inventory;
using Popup.Configs;
using Popup.Defines;



public abstract partial class InventoryBase : MonoBehaviour {

    protected Inventory inventory = null;
    [SerializeField]
    protected GameObject frame;
    [SerializeField]
    protected uint size;
    public PCustomButton takeAll;
    public PCustomButton close;

    public void Insert(params Item[] array) {
        AddNew(array);

        if (isActiveAndEnabled) {
            CreateUndefinedSlot();
        }
    }

    private void OnEnable() {
        CreateUndefinedSlot();
    }

    protected void MakeInventory() => inventory = inventory ?? new WareHouse(size);

    protected void ButtonAction() {
        close.AddClickAction(() => {
            gameObject.SetActive(false);
            //DEBUG_TEST_SHOW_CONTENTS();
        });
    }

    protected void MakeSlot() {
        for (int i = 0; i < size; i++) {
            GameObject slot = ObjectPool.Instance.Get(Prefab.ItemSlot, frame.transform);
            PItemSlot prefab = slot.GetComponent<PItemSlot>();
            prefab.slotId = i;
            prefab.AddInsertAction(item => inventory.Insert(item));
            prefab.AddRemoveAction(item => inventory.Remove(item));
        }
    }
}


public abstract partial class InventoryBase {

    private void AddNew(params Item[] array) {
        foreach (Item item in array) {
            item.SetSlotId = Config.unSlot;
            if (!inventory.Add(item))
                return;
        }
    }

    private int EmptySlotIndex(int startIndex) {
        while (0 < frame.transform.GetChild(startIndex).childCount)
            ++startIndex;
        return startIndex;
    }

    private void CreateUndefinedSlot() {
        int beginIndex = 0;

        foreach (Item item in inventory.UnslotedList()) {
            beginIndex = EmptySlotIndex(beginIndex);

            Transform parent = frame.transform.GetChild(beginIndex);
            item.SetSlotId = beginIndex;

            GameObject obj = ObjectPool.Instance.Get(Type(item), parent);
            SetPrefabData(obj.GetComponent<PItemBase>(), item, parent);
        }
    }

    private Prefab Type(Item item) => item.HaveAttribute(ItemCat.stackable) ? Prefab.StackableItem : Prefab.SolidItem;

    private void SetPrefabData(PItemBase itemBase, Item item, Transform parent) {
        itemBase.Item = item;
        itemBase.lastParent = parent;
        //itemBase.SetAmount(item.UseableCount);
        item.updateUseableConut = itemBase.SetAmount;
        item.updateIcon = itemBase.SetIconImage;
        item.reload();
    }
}



public abstract partial class InventoryBase {

    public void DEBUG_TEST_SHOW_CONTENTS() => inventory.DEBUG_ShowAllItems();
}
