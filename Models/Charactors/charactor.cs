using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Popup.Library;
using Popup.Items;
using Popup.Framework;
using System;





namespace Popup.Charactors
{
    using Cfg = Configs.Configs;
    //using Ivn = Inventory.Inventory;
    public class Charactor : IPopupObject
    {
        public string name { get; protected set; }
        public int uid { get; protected set; }
        public int level { get; protected set; }
        public int exp { get; protected set; }
        public int maxHp { get; protected set; }
        public int curHp { get; protected set; }
        public int maxMp { get; protected set; }
        public int curMp { get; protected set; }
        public int speed { get; protected set; }
        public int power { get; protected set; }
        public Buff[] buffArray { get; protected set; }
        public Spell[] spellArray { get; protected set; }
        public Item[] equipArray { get; protected set; }


        public string   GetName         => name;
        public int      GetLevel        => level;
        public int      GetMaxHp        => maxHp;
        public int      GetMaxMp        => maxMp;
        public int      GetCurrentHp    => curHp;
        public int      GetCurrentMp    => curMp;
        public int      GetSpeed        => speed;
        public int      GetPower        => power;
        public Buff[]   GetBuffArray    => buffArray;
        public Spell[]  GetSpellArray   => spellArray;
        public Item[]   GetEquipArray   => equipArray;


        public int    GetUID()       => uid;
        public object Duplicate()    => null; // impl.
        public object DuplicateNew() => null; // impl.


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
