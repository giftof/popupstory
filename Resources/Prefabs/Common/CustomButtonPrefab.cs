using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Popup.Delegate;
using Popup.Defines;
using Popup.Library;
using TMPro;





public class CustomButtonPrefab : MonoBehaviour, IPointerClickHandler
{
    private GameObject textMesh = null;
    private ButtonAction buttonClick = null;
    [SerializeField]
    private Image image;
    [SerializeField]
    private Sprite[] sprites;



    void Awake() => image.sprite = sprites[0];

    public void SetText(string message, Color color = default)
    {
        TextMeshProUGUI ugui;
        if (textMesh == null)
        {
            textMesh = (GameObject)ObjectPool.Instance.Request(Prefab.TextMesh);
            textMesh.SetActive(true);
            textMesh.transform.SetParent(transform);
            textMesh.transform.localPosition = Vector3.zero;
            textMesh.GetComponent<RectTransform>().sizeDelta = Vector2.one * 20;
        }
        ugui = textMesh.GetComponent<TextMeshProUGUI>();
        ugui.color = color;
        ugui.SetText(message);
    }

    public void AddClickAction(ButtonAction buttonClick) => this.buttonClick += buttonClick;
    public void RemoveClickAction(ButtonAction buttonClick) => this.buttonClick -= buttonClick;
    public void ClearClickAction() => this.buttonClick = null;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click");
        buttonClick?.Invoke();
    }
/*
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log($"OnDrop! {name}, {eventData.selectedObject.name}");
        if (eventData.selectedObject != null)
        {
            eventData.selectedObject.transform.SetParent(transform);
            eventData.selectedObject.transform.localPosition = Vector3.zero;
        }
    }
*/
}
