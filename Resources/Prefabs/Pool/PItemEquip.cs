using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Popup.Items;
using Popup.Framework;
using Popup.Defines;


public class PItemEquip : PItemBase
{
    public override void Use() // impl.
    {
        EquipItem item = Item as EquipItem;
    }

    public override void SetAmount(int _) { }

    public override Prefab Type => Prefab.ItemEquip;
}
