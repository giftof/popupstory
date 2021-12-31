using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Popup.Delegate;
using Popup.Items;



public partial class PItemSlot : MonoBehaviour
{
    public int slotId;
    public ItemAction insert = null;
    public ItemAction remove = null;



    public void AddInsertAction(ItemAction itemAction) => insert += itemAction;
    public void AddRemoveAction(ItemAction itemAction) => remove += itemAction;

    private void MoveAction(PItemBase item, PItemSlot from, PItemSlot to) {
        item.Item.SetSlotId = to.slotId;
        from.remove?.Invoke(item.Item);
        to.insert?.Invoke(item.Item);
    }

    private void SetParent(PItemBase dest, Transform parent) {
        dest.transform.SetParent(parent);
        dest.transform.localPosition = Vector3.zero;
        dest.lastParent = parent;
    }
}



public partial class PItemSlot : IDropHandler {
    public void OnDrop(PointerEventData eventData) {
        if (eventData.selectedObject != null && eventData.selectedObject.TryGetComponent(out PItemBase item)) {
            if (0 < transform.childCount) {
                PItemBase currentItem = transform.GetChild(0).GetComponent<PItemBase>();
                MoveAction(currentItem, this, item.lastParent.GetComponent<PItemSlot>());
                SetParent(currentItem, item.lastParent);
            }

            MoveAction(item, item.lastParent.GetComponent<PItemSlot>(), this);
            SetParent(item, transform);
        }
    }
}
