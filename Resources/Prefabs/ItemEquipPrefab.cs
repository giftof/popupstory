using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Popup.Items;
using Popup.Framework;



public class ItemEquipPrefab : ItemBase
{
    EquipItem Item { get; set; } = null;

    public override void Use() // impl.
    {
        /*Item.Use();*/
        Debug.Log("Double Clicked!");
    }
}
