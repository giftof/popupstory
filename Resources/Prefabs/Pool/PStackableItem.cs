using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Popup.Items;
using Popup.Framework;
using Popup.Defines;
using TMPro;



public class PStackableItem : PItemBase {
    [SerializeField] TextMeshProUGUI amount;

    public override void SetAmount() => amount.text = Item.UseableCount.ToString();
    public override Prefab Type => Prefab.StackableItem;
}
