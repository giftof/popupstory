using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Popup.Defines;
using Popup.Configs;
using Popup.Library;

using UnityEngine.EventSystems;

using Popup.Items;
using Popup.Squad;
using Popup.Charactors;
using Popup.Items;
using Popup.Inventory;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


using Popup.Framework;


//public class CustomEvent : EventArgs {
//    public int intValue;
//}

//public class TestEvent {
//    public event EventHandler<CustomEvent> Click;

//    public void MouseButtonDown() {
//        CustomEvent customEvent = new CustomEvent { intValue = 42 };
//        Click?.Invoke(this, customEvent);
//    }
//}

public static class TE
{
    public static UnityEngine.Object FindObjectFromInstanceID(int iid)
    {
        return (UnityEngine.Object)typeof(UnityEngine.Object)
                .GetMethod("FindObjectFromInstanceID", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
                .Invoke(null, new object[] { iid });

    }
}

public class Entrance : MonoBehaviour {
    public Button toLobby;
    public Button toGame;
    [SerializeField]
    private Canvas canvas;
    private PCustomButton gpgs = null;


    //public ProgressBar progressBar;


    //void ButtonClick(object sender, CustomEvent e)
    //{
    //    Debug.Log("begin button click");
    //    Debug.Log(e.intValue);
    //    Debug.Log("click");
    //    Debug.Log("end button click");
    //}

    //TestEvent testEvent = new TestEvent();
    //testEvent.Click += new EventHandler<CustomEvent>(ButtonClick);
    //testEvent.MouseButtonDown();

    void Start() {

        //Debug.LogWarning(">> begin key test");
        //PItemBase itemBase1 = ObjectPool.Instance.Get(Prefab.SolidItem, transform).GetComponent<PSolidItem>();
        //PItemBase itemBase2 = ObjectPool.Instance.Get(Prefab.SolidItem, transform).GetComponent<PSolidItem>();
        //Debug.LogWarning($">> key = {c_item_slot.Instance.FindKeyWithItemBase(itemBase1)}");
        //Debug.LogWarning(TE.FindObjectFromInstanceID(0));
        ///*seperator*/
        //PItemSlot itemSlot1 = c_item_slot.Instance.MakeSlot(transform, itemBase1);
        //PItemSlot itemSlot2 = c_item_slot.Instance.MakeSlot(transform, itemBase2);
        //Debug.LogWarning($">> key = {c_item_slot.Instance.FindKeyWithItemBase(itemBase1)}");
        //Debug.LogError($">> KEY = {itemSlot1.GetInstanceID()}");
        //Debug.LogWarning(TE.FindObjectFromInstanceID(itemSlot1.GetInstanceID()));


        //Debug.LogWarning(">>>> before swap");
        //Debug.Log($"{c_item_slot.Instance.dictionary[itemSlot1.GetInstanceID()].itemSlot.GetInstanceID()}, {c_item_slot.Instance.dictionary[itemSlot1.GetInstanceID()].itemBase.GetInstanceID()}");
        //Debug.Log($"{c_item_slot.Instance.dictionary[itemSlot2.GetInstanceID()].itemSlot.GetInstanceID()}, {c_item_slot.Instance.dictionary[itemSlot2.GetInstanceID()].itemBase.GetInstanceID()}");
        //c_item_slot.Instance.Swap(itemSlot1.GetInstanceID(), itemSlot2.GetInstanceID());
        //Debug.LogWarning(">>>> after swap");
        //Debug.Log($"{c_item_slot.Instance.dictionary[itemSlot1.GetInstanceID()].itemSlot.GetInstanceID()}, {c_item_slot.Instance.dictionary[itemSlot1.GetInstanceID()].itemBase.GetInstanceID()}");
        //Debug.Log($"{c_item_slot.Instance.dictionary[itemSlot2.GetInstanceID()].itemSlot.GetInstanceID()}, {c_item_slot.Instance.dictionary[itemSlot2.GetInstanceID()].itemBase.GetInstanceID()}");
        //Debug.LogWarning(">> end key test");



        //TestEvent testEvent = new TestEvent();
        //testEvent.Click += new EventHandler<CustomEvent>(ButtonClick);
        //testEvent.MouseButtonDown();

        // var clickStream = this.UpdateAsObservable().Where(_ => Input.GetMouseButtonDown(0));
        // clickStream.Buffer(clickStream.Throttle(TimeSpan.FromSeconds(Config.doubleClickInterval))).Where(x => x.Count >= 2).Subscribe(x => Debug.Log($"x = {x.ToString()}, this is Double Click"));

        Initialize();
        
        TEST_BTN();
        TEST_SET();
        //TEST_PROGBAR();

        // Item item1 = new SolidItem();
        // Item item2 = new SolidItem();
        // Inventory inventory = new WareHouse(10);

        // Debug.Log("add new");
        // inventory.AddNew(item1);
        // Debug.Log("use 1");
        // inventory.Use(item1.Uid);
        // Debug.Log("use 2");
        // inventory.Use(item2.Uid);

        //StartCoroutine(TESTITEM());
    }

