using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using Popup.Items;
using Popup.Framework;
using Popup.Configs;
using Popup.Defines;


// using UniRx;
// using UniRx.Triggers;


public abstract class PItemBase : MonoBehaviour, IITemHandler {

    void Start() {
        // var clickStream = this.UpdateAsObservable().Where(_ => Input.GetMouseButtonDown(0));
        // clickStream.Buffer(clickStream.Throttle(TimeSpan.FromSeconds(Config.doubleClickInterval))).Where(x => x.Count >= 2).Subscribe(x => Debug.Log($"x = {x.ToString()}, this is Double Click"));

    }

    [SerializeField] Image image = null;
    private PItemSlot m_lastParentSlot;
    public PItemSlot lastParentSlot {
        get => m_lastParentSlot;
        set {
            m_lastParentSlot = value;
            transform.SetParent(m_lastParentSlot.transform);
            transform.localPosition = Vector3.zero;
            m_lastParentSlot.CurrentItem = this;
        }
    }
    private Action useAction = null;
    private Vector2 offset = default;

    private Item m_item;
    public Item Item { 
        get => m_item;
        set {
            m_item = value;
            m_item.updateUseableConut = SetAmount;
            m_item.updateIcon = SetIconImage;
            m_item.removeEmptySlot = ReleaseObject;
            m_item.Reload();
        }
    }

    public void SetUseAction(Action action) => useAction = action;
    public void AddUseAction(Action action) => useAction += action;
    public void RemoveUseAction(Action action) => useAction -= action;
    public abstract Prefab Type { get; }



    /********************************/
    /* Delegate Action              */
    /********************************/

    public abstract void SetAmount();
    public void SetIconImage() => image.sprite = Resources.Load<Sprite>($"{Path.icon}{m_item.Icon}");
    public void ReleaseObject() => ObjectPool.Instance.Release(Type, gameObject);



    /********************************/
    /* IItemHandler                 */
    /********************************/

    public void OnDrag(PointerEventData eventData) => transform.position = eventData.position + offset;

    public void OnBeginDrag(PointerEventData eventData) {
        eventData.selectedObject = gameObject;
        image.raycastTarget = false;
    }

    public void OnEndDrag(PointerEventData eventData) {
        image.raycastTarget = true;
    }

    public void OnPointerDown(PointerEventData eventData) {
        offset = (Vector2)transform.position - eventData.position;
        transform.SetParent(Manager.Instance.guiGuide.pickCanvas.transform);
    }

    public void OnPointerUp(PointerEventData eventData) {
        if (lastParentSlot != null) {
            transform.SetParent(lastParentSlot.transform);
            transform.localPosition = Vector3.zero;
        }
    }

    int clickCount = 0;
    float clickTime = 0f;
    public void OnPointerClick(PointerEventData eventData) {
        if (0 < clickCount && eventData.clickTime - clickTime < Config.doubleClickInterval) {
            clickCount = 0;
            useAction.Invoke();
        }
        else {
            clickCount = 1;
            clickTime = eventData.clickTime;
        }
    }
}
