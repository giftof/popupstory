using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Popup.Items;
using Popup.Inventory;
using Popup.Configs;
using Popup.Defines;
using Popup.Delegate;



public abstract class PInventoryBase : MonoBehaviour {

    protected Inventory inventory = null;
    protected PItemSlot[] slotArray = null;
    protected uint inventorySize;
    [SerializeField]
    protected GameObject frame;
    public PCustomButton takeAll;
    public PCustomButton close;



    /********************************/
    /* Abstract funcs              	*/
    /********************************/



    /********************************/
    /* Property funcs              	*/
    /********************************/

    private Prefab Type(Item item) => item.HaveAttribute(ItemCat.stackable) ? Prefab.StackableItem : Prefab.SolidItem;



    /********************************/
    /* Initialize funcs             */
    /********************************/

    // protected void ButtonAction() => close.AddClickAction(() => gameObject.SetActive(false));

    protected void MakeSlot() {
        for (int i = 0; i < inventorySize; i++) {
            PItemSlot prefab = ObjectPool.Instance.Get(Prefab.ItemSlot, frame.transform).GetComponent<PItemSlot>();
            prefab.slotId = i;
            prefab.SetInsertData(item => inventory.Insert(item));
            prefab.SetRemoveData(item => inventory.Remove(item));
            slotArray[i] = prefab;
        }
    }

    protected void Initialize(uint size) {
        inventorySize = size;
        inventory = new WareHouse(size);
        slotArray = new PItemSlot[size];
    }



    /********************************/
    /* Behaviours funcs             */
    /********************************/

    private PItemSlot FindSlot(PItemBase itemBase) {
        foreach (PItemSlot slot in slotArray) {
            if (itemBase == slot.CurrentItem)
                return slot;
        }
        return null;
    }

    public bool Use(Item item) => inventory.Use(item);

    private int nextIndex = -1;
    public PItemBase Next() {
        while (true) {
            if (++nextIndex == inventorySize)
                break;
            if (slotArray[nextIndex].CurrentItem != null)
                return slotArray[nextIndex].CurrentItem;
        }
        nextIndex = -1;
        return null;
    }

    public void Remove(PItemBase[] itemArray) {
        foreach(PItemBase item in itemArray)
            Remove(item);
    }

    public void Remove(PItemBase item) {
Debug.Log($"Requested remove item = {item.Item?.Uid}, slot = {item.Item.SlotId}");
// Debug.Log($"is contain?: {inventory.HaveItem(item.Item)}");
        if (inventory.HaveItem(item.Item.Uid)) {
            inventory.Remove(item.Item);
            FindSlot(item)?.RemoveItem();
        }
    }

    public bool Insert(Item item) {
        if (item.HaveAttribute(ItemCat.stackable)) {
            inventory.AddStackable(item as StackableItem);
            if (!item.IsExist)
                return true;
        }

        while (inventory.HaveSpace()) {
            (Item added, Item remain) = inventory.AddNew(item);

            if (added != null) {
                PItemSlot itemSlot = slotArray[EmptySlotIndex()];
                PItemBase itemBase = ObjectPool.Instance.Get(Type(added), itemSlot.transform).GetComponent<PItemBase>();
                itemBase.Item = added;
                itemBase.Item.SetSlotId = itemSlot.slotId;
                itemBase.lastParentSlot = itemSlot;
            }
            else
                return false;
            if (remain == null)
                return true;
        }
        return false;
    }

    public void Insert(params Item[] array) {
        foreach (Item item in array)
            Insert(item);
    }

    private int EmptySlotIndex(int startIndex = 0) {
        while (slotArray[startIndex].CurrentItem != null)
            ++startIndex;
        return startIndex;
    }



	/********************************/
	/* TEST funcs              	    */
	/********************************/

    public void DEBUG_TEST_SHOW_CONTENTS() => inventory.DEBUG_ShowAllItems();
}
