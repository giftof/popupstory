using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



namespace Popup.Inventory
{
    public class m_inventory_pouch : m_inventory
    {
        public m_inventory_pouch(uint maxSize) : base(maxSize) { }

        public override object DeepCopy(int uid)
        {
            m_inventory_pouch inventoryPouch = (m_inventory_pouch)MemberwiseClone();

            inventoryPouch.Uid = uid;
            return inventoryPouch;
        }
    }
}

