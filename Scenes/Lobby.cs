using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Popup.Defines;
using Popup.Configs;
using Popup.Library;



public class Lobby : MonoBehaviour
{
    [SerializeField] Button toEntrance;
    [SerializeField] Button toGame;
    [SerializeField] InventoryPouchPrefab userPouch;
    [SerializeField] InventoryPouchPrefab shopPouch;


    // Start is called before the first frame update
    void Start()
    {
        Initialize();

        Debug.Log("enter Lobby");
        DEBUG_BTN();
        DEBUG_POUCH();
    }



    private void Initialize()
    {
        _ = FindObjectOfType(typeof(Manager)) ?? Instantiate(Resources.Load<Manager>(Path.manager));
        Manager.Instance.eventSystem.enabled = true;
        Manager.Instance.guiGuide.InitializeCanvas();
    }



    void DEBUG_BTN()
    {
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

    void DEBUG_POUCH()
    {
        userPouch.gameObject.PositionOnParent(GUIPosition.LeftBottom);
        shopPouch.gameObject.PositionOnParent(GUIPosition.RightBottom);
        //userPouch.transform.localPosition = Manager.Instance.guiGuide.Position(userPouch.gameObject, GUIPosition.LeftBottom);
        //shopPouch.transform.localPosition = Manager.Instance.guiGuide.Position(shopPouch.gameObject, GUIPosition.RightBottom);
        //GUIGuide.
    }
}
