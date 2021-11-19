using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Popup.Defines;
using Popup.Items;



public class Entrance : MonoBehaviour
{
    public Button toLobby;
    public Button toGame;
    private Manager manager;
    public CustomButton gpgs;
    //private ObjectPool objectPool;



    void Start()
    {
        Initialize();

        Debug.Log("enter Entrance");
        TEST_SET();
    }



    private void Initialize()
    {
        //objectPool = (ObjectPool)FindObjectOfType(typeof(ObjectPool));
        manager = (Manager)FindObjectOfType(typeof(Manager));
        manager = manager ?? Instantiate(Resources.Load<Manager>("Prefabs/Global/Manager"));
        manager.eventSystem.enabled = true;
    }



    private void TEST_SET()
    {

        toLobby.onClick.AddListener(() =>
        {
            manager.eventSystem.enabled = false;
            manager.sceneController.Load(SceneType.lobby);
        });

        toGame.onClick.AddListener(() =>
        {
            manager.eventSystem.enabled = false;
            manager.sceneController.Load(SceneType.game);
        });

        gpgs.SetText("GPGS", Color.red);
        gpgs.AddActionDown(()=> Debug.Log("(0): Hello Added action DOWN is... mmm... !!!"));
        gpgs.AddActionDown(()=> Debug.Log("(1): Hello Added action DOWN is... mmm... !!!"));
        gpgs.AddActionUp(()=> Debug.Log("(0): Hello Added action UP is... mmm... !!!"));
        gpgs.AddActionUp(()=> Debug.Log("(1): Hello Added action UP is... mmm... !!!"));


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
