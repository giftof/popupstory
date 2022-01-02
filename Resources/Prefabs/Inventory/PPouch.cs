using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Popup.Items;
using Popup.Inventory;
using Popup.Configs;
using Popup.Defines;



public class PPouch : PInventoryBase {

    void Awake() {
        if (inventory == null || slotArray == null) {
            inventorySize = Config.pouchSize;
            inventory = new WareHouse(inventorySize);
            slotArray = new PItemSlot[inventorySize];
        }
    }

    void Start() {
        MakeSlot();
        ButtonAction();
        gameObject.SetActive(false);
    }

    public Item[] TakeAll() {
        foreach (Transform child in frame.transform) {
            if (0 < child.childCount) {
                Transform retrieveObject = child.GetChild(0);
                Prefab type = retrieveObject.GetComponent<PItemBase>().Type;
                ObjectPool.Instance.Release(type, retrieveObject.gameObject);
            }
        }

        return inventory.TakeAll();
    }
}
