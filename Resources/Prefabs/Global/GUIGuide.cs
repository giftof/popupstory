using Popup.Configs;
using Popup.Defines;
using UnityEngine;



public class GUIGuide : MonoBehaviour
{
    public Canvas canvas = null;
    public Canvas pickCanvas = null;
    //private Vector2 size;
    private GameObject anchor = null;
    private RectTransform targetRect = null;
    private RectTransform parentRect = null;
    private Vector2 targetSize = default;
    private Vector2 parentSize = default;

    public void InitializeCanvas()
    {
        canvas = GameObject.Find(OName.canvas).GetComponent<Canvas>();
        pickCanvas = GameObject.Find(OName.pickCanvas)?.GetComponent<Canvas>();
        //size = canvas.GetComponent<RectTransform>().sizeDelta * 0.5f;
    }

    private void Clear()
    {
        anchor = null;
        targetRect = null;
        parentRect = null;
    }

    private void SetProperties(GameObject rect, GameObject anchor = null)
    {
        if (this.anchor == null)
        {
            this.anchor = anchor ?? canvas.gameObject;
        }

        if (targetRect == null)
        {
            targetRect = rect.GetComponent<RectTransform>();
            targetSize = targetRect.sizeDelta * 0.5f * targetRect.localScale;
        }

        if (parentRect == null)
        {
            parentRect = this.anchor.GetComponent<RectTransform>();
            parentSize = parentRect.sizeDelta * 0.5f;
        }
    }

    private void SetParent(Transform child, Transform parent)
    {
        if (!child.parent.Equals(parent))
            child.SetParent(parent);
    }

    private Vector2 SetPosition(GUIPosition type)
    {
        Vector2 position;

        switch (type)
        {
            case GUIPosition.LeftTop:
                position = Vector2.left * (parentSize.x - targetSize.x) + Vector2.up * (parentSize.y - targetSize.y);
                break;
            case GUIPosition.LeftMid:
                position = Vector2.left * (parentSize.x - targetSize.x);
                break;
            case GUIPosition.LeftBottom:
                position = targetSize - parentSize;
                break;
            case GUIPosition.MidTop:
                position = Vector2.up * (parentSize.y - targetSize.y);
                break;
            case GUIPosition.MidBottom:
                position = Vector2.down * (parentSize.y - targetSize.y);
                break;
            case GUIPosition.RightTop:
                position = parentSize - targetSize;
                break;
            case GUIPosition.RightMid:
                position = Vector2.right * (parentSize.x - targetSize.x);
                break;
            case GUIPosition.RightBottom:
                position = Vector2.right * (parentSize.x - targetSize.x) + Vector2.down * (parentSize.y - targetSize.y);
                break;
            default:
                position = Vector2.zero;
                break;
        }

        Clear();
        return position;
    }

    public Vector2 Position(GameObject rect, GUIPosition type, Vector2 rectSize, GameObject anchor = null)
    {
        SetProperties(rect, anchor);
        SetParent(rect.transform, this.anchor.transform);

        targetRect.localScale = Vector2.one;
        targetRect.sizeDelta = rectSize;

        return SetPosition(type);
    }

    public Vector2 Position(GameObject rect, GUIPosition type, GameObject anchor = null)
    {
        SetProperties(rect, anchor);
        SetParent(rect.transform, this.anchor.transform);

        return SetPosition(type);
    }

    //public Vector2 Position(GameObject rect, GUIPosition type, GameObject anchor = null)
    //{
    //    SetProperties(rect, anchor);
    //    anchor = anchor ?? canvas.gameObject;

    //    RectTransform rectTransform = rect.GetComponent<RectTransform>();
    //    Vector2 rectSize = rectTransform.sizeDelta * 0.5f * rectTransform.localScale;

    //    RectTransform anchorRect = anchor.GetComponent<RectTransform>();
    //    Vector2 anchorSize = anchorRect.sizeDelta * 0.5f;

    //    switch (type)
    //    {
    //        case GUIPosition.LeftTop:
    //            return Vector2.left * (anchorSize.x - rectSize.x) + Vector2.up * (anchorSize.y - rectSize.y);
    //        case GUIPosition.LeftMid:
    //            return Vector2.left * (anchorSize.x - rectSize.x);
    //        case GUIPosition.LeftBottom:
    //            return rectSize - anchorSize;
    //        case GUIPosition.MidTop:
    //            return Vector2.up * (anchorSize.y - rectSize.y);
    //        case GUIPosition.MidBottom:
    //            return Vector2.down * (anchorSize.y - rectSize.y);
    //        case GUIPosition.RightTop:
    //            return anchorSize - rectSize;
    //        case GUIPosition.RightMid:
    //            return Vector2.right * (anchorSize.x - rectSize.x);
    //        case GUIPosition.RightBottom:
    //            return Vector2.right * (anchorSize.x - rectSize.x) + Vector2.down * (anchorSize.y - rectSize.y);
    //        default:
    //            return Vector2.zero;
    //    }
    //}
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
