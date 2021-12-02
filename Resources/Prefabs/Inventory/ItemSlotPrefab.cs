using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Popup.Delegate;
using Popup.Items;



public partial class ItemSlotPrefab : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Sprite[] sprites;
    public int slotId;
    public ItemAction insertAction = null;
    public ItemAction removeAction = null;



    public void AddInsertAction(ItemAction itemAction) => insertAction += itemAction;
    public void AddRemoveAction(ItemAction itemAction) => removeAction += itemAction;

    private void MoveAction(ItemBase item, ItemSlotPrefab from, ItemSlotPrefab to)
    {
        item.SetSlotId(to.slotId);
        from.removeAction?.Invoke(item.Item);
        to.insertAction?.Invoke(item.Item);
    }

    private void SetParent(ItemBase dest, Transform parent)
    {
        dest.transform.SetParent(parent);
        dest.transform.localPosition = Vector3.zero;
        dest.lastParent = parent;
    }
}



public partial class ItemSlotPrefab : IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.selectedObject != null && eventData.selectedObject.TryGetComponent(out ItemBase item))
        {
            if (0 < transform.childCount)
            {
                ItemBase currentItem = transform.GetChild(0).GetComponent<ItemBase>();
                MoveAction(currentItem, this, item.lastParent.GetComponent<ItemSlotPrefab>());
                SetParent(currentItem, item.lastParent);
            }

            MoveAction(item, item.lastParent.GetComponent<ItemSlotPrefab>(), this);
            SetParent(item, transform);
        }
    }
}
