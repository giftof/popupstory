using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Popup.Delegate;
using TMPro;





public class CustomButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private TextMeshProUGUI meshProUGUI      = null;
    private ButtonAction    buttonActionDown = null;
    private ButtonAction    buttonActionUp   = null;
    private Image           image;


    public void SetText(string message, Color color = default)
    {
        Debug.Log("     [enter SetText]");

        if (meshProUGUI == null)
        {
            meshProUGUI = Instantiate(Resources.Load<TextMeshProUGUI>("Prefabs/TextMeshUGUI"));
            meshProUGUI.transform.SetParent(transform);
            meshProUGUI.transform.localPosition = Vector3.zero;
        }
        meshProUGUI.color = color;
        meshProUGUI.SetText(message);
    }

    public void AddActionDown   (ButtonAction buttonAction) => buttonActionDown += buttonAction;
    public void AddActionUp     (ButtonAction buttonAction) => buttonActionUp   += buttonAction;
    public void RemoveActionDown(ButtonAction buttonAction) => buttonActionDown -= buttonAction;
    public void RemoveActionUp  (ButtonAction buttonAction) => buttonActionUp   -= buttonAction;


    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
        buttonActionDown?.Invoke();
        if (image == null)
        {
            image = GetComponent<Image>();
        }
        image.color = Color.black;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("OnPointerUp");
        buttonActionUp?.Invoke();
        if (image == null)
        {
            image = GetComponent<Image>();
        }
        image.color = Color.white;
    }
}
