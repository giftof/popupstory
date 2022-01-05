using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

using Popup.Delegate;
using Popup.Items;





public enum SlotEventArgsEnum
{
    drop,
}





public class SlotEventArgs : EventArgs
{
    public SlotEventArgsEnum Enum { get; set; } = SlotEventArgsEnum.drop;
    public PItemBase CurrentItem { get; set; } = null;
    public PItemBase NewItem { get; set; } = null;

}





public class SlotEvent
{
    public event EventHandler<SlotEventArgs> slotEvent;
    public SlotEventArgs args = new SlotEventArgs();

    public void OnDrop() => slotEvent?.Invoke(this, args);
}





public partial class PItemSlot : MonoBehaviour, IDropHandler
{
    private int instanceId;
    SlotEvent slotEvent;


    private void Start()
    {
        slotEvent = new SlotEvent();
        instanceId = GetInstanceID();

        //c_item_slot.Instance.AddEvent(instanceId, slotEvent);
        //slotEvent.slotEvent += new EventHandler<SlotEventArgs>(SlotDropEvent);
        slotEvent.slotEvent += new EventHandler<SlotEventArgs>(c_item_slot.Instance.SlotDropEvent);
    }

    public Image bgImage;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.selectedObject == null)
            return;

        if (eventData.selectedObject.TryGetComponent(out PItemBase selectedItem))
        {
            selectedItem.transform.localPosition = Vector3.zero;

            slotEvent.args.CurrentItem = null;
            slotEvent.args.NewItem = null;
            slotEvent.args.Enum = SlotEventArgsEnum.drop;
            slotEvent.OnDrop();
        }
    }


    //void SlotDropEvent(object sender, SlotEventArgs e)
    //{
    //    Debug.Log("begin button click");
    //    //Debug.Log(e.OnDrop());
    //    Debug.Log("click");
    //    Debug.Log("end button click");
    //}

    private void Swap(PItemBase currentItem, PItemBase newItem)
    {

    }

    //private PItemBase Current()
    //{
    //    FindObjectOfType(PItemBase);
    //    return null;
    //}
}




//public class CustomEvent : EventArgs {
//    public int intValue;
//}

//public class TestEvent {
//    public event EventHandler<CustomEvent> Click;

//    public void MouseButtonDown() {
//        CustomEvent customEvent = new CustomEvent { intValue = 42 };
//        Click?.Invoke(this, customEvent);
//    }
//}



//void ButtonClick(object sender, CustomEvent e)
//{
//    Debug.Log("begin button click");
//    Debug.Log(e.intValue);
//    Debug.Log("click");
//    Debug.Log("end button click");
//}

//TestEvent testEvent = new TestEvent();
//testEvent.Click += new EventHandler<CustomEvent>(ButtonClick);
//testEvent.MouseButtonDown();
