using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Popup.Items;
using Popup.Charactors;
using Popup.Defines;



namespace Popup.Framework
{
    // public interface IConverterToModel<T>
    // {
    //     T DataConvert(IDataReader data);
    // }



    public interface IPopupObject
    {
        int uid { get; }
        int slotId { get; }
        bool IsExist { get; }

        object  Duplicate();
        object  DuplicateNew();
    }



    public interface IItem : IPopupObject
    {
        int UseableCount { get; }
        bool HasSpace { get; }

        object  DuplicateEmpty();
        object  DuplicateEmptyNew();
    }



    public interface IInventory
	{
        // Item    Pick    (int uid);
        // bool    Use     (int uid);
        // bool    Use     (Item item);
        // bool    Pop     (int uid);
        // bool    Add     (Item item);
        // void    SetMaxSize  (int size);
    }



    public interface ICharactor
    {
        Charactor       PickCharactor   (int uid);
        bool            PopCharactor    (int uid);
        bool            PopCharactor    (Charactor charactor);
        bool            AddCharactor    (int uid);
        bool            AddCharactor    (Charactor charactor);
    }
}
