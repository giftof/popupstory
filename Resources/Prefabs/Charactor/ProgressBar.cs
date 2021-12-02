using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;





public class ProgressBar : MonoBehaviour
{
    [SerializeField]
    Image bottom;
    [SerializeField]
    RectTransform bottomRect;
    [SerializeField]
    Image cover;
    [SerializeField]
    RectTransform coverRect;
    [SerializeField]
    Image bar;
    [SerializeField]
    RectTransform barRect;



    public Vector2 Size
    {
        get { return bottomRect.sizeDelta; }
        set {
            bottomRect.sizeDelta = value;
            coverRect.sizeDelta = value;
            barRect.sizeDelta = value;
        }
    }

    public void Rate(float rate)
    {
        if (cover.fillAmount < rate)
        {

        }
        else
        {

        }
    }

    IEnumerator AnimateBar(Image animate, Image instant, float to, float duration)
    {
        float beginTime = Time.time;
        float beginRate = animate.fillAmount;

        instant.fillAmount = to;

        while (true)
        {
            yield return null;
            animate.fillAmount = to;
        }
    }
}
