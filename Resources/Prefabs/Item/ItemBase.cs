using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Popup.Items;
using Popup.Framework;
using Popup.Configs;



public abstract class ItemBase : MonoBehaviour, IITemHandler
{
    Vector2 offset = default;
    [SerializeField] Image image = null;
    public Transform lastParent = null;

    int clickCount = 0;
    float clickTime = 0f;



    public abstract void Use();

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

    public void OnPointerDown(PointerEventData eventData)
    {
        offset = (Vector2)transform.position - eventData.position;
        transform.SetParent(Manager.Instance.guiGuide.canvas.transform);
        transform.SetAsLastSibling();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.SetParent(lastParent);
        transform.localPosition = Vector3.zero;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (0 < clickCount && eventData.clickTime - clickTime < Config.doubleClickInterval)
        {
            clickCount = 0;
            Use();
        }
        else
        {
            clickCount = 1;
            clickTime = eventData.clickTime;
        }
    }
}
