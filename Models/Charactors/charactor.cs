using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using Popup.Utils;
using Popup.Items;
using System;





namespace Popup.Charactor
{
    using Cfg = Configs.Configs;
    //using Ivn = Inventory.Inventory;
    public struct Charactor
    {
        string          name;
        readonly int    uid;
        int             level;
        int             exp;
        int             maxHp;
        int             maxMp;
        int             speed;
        int             damage;
        Buff[]          buffs;
        Spell[]         spells;
        Item[]          equips;
        //Ivn             inventory;



        void TakeAffect(ref Spell takeSpell)
        {

        }

        void GiveAffect(int spellIndex, params Charactor[] targetArray)
        {
            Guard.InRange(spellIndex, ref this.spells);

            foreach (Charactor target in targetArray)
            {
                target.TakeAffect(ref this.spells[spellIndex]);
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
