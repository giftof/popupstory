using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

using Popup.Items;





public partial class PItemSlot : MonoBehaviour, IDropHandler
{
    public Image bgImage;

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

    event EventHandler<PItemBase> DropHandler;

    event EventHandler<PItemSlot> ReleaseHandler;

    public EventHandler<PItemBase> AddItemDropListener
    {
        set { DropHandler += new EventHandler<PItemBase>(value); }
    }

    public EventHandler<PItemSlot> AddReleaseHandler
    {
        set { ReleaseHandler += new EventHandler<PItemSlot>(value); }
    }

    public void Release()
    {
        ReleaseHandler?.Invoke(this, this);
    }
}
