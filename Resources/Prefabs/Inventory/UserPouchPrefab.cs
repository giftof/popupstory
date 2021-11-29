using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Popup.Items;
using Popup.Inventory;
using Popup.Configs;
using Popup.Defines;



public class UserPouchPrefab : InventoryBase
{
    void Start()
    {
        Initialize(Config.pouchSize);
        MakeSlot();
        ButtonAction();
    }
}