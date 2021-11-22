using Popup.Configs;
using Popup.Defines;
using UnityEngine;



public class GUIGuide : MonoBehaviour
{
    private Canvas canvas;
    private Vector2 size;

    public void InitializeCanvas()
    {
        canvas = GameObject.Find(OName.canvas).GetComponent<Canvas>();
        size = canvas.GetComponent<RectTransform>().sizeDelta * 0.5f;
    }

    public Vector2 Position(GameObject rect, GUIPosition type, GameObject anchor = null)
    {
        anchor = anchor ?? canvas.gameObject;

        RectTransform rectTransform = rect.GetComponent<RectTransform>();
        Vector2 rectSize = rectTransform.sizeDelta * 0.5f * rectTransform.localScale;

        RectTransform anchorRect = anchor.GetComponent<RectTransform>();
        Vector2 anchorSize = anchorRect.sizeDelta * 0.5f;

        switch (type)
        {
            case GUIPosition.LeftTop:
                return Vector2.left * (anchorSize.x - rectSize.x) + Vector2.up * (anchorSize.y - rectSize.y);
            case GUIPosition.LeftMid:
                return Vector2.left * (anchorSize.x - rectSize.x);
            case GUIPosition.LeftBottom:
                return rectSize - anchorSize;
            case GUIPosition.MidTop:
                return Vector2.up * (anchorSize.y - rectSize.y);
            case GUIPosition.MidBottom:
                return Vector2.down * (anchorSize.y - rectSize.y);
            case GUIPosition.RightTop:
                return anchorSize - rectSize;
            case GUIPosition.RightMid:
                return Vector2.right * (anchorSize.x - rectSize.x);
            case GUIPosition.RightBottom:
                return Vector2.right * (anchorSize.x - rectSize.x) + Vector2.down * (anchorSize.y - rectSize.y);
            default:
                return Vector2.zero;
        }
    }
/*
    public Vector2 PositionBy(GameObject rect, Anchor type, GameObject anchor)
    {
        RectTransform targetRect = rect.GetComponent<RectTransform>();
        Vector2 targetSize = targetRect.sizeDelta * 0.5f * targetRect.localScale;

        RectTransform anchorRect = anchor.GetComponent<RectTransform>();
        Vector2 anchorSize = anchorRect.sizeDelta * 0.5f;

        switch (type)
        {
            case Anchor.BottomCenter:
                return anchor.transform.localPosition
            default:
                return Vector2.zero;
        }
    }
*/
}
