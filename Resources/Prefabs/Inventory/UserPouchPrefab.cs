using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Popup.Items;
using Popup.Inventory;
using Popup.Configs;
using Popup.Defines;



public class UserPouchPrefab : PInventoryBase {

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
}
