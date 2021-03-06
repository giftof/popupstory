using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Popup.Items;



public class ItemPrefab : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    m_item item;
    Vector2 offset;
    [SerializeField] Image image;



    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        offset = (Vector2)transform.position - eventData.position;
        transform.SetAsLastSibling();   // test code
        eventData.selectedObject = gameObject;
        Debug.Log("OnBeginDrag Call");
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position + offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        Debug.Log("OnEndDrag Call");
    }
}
