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
    void Awake()
    {
        manager = (Manager)FindObjectOfType(typeof(Manager));
        Debug.Log("enter Lobby");
        toEntrance.onClick.AddListener(() => manager.sceneController.Load(SceneType.entrance));
        toGame.onClick.AddListener(() => manager.sceneController.Load(SceneType.game));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
