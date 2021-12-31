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


    //public ProgressBar progressBar;


    void Start()
    {
        Initialize();
        
        TEST_BTN();
        TEST_SET();
        //TEST_PROGBAR();
    }



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




        gpgs = gpgs ?? (GameObject)ObjectPool.Instance.Get(Prefab.CustomButton, transform);
        //gpgs.name = "GPGS";
        PCustomButton gpgsButton = gpgs.GetComponent<PCustomButton>();
        gpgs.PositionOnParent(GUIPosition.RightBottom, Vector2.one * 100);
        gpgsButton.SetText("GPGS", Color.red);

        gpgsButton.AddClickAction(() => Debug.Log("(0): Hello Click action mmm... !!!"));

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
