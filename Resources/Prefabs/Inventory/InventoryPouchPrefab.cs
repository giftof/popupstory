using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Popup.Items;
using Popup.Inventory;

public class InventoryPouchPrefab : MonoBehaviour
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
        foreach (Item item in array)
            inventory.Add(item);
    }


/*
    [SerializeField]
    private CustomButtonPrefab[] items;
*/
}
