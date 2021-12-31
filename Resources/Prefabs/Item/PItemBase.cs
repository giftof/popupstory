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



public abstract partial class PItemBase : MonoBehaviour {
    [SerializeField] Image image = null;
    public Transform lastParent = null;
    private Action useAction = null;
    private Vector2 offset = default;

    int clickCount = 0;
    float clickTime = 0f;

    public Item Item { get; set; }
    public void SetIconImage() => StartCoroutine(LoadSprite(Item.Icon));

    IEnumerator LoadSprite(int iconImageId) {
        Sprite sprite = Resources.Load<Sprite>($"{Path.icon}{iconImageId}");
        yield return sprite;
        image.sprite = sprite;
    }

    public void SetUseAction(Action action) => useAction = action;
    public void AddUseAction(Action action) => useAction += action;
}



public abstract partial class PItemBase {
    public abstract Prefab Type { get; }
    //public abstract void SetAmount(int amount);
    public abstract void SetAmount();
}



public abstract partial class PItemBase : IITemHandler {
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
        if (lastParent != null) {
            transform.SetParent(lastParent);
            transform.localPosition = Vector3.zero;
        }
    }

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