    IEnumerator TESTITEM() {
        yield return new WaitForSecondsRealtime(1);

        Item[] itemArray = Manager.Instance.network.REQ_NEW_ITEM_BY_ITEMID(0, 1, 2, 3);

        foreach (Item element in itemArray) {
            if (element.HaveAttribute(ItemCat.stackable))
                Debug.Log($"name = {element.Name}, amt = {(element as StackableItem).Amount}");
            else
                Debug.Log($"name = {element.Name}, dur = {(element as SolidItem).Durability}");
        }

        //string[] list = Manager.Instance.server.RequestNewItem(1);

        //JObject hello = JObject.Parse(list[0]);
        //Debug.Log(hello);
        //Debug.Log(hello["type"]);
        //Debug.Log(hello["contents"]);

        //SolidItem item = hello["contents"].ToObject<SolidItem>();
        //Debug.Log(item.Name);

        //SolidItem solidItem;
        //object obj3 = JsonConvert.DeserializeObject<object>(list[0]);
        //Debug.Log(obj3);
        //TEST_OBJ2 obj4 = (TEST_OBJ2)obj3;
        //Debug.Log(obj4.nameId);
        //Debug.Log(obj4.contents.Name);
        //Debug.Log(obj3.GetType());

        //Debug.Log($"obj.contents = {obj.contents}");

        //Item item8 = JsonConvert.DeserializeObject<SolidItem>(itemDef8);

    }
    //TestEvent testEvent = new TestEvent();




    private void Initialize()
    {
        _ = FindObjectOfType(typeof(Manager)) ?? Instantiate(Resources.Load<Manager>(Path.manager));
        Manager.Instance.Initialize();
    }



    private void TEST_PROGBAR()
    {
        //progressBar.BeginSize = new Vector2(500, 100);
    }


    private void TEST_BTN()
    {
        toLobby.onClick.AddListener(() =>
        {
            Manager.Instance.eventSystem.enabled = false;
            Manager.Instance.sceneController.Load(SceneType.lobby);
        });

        toGame.onClick.AddListener(() =>
        {
            Manager.Instance.eventSystem.enabled = false;
            Manager.Instance.sceneController.Load(SceneType.game);
        });
    }

    private void TEST_SET()
    {
        Squad squad = new Squad(Manager.Instance.network.REQ_NEW_ID());
        Charactor c1 = new Charactor();
        c1.SetUID = Manager.Instance.network.REQ_NEW_ID();
        c1.SetName = "c1";
        c1.Size = 1;
        Charactor c2 = new Charactor();
        c2.SetUID = Manager.Instance.network.REQ_NEW_ID();
        c2.SetName = "c2";
        c2.Size = 1;
        Charactor c3 = new Charactor();
        c3.SetUID = Manager.Instance.network.REQ_NEW_ID();
        c3.SetName = "c3";
        c3.Size = 2;
        squad.AddLast(c1);
        squad.AddLast(c2);
        squad.AddLast(c3);

        squad.DEBUG_TEST();
        squad.ShiftBackward(c1, 2);
        squad.DEBUG_TEST();




        gpgs = gpgs ?? ObjectPool.Instance.Get<PCustomButton>(Prefab.CustomButton, transform);
        gpgs.gameObject.PositionOnParent(GUIPosition.RightBottom, Vector2.one * 100);
        gpgs.SetText("GPGS", Color.red);
        gpgs.AddClickEventListener = (object sender, EventArgs e) => Debug.Log("(0): Hello Click action mmm... !!!");

        List<int> list = new List<int>();
        list.FirstOrDefault(e => 0 < e);

        Debug.Log(list.Select((e, i) => (e, i)).FirstOrDefault(p=> 3 < p.e && 0 < ++p.i));
        
        list.Add(2);
        list.Add(4);
        list.Add(8);
        
        Debug.Log(list.Select((e, i) => (e, i)).FirstOrDefault(p=> 7 < p.e && 0 < ++p.i));
        Debug.Log(list.Select((e, i) => (e, i)).FirstOrDefault(p=> 3 < p.e && 0 < ++p.i));
    }
}
