using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Popup.Delegate;
using Popup.Defines;
using Popup.Library;
using TMPro;





public class CustomButtonPrefab : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDropHandler
{
    private GameObject textMesh = null;
    private ButtonAction buttonActionDown = null;
    private ButtonAction buttonActionUp = null;
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

    public void AddActionDown(ButtonAction buttonAction) => buttonActionDown += buttonAction;
    public void AddActionUp(ButtonAction buttonAction) => buttonActionUp += buttonAction;
    public void RemoveActionDown(ButtonAction buttonAction) => buttonActionDown -= buttonAction;
    public void RemoveActionUp(ButtonAction buttonAction) => buttonActionUp -= buttonAction;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
        buttonActionDown?.Invoke();

        TEST_DOWN();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("OnPointerUp");
        buttonActionUp?.Invoke();

        TEST_UP();
    }



    public void TEST_DOWN() => image.sprite = sprites[0];  // test
    public void TEST_UP() => image.sprite = sprites[0]; // test

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log($"OnDrop! {name}, {eventData.selectedObject.name}");
        if (eventData.selectedObject != null)
        {
            eventData.selectedObject.transform.SetParent(transform);
            eventData.selectedObject.transform.localPosition = Vector3.zero;
        }
    }
}
