using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Popup.Items;
using Popup.Charactors;
using Popup.Defines;



namespace Popup.Framework
{
    public interface IConverterToModel<T>
    {
        T DataConvert(IDataReader data);
    }



    public interface IPopupObject
    {
        int     GetUID();

        object  Duplicate();
        object  DuplicateNew();
    }



    public interface IItem : IPopupObject
    {
        string 	GetName();
        float 	GetWeight();
        float 	GetVolume();
        ItemCat GetCategory();
        int		GetLeftOver();
        bool	IsExist();
        bool 	Exhaust();
        bool	HasSpace();

        object  DuplicateEmpty();
        object  DuplicateEmptyNew();
    }



    public interface IInventory
	{
        //ModelBase PickItem(int uid);
        Item    PickItem    (int uid);
        bool    ExhaustItem (int uid);
        bool    ExhaustItem (ref Item item);
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
