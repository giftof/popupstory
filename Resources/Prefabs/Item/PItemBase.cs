using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using Popup.Items;
using Popup.Framework;
using Popup.Configs;





public abstract class PItemBase : MonoBehaviour, IItemHandler
{
    [SerializeField] Image image = null;

    void Start()
    {
        _useHandler += new EventHandler<PItemBase>(c_item.Instance.Use);
        _releaseHandler += new EventHandler<PItemBase>(c_item.Instance.Release);
    }

    /********************************/
    /* Delegate Action              */
    /********************************/

    public abstract void SetAmount(Item item);
    public void UpdateIconImage(Item item) => image.sprite = Resources.Load<Sprite>($"{Path.icon}{item.Icon}");

    /********************************/
    /* Events                       */
    /********************************/

    event EventHandler<PItemBase> _releaseHandler;
    event EventHandler<PItemBase> _useHandler;

    public void UpdateCount(Item item)
    {
        if (HaveDisplayPannel(item))
            SetAmount(item);
        if (!item.IsExist)
            _releaseHandler?.Invoke(this, this);
    }

    /********************************/
    /* Implement interface          */
    /********************************/

    public void OnDrag(PointerEventData eventData) => transform.position = eventData.position + offset;

    public void OnBeginDrag(PointerEventData eventData)
    {
        eventData.selectedObject = gameObject;
        image.raycastTarget = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //if (LastParentSlot != null) {
        //    transform.SetParent(LastParentSlot.transform);
        //    transform.localPosition = Vector3.zero;
        //}

        transform.localPosition = Vector3.zero;
    }

    private Vector2 offset = default;
    public void OnPointerDown(PointerEventData eventData)
    {
        offset = (Vector2)transform.position - eventData.position;
        transform.SetParent(Manager.Instance.guiGuide.pickCanvas.transform);
    }

    int clickCount = 0;
    float clickTime = 0f;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (0 < clickCount && eventData.clickTime - clickTime < Config.doubleClickInterval)
        {
            clickCount = 0;
            _useHandler?.Invoke(this, this);
        }
        else
        {
            clickCount = 1;
            clickTime = eventData.clickTime;
        }
    }

    /********************************/
    /* Sub                          */
    /********************************/

    private bool HaveDisplayPannel(Item item) => item is StackableItem;
}
