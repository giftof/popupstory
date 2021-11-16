using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Popup.Framework;
using Popup.Library;
using Popup.Configs;



namespace Popup.Squad
{
    using Charactor = Charactors.Charactor;
    using Inventory = Inventory.Inventory;
    using Configs   = Configs.Configs;
    using Item      = Items.Item;

    public class Squad: IInventory, ICharactor, IPopupObject
    {
        public string      name { get; protected set; }
        public int         uid { get; }
        public Charactor[] charactors { get; protected set; }
        public Inventory   inventory { get; protected set; }
        public int         activateCharactorIndex { get; protected set; }
        public bool        activateTurn { get; protected set; }



        public Squad(int uid, int inventorySize = Configs.squadInventorySize)
        {
            this.uid = uid;
            SetMaxSize(inventorySize);
        }


        public int GetUID() => uid;

        public object Duplicate() => null;      // impl.
        public object DuplicateNew() => null;   // impl.


        private void InventoryVerify ()              => inventory.EraseDummySlot();
        public  bool AddItem         (Item item) => inventory.AddItem(item);
        public  bool UseItem         (Item item) => inventory.UseItem(item);
        public  bool UseItem         (int uid)       => inventory.UseItem(uid);
        public  Item PickItem        (int uid)       => inventory.PickItem(uid);
        public  bool PopItem         (int uid)       => inventory.PopItem(uid);
        public  void SetMaxSize      (int size)      => inventory.SetMaxSize(size);
        public  void SetName         (string name)   => this.name = name;




        public Charactor PickCharactor(int uid) => Guard.MustInclude(uid, charactors, "[PickCharactor in squad]");

        public bool PopCharactor(int uid)
        {
            // Guard.MustInclude(uid, charactors, "[PopCharactor in squad]") = null;
            return true;
        }

        public bool PopCharactor(Charactor charactor) => PopCharactor(charactor.GetUID());

        public bool AddCharactor(int uid) => false;

        public bool AddCharactor(Charactor charactor)
        {
            int index = Libs.FindEmptyIndex(charactors);

            if (Libs.IsInclude(index, charactors.Length))
            {
                charactors[index] = charactor;
                return true;
            }
            return false;
        }

    }
}
