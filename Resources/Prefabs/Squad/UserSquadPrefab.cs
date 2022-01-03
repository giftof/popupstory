using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserSquadPrefab : SquadBase
{
    //public override void LinkInventory(InventoryBase inventory) => Inventory = (UserPouchPrefab)inventory;

    protected override void SetButtonAction()
    {
        inventoryBtn.AddClickAction( () => ToggleInventory() );
        inventoryBase.close.AddClickAction( () => inventoryBase.DEBUG_TEST_SHOW_CONTENTS() );

    }

}
