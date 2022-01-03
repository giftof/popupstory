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
            Initialize(Config.pouchSize);
        }
    }

    void Start() {
        MakeSlot();
        // ButtonAction();
        gameObject.SetActive(false);
    }
}
