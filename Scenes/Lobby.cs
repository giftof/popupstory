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
    [SerializeField] CustomButtonPrefab allieInventoryBtn;
    [SerializeField] CustomButtonPrefab enemyInventoryBtn;
    [SerializeField] InventoryPouchPrefab alliePouch;
    [SerializeField] InventoryPouchPrefab enemyPouch;



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
        alliePouch.gameObject.SetActive(false);
        enemyPouch.gameObject.SetActive(false);
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

    void ToggleAlliePouch() => alliePouch.gameObject.SetActive(!alliePouch.gameObject.activeSelf);
    void ToggleEnemyPouch() => enemyPouch.gameObject.SetActive(!enemyPouch.gameObject.activeSelf);

    void DEBUG_POUCH()
    {
        allieInventoryBtn.AddClickAction(() => {
            ToggleAlliePouch();
            alliePouch.gameObject.PositionOnParent(GUIPosition.LeftBottom);
        });
        enemyInventoryBtn.AddClickAction(() => {
            ToggleEnemyPouch();
            enemyPouch.gameObject.PositionOnParent(GUIPosition.RightBottom);
        });
    }
}
