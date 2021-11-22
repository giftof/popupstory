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
    using Configs   = Configs.Configs;
    using Item      = Items.Item;
    using ServerJob = ServerJob.ServerJob;

    public class Squad: IInventory, IPopupObject
    {
		[JsonProperty]
        public string Name { get; protected set; }
		[JsonProperty]
        public int uid { get; protected set; }
		[JsonProperty]
		public int SlotId { get; protected set; }
        [JsonProperty]
        public Dictionary<int, Charactor> Charactors { get; protected set; }
        /*public LinkedList<Charactor> Charactors { get; protected set; }*/
        //public Charactor[] Charactors { get; protected set; }
        [JsonIgnore]
        private int OccupiedSize { get; set; }
        [JsonProperty]
        public Inventory Inventory { get; protected set; }
		[JsonIgnore]
        public int? ActivateCharactor { get; protected set; }
/*		[JsonProperty]
        public bool ActivateTurn { get; protected set; }*/

        public Squad(int uid, int inventorySize = Configs.squadInventorySize)
        {
            this.uid = uid;
            OccupiedSize = 0;
            Charactors = new Dictionary<int, Charactor>();
            Inventory = new WareHouse(inventorySize);
        }


        public int GetUID() => uid;

        public bool IsExist => 0 < Charactors.Count;
        public bool IsAlive => !Charactors.FirstOrDefault(p => p.Value.IsAlive).Equals(null);
/*        public bool IsAlive()
        {
            return !Charactors.FirstOrDefault(p => p.Value.IsAlive).Equals(null);
            KeyValuePair<int, Charactor> anyone = Charactors.FirstOrDefault(p => p.Value.IsAlive);
            return !anyone.Equals(null);
            if (anyone.Equals(null))
            {

            }
            Debug.Log(anyone);
            Debug.Log(anyone.Key);
            Debug.Log(anyone.Value);
            return true;

            return Charactors.FirstOrDefault(p => p.Value.IsAlive) == default;

        }
*/
        //public bool IsExist => Charactors.FirstOrDefault(c => c.IsAlive) != null;
        public object DeepCopy(int? uid = null, int? _ = null)
        {
            Inventory.EraseExhaustedSlot();

            Squad squad = (Squad)MemberwiseClone();
            squad.uid = uid ?? squad.uid;

            return squad;
        }

        public void SetName(string name) => Name = name;
        public bool Use(Item item) => Inventory.Use(item);
        public bool Add(Item item) => Inventory.Add(item);
        
        public bool AddLast(Charactor charactor)
        {
            if (charactor == null || Configs.squadSize < OccupiedSize + charactor.Size) return false;
            int lastSlotId = IsExist ? Charactors.Max(p => p.Value.SlotId) + 1 : 0;

            charactor.SetSlotId(lastSlotId);
            Charactors.Add(charactor.uid, charactor);
            OccupiedSize += charactor.Size;

            return true;
        }
        
        public bool AddFirst(Charactor charactor)
        {
            if (charactor == null || Configs.squadSize < OccupiedSize + charactor.Size) return false;

            foreach (KeyValuePair<int, Charactor> pair in Charactors)
                pair.Value.ShiftPosition(1);

            charactor.SetSlotId(0);
            Charactors.Add(charactor.uid, charactor);
            OccupiedSize += charactor.Size;

            return true;
        }

        private void CollisionAffect(Charactor target, Spell spell) => target?.TakeAffect(spell);

        public int ShiftForward(Charactor target, int step)
        {
            Guard.MustInclude(target.uid, Charactors, "[ShiftForward in Squad]");

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
            Debug.Log(target.uid);
            Guard.MustInclude(target.uid, Charactors, "[ShiftBackward in Squad]");

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


        //public Charactor PickCharactor(int uid) => Guard.MustInclude(uid, Charactors, "[PickCharactor in squad]");

        //public bool PopCharactor(int uid)
        //{
        //    // Guard.MustInclude(uid, charactors, "[PopCharactor in squad]") = null;
        //    return true;
        //}

        //public bool PopCharactor(Charactor charactor) => PopCharactor(charactor.uid);

        //public bool AddCharactor(int uid) => false;

        //public bool AddCharactor(Charactor charactor)
        //{
        //    int index = Libs.FindEmptyIndex(Charactors);

        //    if (Libs.IsInclude(index, Charactors.Length))
        //    {
        //        Charactors[index] = charactor;
        //        return true;
        //    }
        //    return false;
        //}
/*        public LinkedListNode<Charactor> Node(int offset)
        {
            LinkedListNode<Charactor> target = Charactors.First;

            while (0 < offset-- && target != null)
                target = target.Next;
            return target;
        }
*/

        public void DEBUG_TEST()
        {
            var list = from pair in Charactors
                       where pair.Value != null
                       orderby pair.Value.SlotId ascending
                       select pair.Value;

            foreach (Charactor c in list)
            {
                Debug.Log($"name = {c.Name}, uid = {c.uid}, slotId = {c.SlotId}");
            }
        }

    }
}
