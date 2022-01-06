using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

using Popup.Defines;
using Popup.Library;




namespace Popup.Items
{
    public class c_item_slot
    {
        private static readonly Lazy<c_item_slot> instance = new Lazy<c_item_slot>(() => new c_item_slot());
        public static c_item_slot Instance => instance.Value;

        private readonly Dictionary<int, (PItemSlot itemSlot, PItemBase itemBase)> dictionary;
        private c_item_slot()
        {
            dictionary = new Dictionary<int, (PItemSlot, PItemBase)>();
            _releaseHandler += new EventHandler<PItemBase>(c_item.Instance.Release);
        }

        public PItemSlot MakeSlot(Transform parent, string itemJson = null)
        {
            PItemSlot itemSlot = ObjectPool.Instance.Get<PItemSlot>(Prefab.ItemSlot, parent);
            PItemBase itemBase = c_item.Instance.MakeItem(itemJson, itemSlot.transform).itemBase;
            dictionary.Add(itemSlot.GetInstanceID(), (itemSlot, itemBase));

            return itemSlot;
        }

        /********************************/
        /* Events                       */
        /********************************/

        event EventHandler<PItemBase> _releaseHandler;

        public void ItemDrop(object sender, PItemBase e)
        {
            int currentSlotKey = Libs.InstanceId(sender);
            int newSlotKey = FindKeyFromItemBase(e);

            Swap(currentSlotKey, newSlotKey);
        }

        public void Release(object _, PItemSlot e)
        {
            ItemReleaseEvent(dictionary[e.GetInstanceID()].itemBase);
            ObjectPool.Instance.Release(Prefab.ItemSlot, e.gameObject);
            dictionary.Remove(e.GetInstanceID());

        }

        private void ItemReleaseEvent(PItemBase itemBase)
        {
            if (itemBase == null)
                return;

            _releaseHandler.Invoke(this, itemBase);
        }

        /********************************/
        /* Sub func                     */
        /********************************/

        private int FindKeyFromItemBase(PItemBase itemBase) => dictionary.FirstOrDefault(e => e.Value.itemBase?.Equals(itemBase) ?? false).Key;

        private void Swap(int key1, int key2)
        {
            (PItemSlot currentSlot, PItemBase currentItem) = dictionary[key1];
            (PItemSlot newSlot, PItemBase newItem) = dictionary[key2];

            dictionary[key1] = (currentSlot, newItem);
            dictionary[key2] = (newSlot, currentItem);

            newItem.transform.SetParent(currentSlot.transform);
            newItem.transform.localPosition = Vector3.zero;

            if (currentItem != null)
            {
                currentItem.transform.SetParent(newItem.transform);
                currentItem.transform.localPosition = Vector3.zero;
            }
        }
    }
}
