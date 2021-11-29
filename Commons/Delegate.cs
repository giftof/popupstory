using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using Popup.Items;


namespace Popup.Delegate
{
    public delegate bool DCompare<T>(T value);
    public delegate T ConvertFromString<T>(IDataReader data);

    public delegate void ButtonAction();
    public delegate void ItemAction(Item item);
    //public class Delegate : MonoBehaviour
    //{
    //    // Start is called before the first frame update
    //    void Start()
    //    {

    //    }

    //    // Update is called once per frame
    //    void Update()
    //    {

    //    }
    //}

}
