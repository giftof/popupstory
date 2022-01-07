using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Popup.Items;
using Popup.Charactors;
using Popup.Defines;
using Popup.Delegate;
using UnityEngine.EventSystems;
using Newtonsoft.Json;

namespace Popup.Framework {

    public abstract class PopupObject {

        [JsonProperty]
        public int Uid { get; protected set; }
        [JsonProperty]
        public int NameId { get; protected set; }
        [JsonProperty]
        public int SlotId { get; protected set; }
        [JsonProperty]
        public GameObject Owner { get; protected set; }
        [JsonProperty]
        public string Name { get; protected set; }
        [JsonProperty]
        public Grade Grade { get; protected set; }

        [JsonIgnore]
        public abstract bool IsExist { get; }
        public abstract object DeepCopy(int uid);

        public int SetSlotId { set { SlotId = value; } }
        public int SetUID { set { Uid = value; } }
        public int SetNameId { set { NameId = value; } }
        public string SetName { set { Name = value; } }
        public GameObject SetOwner { set { Owner = value; } }
    }

    public interface IItemHandler : IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler { }


    //public interface IPopupEventDetector<T1, T2>
    //{
    //    PopupCallBack<T1, T2> AddListener();
    //    //Dictionary<string, PopupCallBack<T1, T2>> PopupCallbackDictionary { get; }
    //}



    //public interface IPopupEventArgs<T> where T : class
    //{
    //    Dictionary<string, T> PopupEventArgsDictionary { get; }
    //    void AddPopupEventArgs(string keyName, T valueData);
    //    T GetArgs(string keyName);
    //}

    //public interface IPopupEventDetector<T> where T: class
    //{
    //    Dictionary<string, EventHandler<T>> PopupEventDetectorDictionary { get; }
    //    event EventHandler<T> PopupEventDetector;
    //}

    //public interface IPopupEventReceiver<T> where T: class
    //{
    //    //void AddPopupListener(object sender, T e);
    //    abstract void PopupEventReceiver(object sender, T e);
    //}

    //public interface IPopupClickEvent<T1, T2>
    //{
    //    Dictionary<string, PopupCallBack<T1, T2>> PopupCallBackDictionary { get; }
    //    void AddListener(T1 funcName, T2 arg);
    //    //PopipCallBack<T1, T2> 
    //}

    //public interface IPopupClick

    //public interface IPopupObject
    //{
    //    int Uid { get; }
    //    int NameId { get; }
    //    int SlotId { get; }
    //    bool IsExist { get; }

    //    object DeepCopy(int? _ = null, int? __ = null);
    //}


    //public interface IOwner
    //{
    //    GameObject Owner { get; set; }
    //}

    //public interface IItem : IPopupObject, IOwner
    //{
    //    int UseableCount { get; }
    //    bool HaveSpace(string name = null);
    //    //bool HasSpace { get; }

    //    // object  DuplicateEmpty();
    //    // object  DuplicateEmptyNew();
    //}



    //public interface IPopupObserved {
    //    void Notify();
    //    void AddDelegate(Action action);
    //    void RemoveDelegate(Action action);
    //}

    //public interface IPopupObserver {
    //    void Dispose();
    //}

    //public interface IToolItemHandler : IITemHandler
    //{
    //    /*bool AddStack(ToolItem item);*/
    //}


    //public interface IEquipItemHandler : IITemHandler
    //{
    //}

    //public interface ISpell : IPopupObject, IOwner
    //{

    //}

    //public interface IInventory
    //{
    //    // Item    Pick    (int uid);
    //    // bool    Use     (int uid);
    //    // bool    Use     (Item item);
    //    // bool    Pop     (int uid);
    //    // bool    Add     (Item item);
    //    // void    SetMaxSize  (int size);
    //}


    //public interface ICharactor : IPopupObject, IOwner
    //{
    //    int Size { get; }
    //    bool IsAlive { get; }
    //    bool IsCorpse { get; }
    //    bool IsOccupied { get; }
    //    //Charactor PickCharactor   (int uid);

    //    //bool            PopCharactor    (int uid);
    //    //bool            PopCharactor    (Charactor charactor);
    //    //bool            AddCharactor    (int uid);
    //    //bool            AddCharactor    (Charactor charactor);
    //}
}
