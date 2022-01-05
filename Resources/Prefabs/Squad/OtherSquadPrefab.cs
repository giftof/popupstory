using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Popup.Items;

public class OtherSquadPrefab : SquadBase {
    public PInventoryBase TakeAllTarget { get; set; } = null;

    protected override void SetButtonAction() {

        inventoryBtn.AddClickAction( () => ToggleInventory() );
        //inventoryBase.close.AddClickAction( () => inventoryBase.DEBUG_TEST_SHOW_CONTENTS() );
        //    inventoryBase.takeAll.AddClickAction(() => {
        //        while (true) {
        //            PItemBase next = inventoryBase.Next();
        //            if (next != null) {
        //                TakeAllTarget?.Insert(next.Item);
        //                //if (TakeAllTarget?.Insert(next.Item) ?? false) {
        //                //    Debug.Log($"I'll remove {next.Item.Name}");
        //                //    Remove(next);
        //                //}
        //            }
        //            else
        //                return;
        //        }
        //    });
    }
}
