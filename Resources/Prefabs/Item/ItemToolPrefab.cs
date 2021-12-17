using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Popup.Items;
using Popup.Framework;
using Popup.Defines;
using TMPro;



public class ItemToolPrefab : ItemBase
{
    [SerializeField] TextMeshProUGUI amount;

    public override void Use() // impl.
    {
        ToolItem item = Item as ToolItem;
    }

    public override void SetAmount(int amount) => this.amount.text = amount.ToString();

    public override Prefab Type => Prefab.ItemTool;
}
