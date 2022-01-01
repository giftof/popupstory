using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Popup.Items;
using Popup.Framework;
using Popup.Defines;


public class PSolidItem : PItemBase
{
    public override void SetAmount() { }
    public override Prefab Type => Prefab.SolidItem;
}
