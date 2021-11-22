using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIGuide : MonoBehaviour
{
    Canvas canvas;

    public void InitializeCanvas() => canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

}
