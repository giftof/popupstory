using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Popup.Defines;
using Popup.Configs;
using Popup.Library;



using Popup.Items;
using Popup.Squad;
using Popup.Charactors;



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
        TEST_BTN();
        TEST_SET();
    }



    private void Initialize()
    {
        _ = FindObjectOfType(typeof(Manager)) ?? Instantiate(Resources.Load<Manager>(Path.manager));
        Manager.Instance.Initialize();
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
        Squad squad = new Squad(Manager.Instance.network.RequestNewUID());
        Charactor c1 = new Charactor();
        c1.uid = Manager.Instance.network.RequestNewUID();
        c1.Name = "c1";
        c1.Size = 1;
        Charactor c2 = new Charactor();
        c2.uid = Manager.Instance.network.RequestNewUID();
        c2.Name = "c2";
        c2.Size = 1;
        Charactor c3 = new Charactor();
        c3.uid = Manager.Instance.network.RequestNewUID();
        c3.Name = "c3";
        c3.Size = 2;
        squad.AddLast(c1);
        squad.AddLast(c2);
        squad.AddLast(c3);

        squad.DEBUG_TEST();
        squad.ShiftBackward(c1, 2);
        squad.DEBUG_TEST();




        gpgs = gpgs ?? (GameObject)ObjectPool.Instance.Request(Prefab.CustomButton);
        gpgs.SetActive(true);
        gpgs.name = "GPGS";
Debug.Log(gpgs.GetInstanceID());
        CustomButtonPrefab gpgsButton = gpgs.GetComponent<CustomButtonPrefab>();
        //gpgsButton.transform.SetParent(canvas.transform);
        /*gpgsButton.transform.localScale = Vector2.one;*/
        //gpgsButton.transform.GetComponent<RectTransform>().sizeDelta = Vector2.one * 20;
        gpgs.PositionOnParent(GUIPosition.RightBottom, Vector2.one * 100);
        //gpgsButton.transform.localPosition = Manager.Instance.guiGuide.Position(gpgs, GUIPosition.RightBottom);
        gpgsButton.SetText("GPGS", Color.red);

        gpgsButton.AddClickAction(() => Debug.Log("(0): Hello Click action mmm... !!!"));
/*
        gpgsButton.AddActionDown(() => Debug.Log("(0): Hello Added action DOWN is... mmm... !!!"));
        gpgsButton.AddActionDown(() => Debug.Log("(1): Hello Added action DOWN is... mmm... !!!"));
        gpgsButton.AddActionUp(() => Debug.Log("(0): Hello Added action UP is... mmm... !!!"));
        gpgsButton.AddActionUp(() => Debug.Log("(1): Hello Added action UP is... mmm... !!!"));
*/

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
