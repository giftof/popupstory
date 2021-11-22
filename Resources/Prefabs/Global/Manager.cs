using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;





public class Manager : MonoBehaviour
{
    public Auth auth;
    public EventSystem eventSystem;
    public SceneController sceneController;
    public GUIGuide uiGuide;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
