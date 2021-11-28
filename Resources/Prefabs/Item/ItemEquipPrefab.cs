using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Popup.Items;
using Popup.Framework;



public class ItemEquipPrefab : ItemBase
{
    public override void Use() // impl.
    {
        /*Item.Use();*/
        Debug.Log($"Double Clicked! {Item.Name}");
        EquipItem item = Item as EquipItem;
        Debug.Log($"{item.Durability}");
    }
}
