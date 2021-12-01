using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Popup.Items;
using Popup.Inventory;
using Popup.Configs;
using Popup.Defines;



public class OtherPouchPrefab : InventoryBase
{
    void Start()
    {
        MakeInventory();
        MakeSlot();
        ButtonAction();
        gameObject.SetActive(false);
    }

    public Item[] PopAll()
    {
        foreach (Transform child in frame.transform)
        {
            if (0 < child.childCount)
            {
                Transform retrieveObject = child.GetChild(0);
                Prefab type = retrieveObject.GetComponent<ItemBase>().Type;
                ObjectPool.Instance.Return(type, retrieveObject.gameObject);
            }
        }

        return inventory.PopAll();
    }

}
