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
    public delegate T[] NET_REQ<T>(params int[] id) where T : Item;


    public delegate void ActionWithInt(int value);
}
