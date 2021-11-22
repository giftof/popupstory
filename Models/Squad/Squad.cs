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
        public LinkedList<Charactor> Charactors { get; protected set; }
        //public Charactor[] Charactors { get; protected set; }
        [JsonIgnore]
        private int OccupiedSize { get; set; }
        [JsonProperty]
        public Inventory Inventory { get; protected set; }
		[JsonProperty]
        public int ActivateCharactorIndex { get; protected set; }
		[JsonProperty]
        public bool ActivateTurn { get; protected set; }


        public Squad(int uid, int inventorySize = Configs.squadInventorySize)
        {
            this.uid = uid;
            OccupiedSize = 0;
            Charactors = new LinkedList<Charactor>();
            Inventory = new WareHouse(inventorySize);
        }


        public int GetUID() => uid;

        //public bool IsExist => 0 < Charactors.Count;
        public bool IsExist => Charactors.FirstOrDefault(c => c.IsAlive) != null;
        public object DeepCopy(int? uid = null, int? _ = null)
        {
            Inventory.EraseExhaustedSlot();

            Squad squad = (Squad)MemberwiseClone();
            squad.uid = uid ?? squad.uid;

            return squad;
        }

        public bool Use(Item item) => Inventory.Use(item);
        public bool Add(Item item) => Inventory.Add(item);
        public bool Add(Charactor charactor)
        {
            if (!charactor.IsExist) return false;
            if (Configs.squadSize < OccupiedSize + charactor.Size) return false;


            OccupiedSize += charactor.Size;
            return Charactors.AddLast(charactor) != null;
        }
        public void SetName(string name) => Name = name;

        private void Crash(Charactor target, Spell spell) => target?.TakeAffect(spell);

        public (uint, LinkedListNode<Charactor>) MoveBackward(LinkedListNode<Charactor> target, uint step)
        {
            if (target.Value.uid.Equals(Charactors.Last.Value.uid)) return (0, null);
            LinkedListNode<Charactor> position = target;

            while (position.Next != null && position.Next.Value.Size <= step)
            {
                Crash(position.Value, null);
                Crash(position.Next.Value, null);
                step -= (uint)position.Next.Value.Size;
                position = position.Next;
            }

            Charactors.Remove(target);
            Charactors.AddAfter(position, target);
            return (step, target.Next);
        }

        public (uint, LinkedListNode<Charactor>) MoveForward(LinkedListNode<Charactor> target, uint step)
        {
            if (target.Value.uid.Equals(Charactors.First.Value.uid)) return (0, null);
            LinkedListNode<Charactor> position = target;

            while (position.Previous != null && position.Previous.Value.Size <= step)
            {
                Crash(position.Value, null);
                Crash(position.Previous.Value, null);
                step -= (uint)position.Value.Size;
                position = position.Previous;
            }

            Charactors.Remove(target);
            Charactors.AddAfter(position, target);
            return (step, target.Previous);
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
        public LinkedListNode<Charactor> Node(int offset)
        {
            LinkedListNode<Charactor> target = Charactors.First;

            while (0 < offset-- && target != null)
                target = target.Next;
            return target;
        }


        public void DEBUG_TEST()
        {
            foreach (Charactor c in Charactors)
            {
                Debug.Log($"name = {c.Name}");
            }
        }

    }
}
