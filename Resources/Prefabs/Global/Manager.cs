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

    public static Manager Instance = null;

    private void Awake()
    {
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
