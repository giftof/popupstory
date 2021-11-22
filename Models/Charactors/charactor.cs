using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Popup.Library;
using Popup.Items;
using Popup.Framework;
using Newtonsoft.Json;

using System;





namespace Popup.Charactors
{
    using Cfg = Configs.Configs;
    //using Ivn = Inventory.Inventory;
    using ServerJob = ServerJob.ServerJob;
    public class Charactor : ICharactor
    {
        [JsonProperty]
        //public string Name { get; protected set; }
        public string Name { get; set; }
        [JsonProperty]
        //public int uid { get; protected set; }
        public int uid { get; set; }
        [JsonProperty]
        //public int Size { get; protected set; }
        public int Size { get; set; }
        [JsonProperty]
		public int SlotId { get; protected set; }
        [JsonProperty]
        public int Level { get; protected set; }
        [JsonProperty]
        public int Exp { get; protected set; }
        [JsonProperty]
        public int MaxHp { get; protected set; }
        [JsonProperty]
        public int CurHp { get; protected set; }
        [JsonProperty]
        public int MaxMp { get; protected set; }
        [JsonProperty]
        public int CurMp { get; protected set; }
        [JsonProperty]
        public int Speed { get; protected set; }
        [JsonProperty]
        public int Power { get; protected set; }
        [JsonProperty]
        public Buff[] BuffArray { get; protected set; }
        [JsonProperty]
        public Spell[] SpellArray { get; protected set; }
        [JsonProperty]
        public Item[] EquipArray { get; protected set; }


        public bool IsAlive => 0 < CurHp;
        public bool IsCorpse => !IsAlive;
        public bool IsOccupied => true;


    	public bool IsExist => IsOccupied; // impl.
        public object DeepCopy(int? uid = null, int? notDefinedCount = null) => MemberwiseClone(); // impl.

        public void TakeAffect(params Spell[] spell)
        {
            foreach (Spell sp in spell)
                CurHp -= sp?.AffectiveValue(this) ?? 0;
        }

        public void GiveAffect(int spellIndex, params Charactor[] targetArray)
        {
            Guard.MustInRange(spellIndex, this.SpellArray, "[GiveAffect in charactor]");

            foreach (Charactor target in targetArray)
                target.TakeAffect(this.SpellArray[spellIndex]);
        }

        public void SetSlotId(int slotId) => SlotId = slotId;
        public void ShiftPosition(int offset) => SlotId += offset;

        void ChangeName(string newName)
        {
            // check is name changeable
            Name = newName;
        }

        void TakeExp(int amount)
        {
            Libs.IncreaseValue(Exp, amount);
            while (Libs.IsUnder((Level + 1) * (Level + 1), Exp)) { ++Level; }
            Alignment.ToInclude(Level, (Cfg.minLevel, Cfg.maxLevel));
        }

        void LoseExp(int amount) => TakeExp(-amount);

        void UseItem()
        {

        }

        void TakeItem()
        {

        }

        void GiveItem()
        {

        }
    }
}
