using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;





public class Manager : MonoBehaviour
{
    public Auth             auth;
    public EventSystem      eventSystem;
    public SceneController  sceneController;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        //DontDestroyOnLoad(eventSystem);
        //DontDestroyOnLoad(auth);
        //DontDestroyOnLoad(sceneController);
    }
}
