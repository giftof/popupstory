using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

using Popup.Converter;
using Popup.Defines;





namespace Popup.Items
{
    sealed public class c_item
    {
        private static readonly Lazy<c_item> instance = new Lazy<c_item>(() => new c_item());

        public static c_item Instance
        {
            get { return instance.Value; }
        }

        private readonly Dictionary<int, (m_item item, PItemBase itemBase)> dictionary;

        private c_item()
        {
            dictionary = new Dictionary<int, (m_item item, PItemBase itemBase)>();
        }

        public (m_item item, PItemBase itemBase) MakeItem(Transform parent, string json)
        {
            if (json == null) return (null, null);

            m_item item = FromJson.ToItem(json);
            item.AddChangeUseableCountListener = UpdateCount;

            PItemBase itemBase = ExplictPrefabType(item, parent);
            itemBase.UpdateIconImage(item);

            dictionary.Add(itemBase.GetInstanceID(), (item, itemBase));
            return (item, itemBase);
        }

        /********************************/
        /* Checker                      */
        /********************************/

        public bool HaveItem(m_item item)
        {
            return dictionary.FirstOrDefault(e => e.Value.item.Equals(item)).Value.item != null;
        }

        public PItemBase FindItemBase(m_item item)
        {
            return dictionary.FirstOrDefault(e => e.Value.item.Equals(item)).Value.itemBase;
        }

        public m_item FindItemFromKey(int key)
        {
            return dictionary[key].item;
        }

        /********************************/
        /* Events                       */
        /********************************/

        public void Release(object _, PItemBase e)
        {
            ObjectPool.Instance.Release(ExplictPrefabEnum(e), e.gameObject);
            dictionary.Remove(e.GetInstanceID());
        }

        public void UpdateCount(object _, m_item e)
        {
            int key = FindKeyFromItem(e);
            
            dictionary[key].itemBase.UpdateCount(e);
        }

        public void Use(object _, PItemBase e)
        {
            int key = e.GetInstanceID();
            
            dictionary[key].item.Decrement(1);
        }

        /********************************/
        /* Sub func                     */
        /********************************/

        private int FindKeyFromItem(m_item item)
        {
            return dictionary.FirstOrDefault(e => e.Value.item.Equals(item)).Key;
        }

        private Prefab ExplictPrefabEnum(PItemBase itemBase)
        {
            if (itemBase is PStackableItem)
                return Prefab.StackableItem;
            if (itemBase is PSolidItem)
                return Prefab.SolidItem;
            return Prefab.TextMesh;
        }

        private PItemBase ExplictPrefabType(m_item item, Transform parent)
        {
            if (item is m_solid_item)
                return ObjectPool.Instance.Get<PSolidItem>(Prefab.SolidItem, parent);
            if (item is m_stackable_item)
                return ObjectPool.Instance.Get<PStackableItem>(Prefab.StackableItem, parent);
            return null;
        }
    }

    public static class m_item_extension
    {
        public static int Increment(this m_item item, int amount)
        {
            int increment = Math.Min(item.Space, amount);
            item.UseableCount += increment;
            item.DoChangeUseableCountHandler();
            return increment;
        }

        public static int Decrement(this m_item item, int amount)
        {
            int decrement = Math.Min(item.UseableCount, amount);
            item.UseableCount -= decrement;
            item.DoChangeUseableCountHandler();
            return decrement;
        }
    }
}
