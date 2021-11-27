using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;




public class ItemSlotPrefab : MonoBehaviour, IPointerEnterHandler, IDropHandler
{
    [SerializeField] Image image;
    [SerializeField] Sprite[] sprites;


    public void OnDrop(PointerEventData eventData)
    {
        ItemBase item = null;

        if (eventData.selectedObject?.TryGetComponent(out item) ?? false)
        {
            item.parent = transform;
            eventData.selectedObject.transform.SetParent(transform);
            eventData.selectedObject.transform.localPosition = Vector3.zero;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
/*
        ItemToolPrefab prefab = null;

        if (eventData.selectedObject?.TryGetComponent(out prefab) ?? false)
        {
            prefab.parent = transform;
        }
*/
    }
}
