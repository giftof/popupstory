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
        if (eventData.selectedObject != null && eventData.selectedObject.TryGetComponent(out ItemBase item))
        {
            SwapHierarchy(item.lastParent);

            eventData.selectedObject.transform.SetParent(transform);
            eventData.selectedObject.transform.localPosition = Vector3.zero;
            item.lastParent = transform;
        }
    }

    private void SwapHierarchy(Transform newParent)
    {
        if (0 < transform.childCount && transform.GetChild(0).TryGetComponent(out ItemBase item))
        {
            item.transform.SetParent(newParent);
            item.transform.localPosition = Vector3.zero;
            item.lastParent = newParent;
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
