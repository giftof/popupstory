using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Popup.Items;
using Popup.Inventory;
using Popup.Configs;
using Popup.Defines;



public class UserPouchPrefab : InventoryBase
{
    void Awake()
    {
        size = Config.pouchSize;
        inventory = inventory ?? new WareHouse(size);
    }

    void Start()
    {
        MakeInventory();
        MakeSlot();
        ButtonAction();
        gameObject.SetActive(false);
    }
}
