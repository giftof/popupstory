using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;





public class Manager : MonoBehaviour
{
    public Auth auth;
    public EventSystem eventSystem;
    public SceneController sceneController;
    public GUIGuide guiGuide;
    public Network network;
    public Server server;



    public static Manager Instance = null;

    private void Awake()
    {
        Debug.LogError("Awake Manager");
        if (Instance != null)
            Destroy(this);
        Instance = this;
        DontDestroyOnLoad(this);
    }

    public void Initialize()
    {
        guiGuide.InitializeCanvas();
        eventSystem.enabled = true;
    }
}
