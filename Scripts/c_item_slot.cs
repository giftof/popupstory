using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

using Popup.Defines;
using Popup.Library;





namespace Popup.Items
{
    sealed public class c_item_slot
    {
        private static readonly Lazy<c_item_slot> instance = new Lazy<c_item_slot>(() => new c_item_slot());
        public static c_item_slot Instance
        {
            get { return instance.Value; }
        }

        private readonly Dictionary<int, (PItemSlot itemSlot, PItemBase itemBase)> dictionary;
        private c_item_slot()
        {
            dictionary = new Dictionary<int, (PItemSlot, PItemBase)>();
            ReleaseHandler += new EventHandler<PItemBase>(c_item.Instance.Release);
        }

        public PItemSlot MakeSlot(Transform parent, string itemJson = null)
        {
            PItemSlot itemSlot = ObjectPool.Instance.Get<PItemSlot>(Prefab.ItemSlot, parent);
            itemSlot.AddItemDropListener = ItemDrop;
            itemSlot.AddReleaseHandler = Release;

            PItemBase itemBase = c_item.Instance.MakeItem(itemSlot.transform, itemJson).itemBase;
            dictionary.Add(itemSlot.GetInstanceID(), (itemSlot, itemBase));

            return itemSlot;
        }

        /********************************/
        /* Behaviour                    */
        /********************************/

        public PItemSlot FindItemSlot(PItemBase itemBase)
        {
            return dictionary.FirstOrDefault(e => e.Value.itemBase?.Equals(itemBase) ?? false).Value.itemSlot;
        }

        /********************************/
        /* Events                       */
        /********************************/

        event EventHandler<PItemBase> ReleaseHandler;

        public void ItemDrop(object sender, PItemBase e)
        {
            int currentSlotKey = Libs.InstanceId(sender);
            int newSlotKey = FindKeyFromItemBase(e);

            m_item currentItem = FindItemFromItemBaseKey(FindPItemBaseFromItemSlotKey(currentSlotKey).GetInstanceID());
            m_item newItem = FindItemFromItemBaseKey(e.GetInstanceID());

            if (newItem.NameId.Equals(currentItem?.NameId) && currentItem is m_stackable_item)
            {
                Debug.LogWarning($"{currentItem.UseableCount}");
                Debug.LogWarning($"{newItem.UseableCount}");
                Debug.LogWarning("------------ CHARGE CALL -----------------");
                currentItem.Charge(newItem);
                Debug.LogWarning($"{currentItem.UseableCount}");
                Debug.LogWarning($"{newItem.UseableCount}");
            }
            else
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

            ReleaseHandler.Invoke(this, itemBase);
        }

        /********************************/
        /* Sub func                     */
        /********************************/

        public int FindKeyFromItemBase(PItemBase itemBase)
        {
            return dictionary.FirstOrDefault(e => e.Value.itemBase?.Equals(itemBase) ?? false).Key;
        }

        public PItemBase FindPItemBaseFromItemSlotKey(int key)
        {
            return dictionary[key].itemBase;
        }

        public m_item FindItemFromItemBaseKey(int key)
        {
            return c_item.Instance.FindItemFromKey(key);
        }

        public m_item FindItemFromItemSlotKey(int key)
        {
            return FindItemFromItemBaseKey(FindPItemBaseFromItemSlotKey(key).GetInstanceID());
        }

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
