using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Popup.Defines;
using Popup.Configs;



using Popup.Items;
using Popup.Squad;
using Popup.Charactors;
using Popup.ServerJob;


public class Entrance : MonoBehaviour
{
    public Button toLobby;
    public Button toGame;
    [SerializeField]
    private Canvas canvas;
    private GameObject gpgs = null;



    void Start()
    {
        Initialize();

        Debug.Log("enter Entrance");
        TEST_SET();
    }



    private void Initialize()
    {
        _ = FindObjectOfType(typeof(Manager)) ?? Instantiate(Resources.Load<Manager>(Path.manager));
        Manager.Instance.eventSystem.enabled = true;
        Manager.Instance.guiGuide.InitializeCanvas();
    }


    //LinkedList<int> testList1 = new LinkedList<int>();
    //LinkedList<int> testList2 = new LinkedList<int>();

    //float cur1;
    //float cur2;
    //float f1;
    //float f2;
    //int max = 4;
    //bool fl1 = false;
    //bool fl2 = false;


    //private void Update()
    //{
    //    if (testList1.Count == 0)
    //    {
    //        cur1 = Time.time;
    //    }

    //    if (testList1.Count < max)
    //    {
    //        testList1.AddFirst(1);
    //        //testList1.AddLast(1);
    //    }
    //    else
    //    {
    //        if (!fl1)
    //        {
    //            fl1 = true;
    //            Debug.Log($"elapse: {Time.time - cur1}");
    //        }
            
    //        if (testList2.Count == 0)
    //        {
    //            cur2 = Time.time;
    //        }

    //        if (testList2.Count < max)
    //        {
    //            testList2.AddLast(1);
    //            //testList2.AddFirst(1);
    //        }
    //        else
    //        {
    //            if (!fl2)
    //            {
    //                fl2 = true;
    //                Debug.Log($"elapse: {Time.time - cur2}");
    //            }
    //        }
    //    }
    //}

    private void TEST_SET()
    {
        Squad squad = new Squad(ServerJob.RequestNewUID);
        Charactor c1 = new Charactor();
        c1.uid = ServerJob.RequestNewUID;
        c1.Name = "c1";
        c1.Size = 1;
        Charactor c2 = new Charactor();
        c2.uid = ServerJob.RequestNewUID;
        c2.Name = "c2";
        c2.Size = 1;
        Charactor c3 = new Charactor();
        c3.uid = ServerJob.RequestNewUID;
        c3.Name = "c3";
        c3.Size = 2;
        squad.AddLast(c1);
        squad.AddLast(c2);
        squad.AddLast(c3);

        squad.DEBUG_TEST();
        squad.ShiftBackward(c1, 2);
        squad.DEBUG_TEST();


        //float cur = Time.time;
        //for (int i = 0; i < 1000; ++i)
        //{
        //    testList1.AddFirst(i);
        //}
        //Debug.Log($"elapse add first: {Time.time - cur}");

        //cur = Time.time;
        //for (int i = 0; i < 1000; ++i)
        //{
        //    testList2.AddLast(i);
        //}
        //Debug.Log($"elapse add last: {Time.time - cur}");


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

        gpgs = gpgs ?? (GameObject)ObjectPool.Instance.Request(Prefab.CustomButton);
        gpgs.SetActive(true);
        gpgs.name = "GPGS";
Debug.Log(gpgs.GetInstanceID());
        CustomButtonPrefab gpgsButton = gpgs.GetComponent<CustomButtonPrefab>();
        gpgsButton.transform.SetParent(canvas.transform);
        /*gpgsButton.transform.localScale = Vector2.one;*/
        gpgsButton.transform.GetComponent<RectTransform>().sizeDelta = Vector2.one * 20;
        gpgsButton.transform.localPosition = Manager.Instance.guiGuide.Position(gpgs, GUIPosition.RightBottom);
        gpgsButton.SetText("GPGS", Color.red);

        gpgsButton.AddActionDown(() => Debug.Log("(0): Hello Added action DOWN is... mmm... !!!"));
        gpgsButton.AddActionDown(() => Debug.Log("(1): Hello Added action DOWN is... mmm... !!!"));
        gpgsButton.AddActionUp(() => Debug.Log("(0): Hello Added action UP is... mmm... !!!"));
        gpgsButton.AddActionUp(() => Debug.Log("(1): Hello Added action UP is... mmm... !!!"));


        GameObject test = (GameObject)ObjectPool.Instance.Request(Prefab.CustomButton);
        test.SetActive(true);
        test.name = "TEST";
Debug.Log(test.GetInstanceID());
        CustomButtonPrefab testPrefab = test.GetComponent<CustomButtonPrefab>();
        /*testPrefab.transform.SetParent(canvas.transform);*/
        /*testPrefab.transform.localScale = Vector2.one;*/
        testPrefab.transform.GetComponent<RectTransform>().sizeDelta = Vector2.one * 5;
        testPrefab.transform.localPosition = Manager.Instance.guiGuide.Position(test, GUIPosition.LeftTop, gpgs);
        testPrefab.GetComponent<Image>().color = Color.blue;


        List<int> list = new List<int>();
        // int index;
        // int id;
        // Debug.Log(list.FirstOrDefault(e => 0 < e));

        list.FirstOrDefault(e => 0 < e);


        Debug.Log(list.Select((e, i) => (e, i)).FirstOrDefault(p=> 3 < p.e && 0 < ++p.i));
        // Debug.Log(list.Select(e => new KeyValuePair<int, int>(e, index = 0)).FirstOrDefault(p => 3 < p.Key));
        // Debug.Log(list.Select(e => new KeyValuePair<int, int>(e, index = 0)).FirstOrDefault(p => 3 < p.Key));
        // Debug.Log(list.Select((e, i) => (e, i)).FirstOrDefault(p=> 0 < p.e && 0 < ++p.i));
        
        list.Add(2);
        list.Add(4);
        list.Add(8);
        
        Debug.Log(list.Select((e, i) => (e, i)).FirstOrDefault(p=> 7 < p.e && 0 < ++p.i));
        Debug.Log(list.Select((e, i) => (e, i)).FirstOrDefault(p=> 3 < p.e && 0 < ++p.i));
        // Debug.Log(list.Select(e => new KeyValuePair<int, int>(e, index = 0)).FirstOrDefault(p => 3 < p.Key));

        // list.Select((v, i) => new {v, i}).FirstOrDefault(0 < v)?.i;

/*
        Debug.Log(list.FindIndex(c => 0 < c));
        id = 8;
        index = list[list.Count - 1] < id 
				? list.Count
				: list.FirstOrDefault(e => id < e);
        list.Insert(index, id);
        id = 4;
        index = list[list.Count - 1] < id 
				? list.Count
				: list.FirstOrDefault(e => id < e);
        list.Insert(index, id);
        id = 2;
        index = list[list.Count - 1] < id 
				? list.Count
				: list.FirstOrDefault(e => id < e);
        list.Insert(index, id);
        Debug.Log(list.FindIndex(c => 9 < c));
        Debug.Log(list.FindIndex(c => 0 < c));
        Debug.Log(list.FindIndex(c => 3 < c));
*/
    }
}
