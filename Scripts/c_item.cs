using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

using Popup.Converter;
using Popup.Defines;
using Popup.Library;
using Popup.Framework;





namespace Popup.Items
{
    public class c_item
    {
        private static readonly Lazy<c_item> instance = new Lazy<c_item>(() => new c_item());

        public static c_item Instance => instance.Value;

        private readonly Dictionary<int, (Item item, PItemBase itemBase)> dictionary;

        private c_item() => dictionary = new Dictionary<int, (Item item, PItemBase itemBase)>();

        public (Item item, PItemBase itemBase) MakeItem(string json, Transform parent)
        {
            if (json == null) return (null, null);

            Item item = FromJson.ToItem(json);
            PItemBase itemBase = ExplictPrefabType(item, parent);

            dictionary.Add(itemBase.GetInstanceID(), (item, itemBase));
            itemBase.UpdateIconImage(item);
            return (item, itemBase);
        }

        /********************************/
        /* Events                       */
        /********************************/

        public void Release(object _, PItemBase e)
        {
            ObjectPool.Instance.Release(ExplictPrefabEnum(e), e.gameObject);
            dictionary.Remove(e.GetInstanceID());
        }

        public void UpdateCount(object _, Item e) => dictionary[FindKeyFromItem(e)].itemBase.UpdateCount(e);

        public void Use(object _, PItemBase e) => dictionary[e.GetInstanceID()].item.Decrement(1);

        /********************************/
        /* Sub func                     */
        /********************************/

        private int FindKeyFromItem(Item item) => dictionary.FirstOrDefault(e => e.Value.item.Equals(item)).Key;

        private Prefab ExplictPrefabEnum(PItemBase itemBase)
        {
            if (itemBase is PStackableItem)
                return Prefab.StackableItem;
            if (itemBase is PSolidItem)
                return Prefab.SolidItem;
            return Prefab.TextMesh;
        }

        private PItemBase ExplictPrefabType(Item item, Transform parent)
        {
            if (item is SolidItem)
                return ObjectPool.Instance.Get<PSolidItem>(Prefab.SolidItem, parent);
            if (item is StackableItem)
                return ObjectPool.Instance.Get<PStackableItem>(Prefab.StackableItem, parent);
            return null;
        }
    }
}
