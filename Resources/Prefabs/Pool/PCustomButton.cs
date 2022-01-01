using UnityEngine;
using UnityEngine.EventSystems;
using Popup.Delegate;
using Popup.Defines;
using TMPro;



public class PCustomButton : MonoBehaviour, IPointerClickHandler
{
    private GameObject textMesh = null;
    private ButtonAction buttonClick = null;



    public void AddClickAction(ButtonAction buttonClick) => this.buttonClick += buttonClick;
    public void RemoveClickAction(ButtonAction buttonClick) => this.buttonClick -= buttonClick;
    public void ClearClickAction() => buttonClick = null;
    public void OnPointerClick(PointerEventData eventData) => buttonClick?.Invoke();

    public void SetText(string message, Color color = default) {
        TextMeshProUGUI ugui;

        textMesh = textMesh ?? ObjectPool.Instance.Get(Prefab.TextMesh, transform, Vector3.zero, Vector2.one * 20);
        ugui = textMesh.GetComponent<TextMeshProUGUI>();
        ugui.color = color;
        ugui.text = message;
    }
}
