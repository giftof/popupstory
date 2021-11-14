using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Popup.Defines;


//public class SceneManagerEx
//{
//    public BaseScene CurrentScene { get { return GameObject.FindObjectOfType<BaseScene>(); } }

//    string GetSceneName(Define.Scene type)
//    {
//        string name = System.Enum.GetName(typeof(Define.Scene), type); // C#의 Reflection. Scene enum의 
//        return name;
//    }

//    public void LoadScene(Define.Scene type)
//    {
//        Managers.Clear();

//        SceneManager.LoadScene(GetSceneName(type)); // SceneManager는 UnityEngine의 SceneManager
//    }

//    public void Clear()
//    {
//        CurrentScene.Clear();
//    }
//}


//public class Manager
//{
//    SceneManagerEx _scene = new SceneManagerEx();
//    public static SceneManagerEx Scene { get { return Instance._scene; } }
//}

//public abstract class SceneBase : MonoBehaviour
//{
//    public SceneType scene { get; protected set; } = SceneType.none;
//    public EventSystem eventSystem = null;

//    void Awake()
//    {
//        Initialize();
//    }


//    protected virtual void Initialize()
//    {
//        eventSystem = (EventSystem)GameObject.FindObjectOfType(typeof(EventSystem));
//        if (eventSystem == null)
//        {
//            Managers.Resource.Instantiate("UI/EventSystem").name = "@EventSystem";
//        }
//    }

//    public abstract void Clear();
//}
