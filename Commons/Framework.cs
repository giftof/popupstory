using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Popup.Items;
using Popup.Charactors;


namespace Popup.Framework
{
    public interface IConverterToModel<T>
    {
        T DataConvert(IDataReader data);
    }



    public interface IPopupObject
    {
        int GetUID();
    }



	public interface IInventory
	{
        //ModelBase PickItem(int uid);
        Item    PickItem    (int uid);
        bool    UseItem     (int uid);
        bool    UseItem     (ref Item item);
        bool    PopItem     (int uid);
        bool    AddItem     (ref Item item);
        void    SetMaxSize  (int size);
    }



    public interface ICharactor
    {
        ref Charactor   PickCharactor   (int uid);
        bool            PopCharactor    (int uid);
        bool            PopCharactor    (ref Charactor charactor);
        bool            AddCharactor    (int uid);
        bool            AddCharactor    (ref Charactor charactor);
    }

}
