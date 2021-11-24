using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Popup.Items;



public class ItemPrefab : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    Item item;
    [SerializeField] Image image;



    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        transform.SetAsLastSibling();   // test code
        eventData.selectedObject = gameObject;
        Debug.Log("OnBeginDrag Call");
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        //Debug.Log($"OnDrag Call {eventData.position}");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        Debug.Log("OnEndDrag Call");
    }
}
