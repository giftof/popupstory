using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Popup.Configs;
using Popup.Library;
using DG.Tweening;





public partial class ProgressBar : MonoBehaviour
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



    public Vector2 BeginSize
    {
        get { return bottomRect.sizeDelta; }
        set
        {
            bottomRect.sizeDelta = value;
            coverRect.sizeDelta = value;
            barRect.sizeDelta = value;
        }
    }

    public void Rate(float rate, float duration = 1f)
    {
        Image animate = cover.fillAmount < rate ? cover : bar;
        Image instant = animate == bar ? cover : bar;

        animate.DOKill();
        instant.DOKill();
        
        instant.fillAmount = rate;

        //animate.DOFillAmount(rate, duration).SetId(GetInstanceID()).SetSpeedBased();
        animate.DOFillAmount(rate, Libs.TimeDuration(duration)).SetId(GetInstanceID()).SetEase(Ease.OutCubic);
    }

    private void OnMouseDown()
    {
        Debug.Log("mouse down");
    }
}

public partial class ProgressBar    // TEST
{
    public PCustomButton button;

    private void Start()
    {
        button.AddClickEventListener = (object sender, EventArgs e) =>
        {
            float random = UnityEngine.Random.Range(0f, 1f);
            Debug.Log($"current bar = {bar.fillAmount}, current cover = {cover.fillAmount}, random = {random}");
            Rate(random);
        };
    }
}