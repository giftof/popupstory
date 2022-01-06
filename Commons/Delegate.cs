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
    public delegate T[] NET_REQ<T>(params int[] id) where T : Item;

    public delegate void ActionWithItemBase(PItemBase itemBase);
    public delegate void ActionWithItem(Item item);
    public delegate void ActionWithInt(int value);

    public delegate void ActionEvent<T>(object sender, T e) where T : class;
}
