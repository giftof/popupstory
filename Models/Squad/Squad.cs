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
        string      name;
        int         uid;
        Charactor[] charactors;
        Inventory   inventory;



        public Squad(int uid, int inventorySize = Configs.squadInventorySize)
        {
            this.uid = uid;
            SetMaxSize(inventorySize);
        }

        public int GetUID() => uid;

        private void InventoryVerify ()              => inventory.EraseDummySlot();
        public  bool AddItem         (ref Item item) => inventory.AddItem(ref item);
        public  bool ExhaustItem     (ref Item item) => inventory.ExhaustItem(ref item);
        public  bool ExhaustItem     (int uid)       => inventory.ExhaustItem(uid);
        public  Item PickItem        (int uid)       => inventory.PickItem(uid);
        public  bool PopItem         (int uid)       => inventory.PopItem(uid);
        public  void SetMaxSize      (int size)      => inventory.SetMaxSize(size);
        public  void SetName         (string name)   => this.name = name;




        public ref Charactor PickCharactor(int uid) => ref Guard.MustInclude(uid, ref charactors, "[PickCharactor in squad]");

        public bool PopCharactor(int uid)
        {
            Guard.MustInclude(uid, ref charactors, "[PopCharactor in squad]") = null;
            return true;
        }

        public bool PopCharactor(ref Charactor charactor) => PopCharactor(charactor.GetUID());

        public bool AddCharactor(int uid) => false;

        public bool AddCharactor(ref Charactor charactor)
        {
            int index = Libs.FindEmptyIndex(ref charactors);

            if (Libs.IsInclude(index, charactors.Length))
            {
                charactors[index] = charactor;
                return true;
            }
            return false;
        }
    }
}
