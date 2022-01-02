using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Popup.Delegate;
using Popup.Items;



public partial class PItemSlot : MonoBehaviour, IDropHandler {
    public int slotId;
    public ActionWithItem InsertDelegate = null;
    public ActionWithItem RemoveDelegate = null;
    public PItemBase CurrentItem { get; set; } = null;



    /********************************/
    /* Behaviours funcs             */
    /********************************/

    public void SetInsertData(ActionWithItem itemAction) => InsertDelegate = itemAction;
    public void AddInsertData(ActionWithItem itemAction) => InsertDelegate += itemAction;
    public void SetRemoveData(ActionWithItem itemAction) => RemoveDelegate = itemAction;
    public void AddRemoveData(ActionWithItem itemAction) => RemoveDelegate += itemAction;
    public void PutItem(PItemBase item) {
        SetData(item, null, this);
        SetTransform(item, this);
    }


    /********************************/
    /* Implement Interface          */
    /********************************/

    public void OnDrop(PointerEventData eventData) {
        if (eventData.selectedObject == null)
            return;

        if (eventData.selectedObject.TryGetComponent(out PItemBase selectedItem)) {

            Debug.Log(CurrentItem);
            Debug.Log(selectedItem);

            SetData(CurrentItem, this, selectedItem.lastParentSlot);
            SetTransform(CurrentItem, selectedItem.lastParentSlot);

            SetData(selectedItem, selectedItem.lastParentSlot, this);
            SetTransform(selectedItem, this);
        }
    }

    private void SetData(PItemBase item, PItemSlot from, PItemSlot to) {
        if (item == null)
            return;
        item.Item.SetSlotId = to.slotId;
        from?.RemoveDelegate?.Invoke(item.Item);
        to?.InsertDelegate?.Invoke(item.Item);
    }

    private void SetTransform(PItemBase dest, PItemSlot slot) {
        if (dest != null)
            dest.lastParentSlot = slot;
        slot.CurrentItem = dest;
    }
}
