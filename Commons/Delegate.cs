using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;



namespace Popup.Delegate
{
    public delegate bool DCompare<T>(T value);
    public delegate T ConvertFromString<T>(IDataReader data);

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
