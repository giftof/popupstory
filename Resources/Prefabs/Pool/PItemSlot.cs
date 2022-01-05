using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;





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

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.selectedObject == null)
            return;

        if (eventData.selectedObject.TryGetComponent(out PItemBase selectedItem))
        {
            selectedItem.transform.localPosition = Vector3.zero;
            _dropHandler?.Invoke(this, selectedItem);
        }
    }

    public void Release() => _releaseHandler?.Invoke(this, this);
}
