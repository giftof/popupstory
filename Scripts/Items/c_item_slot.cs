using UnityEngine;
using System;
using System.Collections.Generic;

using Popup.Defines;





public class c_item_slot
{
    private static readonly Lazy<c_item_slot> instance = new Lazy<c_item_slot>(() => new c_item_slot());
    public static c_item_slot Instance => instance.Value;

    private readonly Dictionary<int, PItemSlot> dictionary;
    private c_item_slot() => dictionary = new Dictionary<int, PItemSlot>();

    public PItemSlot MakeSlot(Transform parent)
    {
        PItemSlot itemSlot = ObjectPool.Instance.Get(Prefab.ItemSlot, parent).GetComponent<PItemSlot>();
        dictionary.Add(itemSlot.GetInstanceID(), itemSlot);
        return itemSlot;
    }

    public void ItemDrop(object sender, PItemBase e)
    {
        Debug.Log("begin ItemDropEvent");
        Debug.Log($"instenceId = {((MonoBehaviour)sender).gameObject.GetInstanceID()}");
        Debug.Log($"name = {e.Item.Name}");
        Debug.Log("end ItemDropEvent");
    }

    public void Release(object sender, PItemSlot e)
    {
        Debug.Log("begin ItemDropEvent");
        Debug.Log($"instenceId = {((MonoBehaviour)sender).gameObject.GetInstanceID()}");
        Debug.Log($"instenceId = {e.GetInstanceID()}");
        Debug.Log($"bgImage = {e.bgImage}");
        Debug.Log("end ItemDropEvent");

        ObjectPool.Instance.Release(Prefab.ItemSlot, e.gameObject);
        dictionary.Remove(e.GetInstanceID());
    }

}
