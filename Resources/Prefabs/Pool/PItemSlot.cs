using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

using Popup.Items;





public partial class PItemSlot : MonoBehaviour, IDropHandler
{
    public Image bgImage;
    event EventHandler<PItemBase> _dropHandler;
    event EventHandler<PItemSlot> _releaseHandler;

    private void Start()
    {
        _dropHandler += new EventHandler<PItemBase>(c_item_slot.Instance.ItemDrop);
        _releaseHandler += new EventHandler<PItemSlot>(c_item_slot.Instance.Release);
    }

    /********************************/
    /* Implement interface          */
    /********************************/

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.selectedObject == null)
            return;

        if (eventData.selectedObject.TryGetComponent(out PItemBase selectedItem))
            _dropHandler?.Invoke(this, selectedItem);
    }

    /********************************/
    /* Events                       */
    /********************************/

    public void Release() => _releaseHandler?.Invoke(this, this);
}
