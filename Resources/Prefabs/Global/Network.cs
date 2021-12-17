using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Popup.Items;
using Popup.Defines;



public class Network : MonoBehaviour
{
    public int REQ_NEW_ID() => Manager.Instance.server.RequestNewUID;
    public T[] REQ_NEW_ITEM_BY_ITEMID<T>(params int[] itemId) where T: Item
    {
        List<T> list = new List<T>();
        string[] array = Manager.Instance.server.RequestNewItem(itemId);

        foreach (string element in array)
        {
            T item = JsonConvert.DeserializeObject<T>(element);
            item.SetUID = REQ_NEW_ID();
            list.Add(item);
        }

        return list.ToArray();
    }
    //public EquipItem[] REQ_NEW_ITEMS_BY_EQUIPITEMID(params int[] itemId)
    //{
    //    List<EquipItem> list = new List<EquipItem>();
    //    string[] array = Manager.Instance.server.RequestNewItem(itemId);

    //    foreach (string element in array)
    //    {
    //        EquipItem item = JsonConvert.DeserializeObject<EquipItem>(element);
    //        item.DEBUG_TEST_SET_UID(REQ_NEW_ID());
    //        list.Add(item);
    //    }

    //    return list.ToArray();
    //}

    //public ToolItem[] REQ_NEW_ITEMS_BY_TOOLITEMID(params int[] itemId)
    //{
    //    List<ToolItem> list = new List<ToolItem>();
    //    string[] array = Manager.Instance.server.RequestNewItem(itemId);

    //    foreach (string element in array)
    //    {
    //        ToolItem item = JsonConvert.DeserializeObject<ToolItem>(element);
    //        item.DEBUG_TEST_SET_UID(REQ_NEW_ID());
    //        list.Add(item);
    //    }

    //    return list.ToArray();
    //}

}
