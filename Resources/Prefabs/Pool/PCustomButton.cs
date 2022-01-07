using Popup.Defines;
using Popup.Delegate;
using System;
using UnityEngine;
using UnityEngine.EventSystems;





public class PCustomButton : MonoBehaviour, IPointerClickHandler
{
    private PTextMesh textMesh = null;

    public void SetText(string message, Color color = default) 
    {
        textMesh = textMesh ?? ObjectPool.Instance.Get<PTextMesh>(Prefab.TextMesh, transform, Vector3.zero, Vector2.one * 20);
        textMesh.ugui.color = color;
        textMesh.ugui.text = message;
    }

    /********************************/
    /* Interface                    */
    /********************************/

    public void OnPointerClick(PointerEventData eventData)
    {
        ClickEventDelegate?.Invoke(this, EventArgs.Empty);
    }

    /********************************/
    /* Events                       */
    /********************************/

    event EventHandler ClickEventDelegate;

    public EventHandler<EventArgs> AddClickEventListener
    { 
        set { ClickEventDelegate += new EventHandler(value); }
    }

}
