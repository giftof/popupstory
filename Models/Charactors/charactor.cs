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
    [Serializable]
    public class Charactor : IPopupObject
    {
        [SerializeField]
        string          name;
        [SerializeField]
        readonly int    uid;
        [SerializeField]
        int             level;
        [SerializeField]
        int             exp;
        [SerializeField]
        int             maxHp;
        [SerializeField]
        int             curHp;
        [SerializeField]
        int             maxMp;
        [SerializeField]
        int             curMp;
        [SerializeField]
        int             speed;
        [SerializeField]
        int             power;
        [SerializeField]
        Buff[]          buffArray;
        [SerializeField]
        Spell[]         spellArray;
        [SerializeField]
        Item[]          equipArray;
        //Ivn             inventory;


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


        void TakeAffect(ref Spell takeSpell)
        {

        }

        void GiveAffect(int spellIndex, params Charactor[] targetArray)
        {
            Guard.MustInRange(spellIndex, ref this.spellArray, "[GiveAffect in charactor]");

            foreach (Charactor target in targetArray)
            {
                target.TakeAffect(ref this.spellArray[spellIndex]);
            }
        }

        void ChangeName(string newName)
        {
            // check is name changeable
            name = newName;
        }

        void TakeExp(int amount)
        {
            Libs.IncreaseValue(ref exp, amount);
            while (Libs.IsUnder((level + 1) * (level + 1), exp)) { ++level; }
            Alignment.ToInclude(ref level, (Cfg.minLevel, Cfg.maxLevel));
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
