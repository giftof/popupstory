using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Popup.Configs;



public class GUIGuide : MonoBehaviour
{
    public Canvas canvas;

    public void InitializeCanvas() => canvas = GameObject.Find(OName.canvas).GetComponent<Canvas>();

}
