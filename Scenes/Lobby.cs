using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Popup.Defines;



public class Lobby : MonoBehaviour
{
    public Button toEntrance;
    public Button toGame;
    private Manager manager;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();

        Debug.Log("enter Lobby");
        toEntrance.onClick.AddListener(() =>
        {
            manager.eventSystem.enabled = false;
            manager.sceneController.Load(SceneType.entrance);
        });
        toGame.onClick.AddListener(() =>
        {
            manager.eventSystem.enabled = false;
            manager.sceneController.Load(SceneType.game);
        });
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
}
