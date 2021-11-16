using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Popup.Inventory;
using Popup.Items;
using Popup.Configs;
using Popup.Defines;
using Popup.Library;
using Newtonsoft.Json;

using Popup.ServerJob;





public class Game : MonoBehaviour
{
    // Start is called before the first frame update

    public Button toEntrance;
    public Button toLobby;
    private Manager manager;

    // Start is called before the first frame update
    void Awake()
    {
        Initialize();

        Debug.Log("enter Game");
        toEntrance.onClick.AddListener(() =>
        {
            manager.eventSystem.enabled = false;
            manager.sceneController.Load(SceneType.entrance);
        });
        toLobby.onClick.AddListener(() =>
        {
            manager.eventSystem.enabled = false;
            manager.sceneController.Load(SceneType.lobby);
        });
        //StartCoroutine(WaitDB());
        DEBUG_ShowInventory();
        //DEBUG_Convert();
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

    //IEnumerator WaitDB()
    //{
    //    while (!db.ready)
    //    {
    //        yield return null;
    //    }
    //    DEBUG_Convert();
    //}

    void DEBUG_Convert()
    {
        string TEST_JSON_ITEM1 = "{\"name\":\"glass sword\",\"uid\":88,\"weight\":1.2,\"volume\":3.4,\"amount\":1,\"grade\":4,\"category\":63,\"magicIdArray\":[1,2,3,4,5]}";
        EquipItem test = Libs.FromJson<EquipItem>(TEST_JSON_ITEM1);
        string json;

        Debug.Log(test.name);
        Debug.Log(test.uid);
        Debug.Log(test.durability);
        Debug.Log(test.Weight());
        Debug.Log(test.Volume());
        Debug.Log(test.SpellAmount);


        // string json = Libs.ToJson(item8);
        // Debug.Log(json);


        json = Libs.ToJson(test);
        Debug.Log(json);


        //string TEST_INSERT = "Insert into itemTable(uid, name, weight, volume, amount, grade, magicIdArray, category, durability) VALUES(99, \"glass sword\", 1.2, 3.4, 1, 4, \"1,2,3,4,5\", 63, 10)";
        //db.DataBaseInsert(TEST_INSERT);



        //string TEST_READ = "Select * From itemTable";
        //Debug.Log(TEST_READ);
        //List<Item> list = db.DataBaseRead<Item>(TEST_READ, Item.convert);
        //Debug.Log(list.Count);
        //if (0 < list.Count)
        //{
        //    Debug.Log(list[0].GetName);
        //    Debug.Log(list[0].GetUID);
        //    Debug.Log(list[0].GetWeight);
        //    Debug.Log(list[0].GetVolume);
        //    Debug.Log(list[0].GetAmount);
        //    Debug.Log(list[0].GetDurability);
        //    if (0 < list[0].GetSpellAmount)
        //    {
        //        for (int i = 0; i < list[0].GetSpellAmount; ++i)
        //        {
        //            Debug.Log("spellID = " + list[0].GetSpell(i));
        //        }
        //    }
        //}
        //else
        //{
        //    string TEST_INSERT = "Insert into itemTable(uid, name, weight, volume, amount, grade, magicIdArray, category, durability) VALUES(99, \"glass sword\", 1.2, 3.4, 1, 4, \"1,2,3,4,5\", 63, 10)";
        //    db.DataBaseInsert(TEST_INSERT);
        //}
    }

    void DEBUG_ShowInventory()
    {
        Inventory inventory = new Inventory(null, Configs.squadInventorySize);

        string itemDef1 = $"{{\"uid\":0,\"name\":\"stack1\",\"category\":{(int)ItemCat.tool},\"amount\":12,\"weight\":0.1,\"volume\":0.2}}";
        Item item1 = JsonConvert.DeserializeObject<ToolItem>(itemDef1);
        Debug.Log(item1);
        Debug.Log(item1.uid);
        Debug.Log(item1.name);


        string itemDef2 = $"{{\"uid\":1,\"name\":\"stack2\",\"category\":{(int)ItemCat.tool},\"amount\":5,\"weight\":0.5,\"volume\":0.1}}";
        Item item2 = JsonConvert.DeserializeObject<ToolItem>(itemDef2);

        // Item item3 = new ToolItem(ServerJob.RequestNewUID);
        // item3.SetName("stack1");
        // item3.SetCat(ItemCat.tool);
        // ((ToolItem)item3).SetAmount(25);
        // item3.SetWeight(0.10f);
        // item3.SetVolume(0.20f);

        // Item item4 = new EquipItem(ServerJob.RequestNewUID);
        // item4.SetName("weapon1");
        // item4.SetCat(ItemCat.equip);
        // item4.SetAmount(1);
        // item4.SetWeight(2f);
        // item4.SetVolume(3f);

        // Item item5 = new EquipItem(ServerJob.RequestNewUID);
        // item5.SetName("helmet1");
        // item5.SetCat(ItemCat.equip);
        // item5.SetAmount(1);
        // item5.SetWeight(0.2f);
        // item5.SetVolume(1f);

        // Item item6 = new EquipItem(ServerJob.RequestNewUID);
        // item6.SetName("leggings1");
        // item6.SetCat(ItemCat.equip);
        // item6.SetAmount(1);
        // item6.SetWeight(0.5f);
        // item6.SetVolume(5f);

        // Item item7 = new EquipItem(ServerJob.RequestNewUID);
        // item7.SetName("chest1");
        // item7.SetCat(ItemCat.equip);
        // item7.SetAmount(1);
        // item7.SetWeight(1f);
        // item7.SetVolume(5f);

        string itemDef8 = $"{{\"uid\":7,\"name\":\"boots1\",\"category\":{(int)ItemCat.equip},\"durability\":50,\"weight\":0.2,\"volume\":1}}";
        Item item8 = JsonConvert.DeserializeObject<EquipItem>(itemDef8);

        // Item item9 = new EquipItem(ServerJob.RequestNewUID);
        // item9.SetName("gloves1");
        // item9.SetCat(ItemCat.equip);
        // item9.SetAmount(1);
        // item9.SetWeight(0.2f);
        // item9.SetVolume(1f);

        // Item item10 = new ToolItem(ServerJob.RequestNewUID);
        // item10.SetName("stack1");
        // item10.SetCat(ItemCat.tool);
        // item10.SetAmount(10);
        // item10.SetWeight(0.10f);
        // item10.SetVolume(0.20f);

        //inventory.DEBUG_ShowAllItems();

        Debug.Log("add item1 = " + inventory.AddItem(item1));
        Debug.Log("add item2 = " + inventory.AddItem(item2));
        // Debug.Log("add item3 = " + inventory.AddItem(item3));
        // Debug.Log("add item4 = " + inventory.AddItem(item4));
        // Debug.Log("add item5 = " + inventory.AddItem(item5));
        // Debug.Log("add item6 = " + inventory.AddItem(item6));
        // Debug.Log("add item7 = " + inventory.AddItem(item7));
        Debug.Log("add item8 = " + inventory.AddItem(item8));
        //Debug.Log("add item9 = " + inventory.AddItem(item9));
        // Debug.Log("add item1 = " + inventory.AddItem(item1));
        // Debug.Log("add item2 = " + inventory.AddItem(item2));
        // Debug.Log("add item3 = " + inventory.AddItem(item3));

        //inventory.UseItem(1);
        //inventory.UseItem(2);



        //Debug.Log("add item10 = " + inventory.AddItem(item10));

        //inventory.ShowAllItems();
        //Debug.Log(item10.GetAmount);

        Inventory inventory2 = new Inventory(inventory);

        // Debug.Log("use i1 10 = " + inventory.UseItem(10));
        // Debug.Log("use i2 10 = " + inventory2.UseItem(10));
        // Debug.Log("use i1 10 = " + inventory.UseItem(10));
        // Debug.Log("use i2 10 = " + inventory2.UseItem(10));
        // Debug.Log("use i1 10 = " + inventory.UseItem(10));
        // Debug.Log("use i2 10 = " + inventory2.UseItem(10));
        // Debug.Log("use i1 10 = " + inventory.UseItem(10));
        // Debug.Log("use i2 10 = " + inventory2.UseItem(10));
        // Debug.Log("use i1 10 = " + inventory.UseItem(10));
        // Debug.Log("use i2 10 = " + inventory2.UseItem(10));
        // Debug.Log("use i1 10 = " + inventory.UseItem(10));
        // Debug.Log("use i2 10 = " + inventory2.UseItem(10));
        // Debug.Log("use i1 11 = " + inventory.UseItem(11));
        // Debug.Log("use i2 11 = " + inventory2.UseItem(11));
        Debug.Log("use i1 0 = " + inventory.UseItem(0));
        Debug.Log("use i2 0 = " + inventory2.UseItem(0));
        Debug.Log("use i1 0 = " + inventory.UseItem(0));
        Debug.Log("use i2 0 = " + inventory2.UseItem(0));
        Debug.Log("use i1 0 = " + inventory.UseItem(0));
        Debug.Log("use i2 0 = " + inventory2.UseItem(0));
        Debug.Log("use i1 0 = " + inventory.UseItem(0));
        Debug.Log("use i2 0 = " + inventory2.UseItem(0));
        Debug.Log("use i1 0 = " + inventory.UseItem(0));
        Debug.Log("use i2 0 = " + inventory2.UseItem(0));
        Debug.Log("use i1 0 = " + inventory.UseItem(0));
        Debug.Log("use i2 0 = " + inventory2.UseItem(0));
        Debug.Log("use i1 1 = " + inventory.UseItem(1));
        Debug.Log("use i2 1 = " + inventory2.UseItem(1));


        // Debug.Log("use i1 11 = " + inventory.UseItem(11));

        // Debug.Log("use i2 10 = " + inventory2.UseItem(10));
        // Debug.Log("use i2 11 = " + inventory2.UseItem(11));

        // Debug.Log("use i1 10 = " + inventory.UseItem(10));
        // Debug.Log("use i1 11 = " + inventory.UseItem(11));

        // Debug.Log("use i2 10 = " + inventory2.UseItem(10));
        // Debug.Log("use i2 11 = " + inventory2.UseItem(11));

        // Debug.Log("use i1 10 = " + inventory.UseItem(10));
        // Debug.Log("use i1 11 = " + inventory.UseItem(11));

        // Debug.Log("use i2 10 = " + inventory2.UseItem(10));
        // Debug.Log("use i2 11 = " + inventory2.UseItem(11));

        // Debug.Log("use i1 10 = " + inventory.UseItem(10));
        // Debug.Log("use i1 11 = " + inventory.UseItem(11));

        // Debug.Log("use i2 10 = " + inventory2.UseItem(10));
        // Debug.Log("use i2 11 = " + inventory2.UseItem(11));

        inventory.DEBUG_ShowAllItems();
        inventory2.DEBUG_ShowAllItems();
        // string TEST_JSON_ITEM1 = "{\"name\":\"glass sword\",\"uid\":99,\"weight\":1.2,\"volume\":3.4,\"amount\":1,\"grade\":4,\"category\":63,\"magicIdArray\":[1,2,3,4,5]}";

        // string json = Libs.ToJson(item8);
        // Debug.Log(json);
        // Item DT = JsonConvert.DeserializeObject<ToolItem>(json);
        // Debug.Log(DT.GetName());
        // Debug.Log(DT.GetWeight());
        // Debug.Log(DT.GetVolume());

        Debug.Log(JsonConvert.SerializeObject(item1));
        Debug.Log(JsonConvert.SerializeObject(item2));
        Debug.Log(JsonConvert.SerializeObject(item8));
    }
}
