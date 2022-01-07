using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Popup.Items;
using Popup.Inventory;
using Popup.Configs;
using Popup.Defines;
using Popup.Delegate;



public abstract class PInventoryBase : MonoBehaviour {

    protected m_inventory inventory = null;
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

    //private Prefab Type(m_item item) => item.HaveAttribute(ItemCat.stackable) ? Prefab.StackableItem : Prefab.SolidItem;



    /********************************/
    /* Initialize funcs             */
    /********************************/

    // protected void ButtonAction() => close.AddClickAction(() => gameObject.SetActive(false));

    protected void MakeSlot() {
        for (int i = 0; i < inventorySize; i++) {
            PItemSlot prefab = ObjectPool.Instance.Get<PItemSlot>(Prefab.ItemSlot, frame.transform);
            //prefab.slotId = i;
            //prefab.SetInsertData(item => inventory.Insert(item));
            //prefab.SetRemoveData(item => inventory.Remove(item));
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

    //private PItemSlot FindSlot(PItemBase itemBase) {
    //    foreach (PItemSlot slot in slotArray) {
    //        if (itemBase == slot.CurrentItem)
    //            return slot;
    //    }
    //    return null;
    //}

    //public void Use(Item item) => inventory.Use(item.Uid);

    //private int nextIndex = -1;
    //public PItemBase Next() {
    //    while (true) {
    //        if (++nextIndex == inventorySize)
    //            break;
    //        if (slotArray[nextIndex].CurrentItem != null)
    //            return slotArray[nextIndex].CurrentItem;
    //    }
    //    nextIndex = -1;
    //    return null;
    //}

    //public void Remove(PItemBase[] itemArray) {
    //    foreach(PItemBase item in itemArray)
    //        Remove(item);
    //}

    //public void Remove(PItemBase item) {
    //    if (inventory.HaveItem(item.Item.Uid)) {
    //        //inventory.Remove(item.Item);
    //        FindSlot(item)?.RemoveItem();
    //    }
    //}

    //public void Insert(Item item) {
    //    if (item.HaveAttribute(ItemCat.stackable)) {
    //        inventory.InsertItem(item);
    //        //inventory.AddStackable(item as StackableItem);
    //        if (!item.IsExist)
    //            return;
    //    }

    //    while (inventory.HaveSpace()) {
    //        Item newItem = inventory.InsertItem(item);
    //        //(Item added, Item remain) = inventory.AddNew(item);

    //        if (newItem != null) {
    //            PItemSlot itemSlot = slotArray[EmptySlotIndex()];
    //            PItemBase itemBase = ObjectPool.Instance.Get(Type(newItem), itemSlot.transform).GetComponent<PItemBase>();
    //            itemBase.Item = newItem;
    //            itemBase.Item.SetSlotId = itemSlot.slotId;
    //            itemBase.LastParentSlot = itemSlot;
    //        }
    //        else
    //            return;
    //        if (!item.IsExist)
    //            return;
    //    }
    //    return;
    //}

    //public void Insert(params Item[] array) {
    //    foreach (Item item in array)
    //        Insert(item);
    //}

    //private int EmptySlotIndex(int startIndex = 0) {
    //    while (slotArray[startIndex].CurrentItem != null)
    //        ++startIndex;
    //    return startIndex;
    //}



	/********************************/
	/* TEST funcs              	    */
	/********************************/

    //public void DEBUG_TEST_SHOW_CONTENTS() => inventory.DEBUG_ShowAllItems();
}
