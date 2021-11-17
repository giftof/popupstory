using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Popup.Framework;
using Popup.Library;
using Popup.Configs;
using Newtonsoft.Json;



namespace Popup.Squad
{
    using Charactor = Charactors.Charactor;
    using Inventory = Inventory.Inventory;
    using Configs   = Configs.Configs;
    using Item      = Items.Item;
    using ServerJob = ServerJob.ServerJob;

    public class Squad: IInventory, ICharactor, IPopupObject
    {
		[JsonProperty]
        public string      name { get; protected set; }
		[JsonProperty]
        public int         uid { get; protected set; }
		[JsonProperty]
		public int slotId { get; protected set; }
		[JsonProperty]
        public Charactor[] charactors { get; protected set; }
		[JsonProperty]
        public Inventory   inventory { get; protected set; }
		[JsonProperty]
        public int         activateCharactorIndex { get; protected set; }
		[JsonProperty]
        public bool        activateTurn { get; protected set; }



        public Squad(int uid, int inventorySize = Configs.squadInventorySize)
        {
            this.uid = uid;
            SetMaxSize(inventorySize);
        }


        public int GetUID() => uid;

        public bool IsExist => 0 < charactors.Length;
        public object Duplicate() => MemberwiseClone();      // impl.
        public object DuplicateNew() => (((Squad)Duplicate()).uid = ServerJob.RequestNewUID);   // impl.


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

        public bool PopCharactor(Charactor charactor) => PopCharactor(charactor.uid);

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
