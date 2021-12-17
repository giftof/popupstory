using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Popup.Defines;
using Popup.Configs;
using Popup.Library;
using Popup.Items;
using Popup.Delegate;

using Newtonsoft.Json;



public class Lobby : MonoBehaviour
{
    [SerializeField] Button toEntrance;
    [SerializeField] Button toGame;
    [SerializeField] UserSquadPrefab userSquad;
    [SerializeField] OtherSquadPrefab otherSquad;



    void Start()
    {
        Initialize();

        Debug.Log("enter Lobby");
        DEBUG_BTN();
        //DEBUG_POUCH();
        DEBUG_ITEM();
        DEBUG_SQUAD();
    }



    private void Initialize()
    {
        _ = FindObjectOfType(typeof(Manager)) ?? Instantiate(Resources.Load<Manager>(Path.manager));
        Manager.Instance.Initialize();

        //userPouch.gameObject.SetActive(false);
        //otherPouch.gameObject.SetActive(false);
    }



    //void ToggleAlliePouch() => userPouch.gameObject.SetActive(!userPouch.gameObject.activeSelf);
    //void ToggleEnemyPouch() => otherPouch.gameObject.SetActive(!otherPouch.gameObject.activeSelf);

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

    void DEBUG_SQUAD()
    {
        userSquad.InventoryPosition(GUIPosition.LeftBottom);
        otherSquad.InventoryPosition(GUIPosition.RightBottom);
        otherSquad.TakeAllTarget = userSquad.Inventory;
    }

    //void DEBUG_POUCH()
    //{
    //    userInventoryBtn.AddClickAction(() => {
    //        ToggleAlliePouch();
    //        userPouch.gameObject.PositionOnParent(GUIPosition.LeftBottom);
    //    });
    //    otherInventoryBtn.AddClickAction(() => {
    //        ToggleEnemyPouch();
    //        otherPouch.gameObject.PositionOnParent(GUIPosition.RightBottom);
    //    });

    //    otherPouch.takeAll.AddClickAction(() => {
    //        userPouch.Insert(otherPouch.PopAll());
    //    });
    //}

    IEnumerator REQ_ITEM<T>(NET_REQ<T> req, params int[] id) where T: Item
    {
        T[] array;

        yield return array = req(id);

        foreach (var obj in array)
        {
            Debug.Log($"read obj = {Libs.ToJson(obj)}");
        }

        otherSquad.Insert(array);
    }

    void DEBUG_ITEM()
    {
        StartCoroutine(REQ_ITEM(Manager.Instance.network.REQ_NEW_ITEM_BY_ITEMID<EquipItem>, 0, 1));
        StartCoroutine(REQ_ITEM(Manager.Instance.network.REQ_NEW_ITEM_BY_ITEMID<ToolItem>, 1000, 1001, 1000));

        //otherSquad.Insert(Manager.Instance.network.REQ_NEW_ITEMS_BY_EQUIPITEMID(0, 1));
        //otherSquad.Insert(Manager.Instance.network.REQ_NEW_ITEMS_BY_TOOLITEMID(1000, 1001, 1000));

        //string TEST_JSON_ITEM1 = $"{{\"uid\":{Manager.Instance.network.REQ_NEW_ID()},\"name\":\"glass sword\",\"category\":{(int)ItemCat.equip},\"weight\":1.2,\"volume\":3.4,\"amount\":1,\"grade\":4,\"durability\":50,\"magicIdArray\":[1,2,3,4,5]}}";
        //string TEST_JSON_ITEM2 = $"{{\"uid\":{Manager.Instance.network.REQ_NEW_ID()},\"name\":\"stack1\",\"category\":{(int)ItemCat.tool},\"amount\":12,\"weight\":0.1,\"volume\":0.2}}";
        //string TEST_JSON_ITEM3 = $"{{\"uid\":{Manager.Instance.network.REQ_NEW_ID()},\"name\":\"stack2\",\"category\":{(int)ItemCat.tool},\"amount\":5,\"weight\":0.5,\"volume\":0.1}}";

        //Item item1 = Libs.FromJson<EquipItem>(TEST_JSON_ITEM1);
        //Item item2 = Libs.FromJson<ToolItem>(TEST_JSON_ITEM2);
        //Item item3 = Libs.FromJson<ToolItem>(TEST_JSON_ITEM3);

        //otherSquad.Insert(item1, item2, item3);
    }
}
