using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherSquadPrefab : SquadBase
{
    //public override void LinkInventory(InventoryBase inventory) => Inventory = (OtherPouchPrefab)inventory;
    public InventoryBase TakeAllTarget { get; set; } = null;

    protected override void SetButtonAction()
    {
        inventoryBtn.AddClickAction(() => {
            ToggleInventory();
        });

        inventoryBase.takeAll.AddClickAction(() => {
            TakeAllTarget?.Insert(((OtherPouchPrefab)inventoryBase).PopAll());
        });
    }
}
