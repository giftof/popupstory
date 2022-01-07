using System;
using Popup.Items;


namespace Popup.Delegate
{
    public delegate T[] NET_REQ<T>(params int[] id) where T : m_item;

    public delegate void PopupAction<T>(T arg) where T: class;
}
