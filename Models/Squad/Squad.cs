using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Popup.Framework;



namespace Popup.Squad
{
    using Charactor = Charactors.Charactor;
    using Inventory = Inventory.Inventory;
    using Item = Items.Item;

    public class Squad: IInventory
    {
        string      name;
        int         uid;
        Charactor[] charactors;
        Inventory   inventory;



        void        InventoryVerify ()              => inventory.EraseDummySlot();
        public bool AddItem         (ref Item item) => inventory.AddItem(ref item);
        public bool UseItem         (Item item)     => inventory.UseItem(item);
        public bool UseItem         (int uid)       => inventory.UseItem(uid);
        public Item PickItem        (int uid)       => inventory.PickItem(uid);
        public bool PopItem         (int uid)       => inventory.PopItem(uid);
        public void SetMaxSize      (int size)      => inventory.SetMaxSize(size);
    }
}
