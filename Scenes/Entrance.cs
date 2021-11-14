using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Popup.Defines;



public class Entrance : MonoBehaviour
{
    public  Button  toLobby;
    public  Button  toGame;
    private Manager manager;



    void Awake()
    {
        Initialize();

        Debug.Log("enter Entrance");
        toLobby.onClick.AddListener(() => manager.sceneController.Load(SceneType.lobby));
        toGame.onClick.AddListener(() => manager.sceneController.Load(SceneType.game));
    }



    private void Initialize()
    {
        manager = (Manager)FindObjectOfType(typeof(Manager));

        if (manager == null)
        {
            manager = Instantiate(Resources.Load<Manager>("Prefabs/Global/Manager"));
        }
    }
}
