using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Popup.Defines;
using Popup.Configs;



public class Lobby : MonoBehaviour
{
    public Button toEntrance;
    public Button toGame;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();

        Debug.Log("enter Lobby");
        toEntrance.onClick.AddListener(() =>
        {
            Manager.Instance.eventSystem.enabled = false;
            Manager.Instance.sceneController.Load(SceneType.entrance);
        });
        toGame.onClick.AddListener(() =>
        {
            Manager.Instance.eventSystem.enabled = false;
            Manager.Instance.sceneController.Load(SceneType.game);
        });
    }



    private void Initialize()
    {
        _ = FindObjectOfType(typeof(Manager)) ?? Instantiate(Resources.Load<Manager>(Path.manager));
        Manager.Instance.eventSystem.enabled = true;
        Manager.Instance.guiGuide.InitializeCanvas();
    }
}
