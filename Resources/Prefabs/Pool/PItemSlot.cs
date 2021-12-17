using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Popup.Delegate;
using Popup.Items;



public partial class PItemSlot : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Sprite[] sprites;
    public int slotId;
    public ItemAction insertAction = null;
    public ItemAction removeAction = null;



    public void AddInsertAction(ItemAction itemAction) => insertAction += itemAction;
    public void AddRemoveAction(ItemAction itemAction) => removeAction += itemAction;

    private void MoveAction(PItemBase item, PItemSlot from, PItemSlot to)
    {
        item.SetSlotId(to.slotId);
        from.removeAction?.Invoke(item.Item);
        to.insertAction?.Invoke(item.Item);
    }

    private void SetParent(PItemBase dest, Transform parent)
    {
        dest.transform.SetParent(parent);
        dest.transform.localPosition = Vector3.zero;
        dest.lastParent = parent;
    }
}



public partial class PItemSlot : IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.selectedObject != null && eventData.selectedObject.TryGetComponent(out PItemBase item))
        {
            if (0 < transform.childCount)
            {
                PItemBase currentItem = transform.GetChild(0).GetComponent<PItemBase>();
                MoveAction(currentItem, this, item.lastParent.GetComponent<PItemSlot>());
                SetParent(currentItem, item.lastParent);
            }

            MoveAction(item, item.lastParent.GetComponent<PItemSlot>(), this);
            SetParent(item, transform);
        }
    }
}
