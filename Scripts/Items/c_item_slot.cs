using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using System;



//public enum SlotEventEnum
//{
//    dropItem,
//}



//public class SlotEvent : EventArgs
//{
//    SlotEventEnum eventEnum;
//}



public class c_item_slot
{
    //private c_item_slot() => actionDic = new Dictionary<int, SlotEvent>();
    private static readonly Lazy<c_item_slot> instance = new Lazy<c_item_slot>(() => new c_item_slot());
    public static c_item_slot Instance => instance.Value;

    public void SlotDropEvent(object sender, SlotEventArgs e)
    {
        Debug.Log("begin button click");
        Debug.Log(e.CurrentItem);
        Debug.Log("click");
        Debug.Log("end button click");
    }


    //Dictionary<int, SlotEvent> actionDic;

    //public void AddEvent(int key, SlotEvent value)
    //{
    //    if (!actionDic.ContainsKey(key))
    //        actionDic.Add(key, value);
    //    //else
    //    //    actionDic[key] += value;
    //}

    //public void DoAction(int key)
    //{
    //    if (actionDic.ContainsKey(key))
    //        actionDic[key].Invoke();
    //}

    //public void OnDrop(PointerEventData eventData)
    //{
    //    if (eventData.selectedObject == null)
    //        return;

    //    if (eventData.selectedObject.TryGetComponent(out PItemBase selectedItem))
    //    {
    //        Debug.Log(selectedItem.name);
    //        //selectedItem.Item.SetSlotId = slotId;
    //        //selectedItem.LastParentSlot = this;
    //        //selectedItem.transform.localPosition = Vector3.zero;

    //        //if (CurrentItemBase != null)
    //        //{
    //        //    CurrentItemBase.LastParentSlot = selectedItem.LastParentSlot;

    //        //}
    //        //    SetData(CurrentItem, this, selectedItem.lastParentSlot);
    //        //    SetTransform(CurrentItem, selectedItem.lastParentSlot);
    //        //    SetData(selectedItem, selectedItem.lastParentSlot, this);
    //        //    SetTransform(selectedItem, this);
    //    }
    //}

    //private void Swap(PItemBase come, PItemBase current)
    //{
    //    CurrentItemBase = come;
    //    come.LastParentSlot.CurrentItemBase = current;




    //}

}
