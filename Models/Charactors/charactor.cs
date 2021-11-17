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
    public class Charactor : IPopupObject
    {
        [JsonProperty]
        public string name { get; protected set; }
        [JsonProperty]
        public int uid { get; protected set; }
		[JsonProperty]
		public int slotId { get; protected set; }
        [JsonProperty]
        public int level { get; protected set; }
        [JsonProperty]
        public int exp { get; protected set; }
        [JsonProperty]
        public int maxHp { get; protected set; }
        [JsonProperty]
        public int curHp { get; protected set; }
        [JsonProperty]
        public int maxMp { get; protected set; }
        [JsonProperty]
        public int curMp { get; protected set; }
        [JsonProperty]
        public int speed { get; protected set; }
        [JsonProperty]
        public int power { get; protected set; }
        [JsonProperty]
        public Buff[] buffArray { get; protected set; }
        [JsonProperty]
        public Spell[] spellArray { get; protected set; }
        [JsonProperty]
        public Item[] equipArray { get; protected set; }


        // public string   GetName         => name;
        // public int      GetLevel        => level;
        // public int      GetMaxHp        => maxHp;
        // public int      GetMaxMp        => maxMp;
        // public int      GetCurrentHp    => curHp;
        // public int      GetCurrentMp    => curMp;
        // public int      GetSpeed        => speed;
        // public int      GetPower        => power;
        // public Buff[]   GetBuffArray    => buffArray;
        // public Spell[]  GetSpellArray   => spellArray;
        // public Item[]   GetEquipArray   => equipArray;


        // public int    GetUID()       => uid;
    	public bool IsExist => false; // impl.
        public object Duplicate() => MemberwiseClone();
        public object DuplicateNew() => (((Charactor)Duplicate()).uid = ServerJob.RequestNewUID);


        void TakeAffect(Spell takeSpell)
        {

        }

        void GiveAffect(int spellIndex, params Charactor[] targetArray)
        {
            Guard.MustInRange(spellIndex, this.spellArray, "[GiveAffect in charactor]");

            foreach (Charactor target in targetArray)
            {
                target.TakeAffect(this.spellArray[spellIndex]);
            }
        }

        void ChangeName(string newName)
        {
            // check is name changeable
            name = newName;
        }

        void TakeExp(int amount)
        {
            Libs.IncreaseValue(exp, amount);
            while (Libs.IsUnder((level + 1) * (level + 1), exp)) { ++level; }
            Alignment.ToInclude(level, (Cfg.minLevel, Cfg.maxLevel));
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
