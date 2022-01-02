using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Popup.Items;

public class OtherSquadPrefab : SquadBase {
    public PInventoryBase TakeAllTarget { get; set; } = null;

    protected override void SetButtonAction() {
        inventoryBtn.AddClickAction( () => ToggleInventory() );
        
        inventoryBase.takeAll.AddClickAction(() => {
            
            while (true) {
                PItemBase next = inventoryBase.Next();

                Debug.LogError($"next = {next?.Item.Name}");

                if (next != null && TakeAllTarget != null) {
                    if (TakeAllTarget.Insert(next.Item))
                        Remove(next);
                }
                else
                    return;
            }
        });
    }
}
