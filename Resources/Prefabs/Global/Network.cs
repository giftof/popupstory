using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Popup.Items;
using Popup.Defines;
using Popup.Converter;



public class Network : MonoBehaviour
{
    public int REQ_NEW_ID() => Manager.Instance.server.RequestNewUID;

    public Item[] REQ_NEW_ITEM_BY_ITEMID(params int[] itemIdArray) {
        List<Item> list = new List<Item>();
        string[] array = Manager.Instance.server.RequestNewItem(itemIdArray);

        foreach (string element in array) {
            Debug.Log($"src = {element}");
            Item item = FromJson.ToItem(element);

            Debug.Log($"name = {item?.Name}");
            //item.SetUID = REQ_NEW_ID();
            list.Add(item);
        }

        return list.ToArray();
    }
}
