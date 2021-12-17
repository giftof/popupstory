using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Popup.Framework;
using Popup.Library;
using Popup.Configs;
using Popup.Inventory;
using Newtonsoft.Json;



namespace Popup.Squad
{
    using Charactor = Charactors.Charactor;
    using Inventory = Inventory.Inventory;
    using Config    = Configs.Config;
    using Item      = Items.Item;
    // using ServerJob = ServerJob.ServerJob;

    public partial class Squad : PopupObject /*, IInventory*/
    {
        [JsonProperty]
        public Dictionary<int, Charactor> Charactors { get; protected set; }
        [JsonIgnore]
        private int OccupiedSize { get; set; }
        [JsonProperty]
        public Inventory Inventory { get; protected set; }
		[JsonIgnore]
        public int? ActivateCharactor { get; protected set; }

        public Squad(int uid, uint inventorySize = Config.squadInventorySize)
        {
            this.Uid = uid;
            OccupiedSize = 0;
            Charactors = new Dictionary<int, Charactor>();
            Inventory = new WareHouse(inventorySize);
        }


        public int GetUID() => Uid;

        public override bool IsExist => 0 < Charactors.Count;
        public bool IsAlive => !Charactors.FirstOrDefault(p => p.Value.IsAlive).Equals(null);
        public override object DeepCopy(int? uid = null, int? _ = null)
        {
            Inventory.EraseExhaustedSlot();

            Squad squad = (Squad)MemberwiseClone();
            squad.Uid = uid ?? squad.Uid;

            return squad;
        }

        public bool Use(Item item) => Inventory.Use(item);
        public bool Add(Item item) => Inventory.Add(item);

        public bool AddLast(Charactor charactor)
        {
            if (charactor == null || Config.squadSize < OccupiedSize + charactor.Size) return false;
            int lastSlotId = IsExist ? Charactors.Max(p => p.Value.SlotId) + 1 : 0;

            charactor.SetSlotId = lastSlotId;
            Charactors.Add(charactor.Uid, charactor);
            OccupiedSize += charactor.Size;

            return true;
        }
        
        public bool AddFirst(Charactor charactor)
        {
            if (charactor == null || Config.squadSize < OccupiedSize + charactor.Size) return false;

            foreach (KeyValuePair<int, Charactor> pair in Charactors)
                pair.Value.ShiftPosition(1);

            charactor.SetSlotId = 0;
            Charactors.Add(charactor.Uid, charactor);
            OccupiedSize += charactor.Size;

            return true;
        }

        private void CollisionAffect(Charactor target, Spell spell) => target?.TakeAffect(spell);

        public int ShiftForward(Charactor target, int step)
        {
            Guard.MustInclude(target.Uid, Charactors, "[ShiftForward in Squad]");

            var list = from pair in Charactors
                       where pair.Value.SlotId < target.SlotId
                       orderby pair.Value.SlotId descending
                       select pair.Value;

            foreach (Charactor c in list)
            {
                if (c.Size < step)
                {
                    step -= c.Size;
                    c.ShiftPosition(1);
                    target.ShiftPosition(-1);
                    CollisionAffect(c, null);
                    CollisionAffect(target, null);
                }
                else break;
            }
            
            return step;
        }

        public int ShiftBackward(Charactor target, int step)
        {
            Debug.Log(target.Uid);
            Guard.MustInclude(target.Uid, Charactors, "[ShiftBackward in Squad]");

            var list = from pair in Charactors
                       where target.SlotId < pair.Value.SlotId
                       orderby pair.Value.SlotId ascending
                       select pair.Value;

            foreach (Charactor c in list)
            {
                if (c.Size < step)
                {
                    step -= c.Size;
                    c.ShiftPosition(-1);
                    target.ShiftPosition(1);
                    CollisionAffect(c, null);
                    CollisionAffect(target, null);
                }
                else break;
            }

            return step;
        }
    }



    public partial class Squad
    {
        public void DEBUG_TEST()
        {
            var list = from pair in Charactors
                       where pair.Value != null
                       orderby pair.Value.SlotId ascending
                       select pair.Value;

            foreach (Charactor c in list)
            {
                Debug.Log($"name = {c.Name}, uid = {c.Uid}, slotId = {c.SlotId}");
            }
        }
    }
}
