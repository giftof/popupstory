using System;
using System.Collections.Generic;
using UnityEngine;

using Popup.Converter;
using Popup.Defines;





namespace Popup.Items
{
    public class c_item
    {
        private static readonly Lazy<c_item> instance = new Lazy<c_item>(() => new c_item());
        public static c_item Instance => instance.Value;

        private readonly Dictionary<int, (Item item, PItemBase itemBase)> dictionary;
        private c_item() => dictionary = new Dictionary<int, (Item item, PItemBase itemBase)>();

        //public (Item item, PItemBase itemBase) MakeItem(string json, Transform parent)
        public PItemBase MakeItem(string json, Transform parent)
        {
            Item item = FromJson.ToItem(json);
            PItemBase itemBase = GetExplictPrefabType(item, parent);

            dictionary.Add(itemBase.GetInstanceID(), (item, itemBase));
            //return (item, itemBase);
            return itemBase;
        }



        public void Release(object sender, (Item item, PItemBase itemBase) e)
        {
            Debug.Log($"instenceId = {((MonoBehaviour)sender).gameObject.GetInstanceID()}");

            Prefab prefab = GetExplictPrefab(e.item);

            ObjectPool.Instance.Release(prefab, e.itemBase.gameObject);
            dictionary.Remove(e.itemBase.GetInstanceID());
        }



        private Prefab GetExplictPrefab(Item item)
        {
            if (item is StackableItem)
                return Prefab.StackableItem;
            if (item is SolidItem)
                return Prefab.SolidItem;
            return Prefab.TextMesh;
        }

        private PItemBase GetExplictPrefabType(Item item, Transform parent)
        {
            if (item is SolidItem)
                return ObjectPool.Instance.Get(Prefab.SolidItem, parent).GetComponent<PSolidItem>();
            if (item is StackableItem)
                return ObjectPool.Instance.Get(Prefab.StackableItem, parent).GetComponent<PStackableItem>();
            return null;
        }
    }
}
