using UnityEngine;
using UnityEngine.EventSystems;
using System;

using Popup.Delegate;
using Popup.Defines;





public class PCustomButton : MonoBehaviour, IPointerClickHandler
{
    private PTextMesh textMesh = null;

    public void SetText(string message, Color color = default) {
        textMesh = textMesh ?? ObjectPool.Instance.Get<PTextMesh>(Prefab.TextMesh, transform, Vector3.zero, Vector2.one * 20);
        textMesh.ugui.color = color;
        textMesh.ugui.text = message;
    }

    /********************************/
    /* Implement interface          */
    /********************************/

    public void OnPointerClick(PointerEventData eventData) => _clickHandler?.Invoke(this, EventArgs.Empty);

    /********************************/
    /* Events                       */
    /********************************/

    event EventHandler _clickHandler;
    public ActionEvent<EventArgs> AddClickEventListener { set { _clickHandler += new EventHandler(value); } }

}
