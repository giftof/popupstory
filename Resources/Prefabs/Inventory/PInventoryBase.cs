using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Popup.Items;
using Popup.Inventory;
using Popup.Configs;
using Popup.Defines;



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

    protected void ButtonAction() => close.AddClickAction(() => gameObject.SetActive(false));

    protected void MakeSlot() {
        for (int i = 0; i < inventorySize; i++) {
            PItemSlot prefab = ObjectPool.Instance.Get(Prefab.ItemSlot, frame.transform).GetComponent<PItemSlot>();
            prefab.slotId = i;
            prefab.SetInsertData(item => inventory.Insert(item));
            prefab.SetRemoveData(item => inventory.Remove(item));
            slotArray[i] = prefab;
        }
    }



    /********************************/
    /* Behaviours funcs             */
    /********************************/

    private void OnEnable() => CreateUndefinedSlot();

    public int nextIndex = 0;
    public PItemBase Next() {
        
        while (true) {
            if (0 < frame.transform.GetChild(nextIndex++).childCount) {
                return frame.transform.GetChild(nextIndex - 1).GetChild(0).GetComponent<PItemBase>();
            }
            if (nextIndex == inventorySize)
                break;
        }
        nextIndex = 0;
        return null;
    }

    public void Remove(PItemBase item) {
        inventory.Remove(item.Item);
        ObjectPool.Instance.Release(item.Type, item.gameObject);
    }

    public bool Insert(params Item[] array) {
        bool success = AddNew(array);

        if (isActiveAndEnabled)
            CreateUndefinedSlot();

        return success;
    }

    private bool AddNew(params Item[] array) {
        foreach (Item item in array) {
            item.SetSlotId = Config.unSlot;
            if (!inventory.Add(item))
                return false;
        }
        return true;
    }

    private int EmptySlotIndex(int startIndex) {

        while (slotArray[startIndex].CurrentItem != null)
            ++startIndex;
        return startIndex;
    }

    private void CreateUndefinedSlot() {
        int emptyIndex = 0;

        foreach (Item item in inventory.UnslotedList()) {
            emptyIndex = EmptySlotIndex(emptyIndex);

            PItemSlot parent = slotArray[emptyIndex];
            PItemBase itemBase = ObjectPool.Instance.Get(Type(item), parent.transform).GetComponent<PItemBase>();

            itemBase.Item = item;
            itemBase.lastParentSlot = parent;

            item.SetSlotId = emptyIndex;
            item.updateUseableConut = itemBase.SetAmount;
            item.updateIcon = itemBase.SetIconImage;
            item.removeEmptySlot = itemBase.ReleaseObject;
            item.Reload();

        }
    }

    //private void SetPrefabData(PItemBase itemBase, Item item, PItemSlot parent) {
    //    itemBase.Item = item;
    //    itemBase.lastParentSlot = parent;
    //    item.updateUseableConut = itemBase.SetAmount;
    //    item.updateIcon = itemBase.SetIconImage;
    //    item.removeEmptySlot = itemBase.ReleaseObject;
    //    item.Reload();
    //}



	/********************************/
	/* TEST funcs              	    */
	/********************************/

    public void DEBUG_TEST_SHOW_CONTENTS() => inventory.DEBUG_ShowAllItems();
}
