using System.Data;
using Popup.Items;


namespace Popup.Delegate
{
    public delegate T[] NET_REQ<T>(params int[] id) where T : Item;

    public delegate void ActionWith<T>(T arg) where T: class;
    public delegate void PopupEvent<T>(object sender, T e) where T : class;
}
