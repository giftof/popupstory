using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Popup.Inventory;
using Popup.Items;
using Popup.Configs;



public class InventoryPrefab : MonoBehaviour
{
    Inventory inventory;

    void Start()
    {

    }

    public void Initialize(int size) => inventory = new WareHouse(size);
    public void Initialize(Item[] array)
    {
        inventory = new WareHouse(array.Length);
        Add(array);
    }

    private void Add(params Item[] array)
    {
        foreach(Item item in array)
            inventory.Add(item);
    }
}
