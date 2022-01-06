using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

using Popup.Items;





public partial class PItemSlot : MonoBehaviour, IDropHandler
{
    public Image bgImage;
    event EventHandler<PItemBase> DropHandler;
    event EventHandler<PItemSlot> ReleaseHandler;

    private void Start()
    {
        DropHandler += new EventHandler<PItemBase>(c_item_slot.Instance.ItemDrop);
        ReleaseHandler += new EventHandler<PItemSlot>(c_item_slot.Instance.Release);
    }

    /********************************/
    /* Implement interface          */
    /********************************/

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.selectedObject == null)
            return;

        if (eventData.selectedObject.TryGetComponent(out PItemBase selectedItem))
            DropHandler?.Invoke(this, selectedItem);
    }

    /********************************/
    /* Events                       */
    /********************************/

    public void Release()
    {
        ReleaseHandler?.Invoke(this, this);
    }
}
