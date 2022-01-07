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

    public m_item[] REQ_NEW_ITEM_BY_ITEMID(params int[] itemIdArray)
    {
        List<m_item> list = new List<m_item>();
        string[] array = REQ_NEW_ITEMDATA_BY_ITEMID(itemIdArray);

        foreach (string element in array)
        {
            list.Add(FromJson.ToItem(element));
            //Debug.Log($"src = {element}");
            //m_item item = FromJson.ToItem(element);

            //Debug.Log($"name = {item?.Name}");
            //list.Add(item);
        }

        return list.ToArray();
    }

    public string[] REQ_NEW_ITEMDATA_BY_ITEMID(params int[] itemIdArray)
    {
        return Manager.Instance.server.RequestNewItem(itemIdArray);
    }
}
