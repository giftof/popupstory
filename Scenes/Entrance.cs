using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Popup.Defines;
using Popup.Items;



public class Entrance : MonoBehaviour
{
    public  Button       toLobby;
    public  Button       toGame;
    private Manager      manager;
    public  CustomButton gpgs;



    void Awake()
    {
        Initialize();

        Debug.Log("enter Entrance");
        TEST_SET();
    }



    private void Initialize()
    {
        manager = (Manager)FindObjectOfType(typeof(Manager));

        if (manager == null)
        {
            manager = Instantiate(Resources.Load<Manager>("Prefabs/Global/Manager"));
        }

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

        gpgs.SetText("GPGS");
        gpgs.AddActionDown(()=> Debug.Log("(0): Hello Added action DOWN is... mmm... !!!"));
        gpgs.AddActionDown(()=> Debug.Log("(1): Hello Added action DOWN is... mmm... !!!"));
        gpgs.AddActionUp(()=> Debug.Log("(0): Hello Added action UP is... mmm... !!!"));
        gpgs.AddActionUp(()=> Debug.Log("(1): Hello Added action UP is... mmm... !!!"));


    }
}
