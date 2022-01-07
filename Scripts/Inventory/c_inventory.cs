using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

using Popup.Defines;
using Popup.Items;




namespace Popup.Inventory
{
    public class c_inventory
    {
        private static readonly Lazy<c_inventory> instance = new Lazy<c_inventory>(() => new c_inventory());

        public static c_inventory Instance
        {
            get { return instance.Value; }
        }

        private readonly Dictionary<int, (m_inventory inventory, PInventoryBase inventoryBase)> dictionary;

        private c_inventory()
        {
            dictionary = new Dictionary<int, (m_inventory inventory, PInventoryBase inventoryBase)>();
        }

        public void MakeInventory(bool destructible, uint size, string[] itemJsonArray)
        {
            PInventoryBase inventoryBase = ObjectPool.Instance.Get<PInventoryBase>(Prefab.Pouch, null);
            m_inventory inventory = new m_inventory_pouch(size);

            inventory.Destructible = destructible;

            foreach (var e in itemJsonArray)
            {
                PItemSlot itemSlot = c_item_slot.Instance.MakeSlot(inventoryBase.transform, e);
                inventory.slotInstanceIdList.Add(itemSlot.GetInstanceID());
            }

            dictionary.Add(inventoryBase.GetInstanceID(), (inventory, inventoryBase));
        }

        /********************************/
        /* Checker						*/
        /********************************/

        public bool HaveItem(m_item item)
        {
            return c_item.Instance.HaveItem(item);
        }

        public m_inventory WhichInventory(m_item item)
        {
            PItemBase itemBase = c_item.Instance.FindItemBase(item);
            PItemSlot itemSlot = c_item_slot.Instance.FindItemSlot(itemBase);
            int slotInstanceId = itemSlot.GetInstanceID();

            return dictionary.FirstOrDefault(
                e1 => e1.Value.inventory.slotInstanceIdList.FirstOrDefault(
                    e2 => e2 == slotInstanceId) != 0).Value.inventory;
        }

        /********************************/
        /* Behaviour                    */
        /********************************/

        //public PItemSlot MakeSlot(Transform parent, string itemJson = null)
        //{
        //    PItemSlot itemSlot = ObjectPool.Instance.Get<PItemSlot>(Prefab.ItemSlot, parent);
        //    PItemBase itemBase = c_item.Instance.MakeItem(itemJson, itemSlot.transform).itemBase;
        //    dictionary.Add(itemSlot.GetInstanceID(), (itemSlot, itemBase));

        //    return itemSlot;
        //}


        /********************************/
        /* Events                       */
        /********************************/

        //public void Release(object _, PItemBase e)
        //{
        //    ObjectPool.Instance.Release(ExplictPrefabEnum(e), e.gameObject);
        //    dictionary.Remove(e.GetInstanceID());
        //}

        //public void UpdateCount(object _, m_item e)
        //{
        //    int key = FindKeyFromItem(e);

        //    dictionary[key].itemBase.UpdateCount(e);
        //}

        //public void Use(object _, PItemBase e)
        //{
        //    int key = e.GetInstanceID();

        //    dictionary[key].item.Decrement(1);
        //}

    }

    public static class m_inventory_extension
    {
        public static void AddItem(this m_inventory inventory, m_item item)
        {

        }
    }
}
