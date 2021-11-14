using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Popup.Inventory;
using Popup.Items;
using Popup.Configs;
using Popup.Status;
using Popup.Utils;





public class Game : MonoBehaviour
{
    // Start is called before the first frame update



    void Start()
    {
        Debug.Log("enter Game");

        //StartCoroutine(WaitDB());
        //DEBUG_ShowInventory();
        //DEBUG_Convert();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        Item test = Libs.FromJson<Item>(TEST_JSON_ITEM1);
        string json;

        Debug.Log(test.GetName);
        Debug.Log(test.GetUID);
        Debug.Log(test.GetAmount);
        Debug.Log(test.GetWeight);
        Debug.Log(test.GetVolume);
        Debug.Log(test.GetSpellAmount);


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

        Item item1 = new Item(Libs.RequestNewUID);
        item1.SetName("stack1");
        item1.SetCat(ItemCat.stackable);
        item1.SetAmount(12);
        item1.SetWeight(0.10f);
        item1.SetVolume(0.20f);

        Item item2 = new Item(Libs.RequestNewUID);
        item2.SetName("stack2");
        item2.SetCat(ItemCat.stackable);
        item2.SetAmount(5);
        item2.SetWeight(0.50f);
        item2.SetVolume(0.10f);

        Item item3 = new Item(Libs.RequestNewUID);
        item3.SetName("stack1");
        item3.SetCat(ItemCat.stackable);
        item3.SetAmount(25);
        item3.SetWeight(0.10f);
        item3.SetVolume(0.20f);

        Item item4 = new Item(Libs.RequestNewUID);
        item4.SetName("weapon1");
        item4.SetCat(ItemCat.equip);
        item4.SetAmount(1);
        item4.SetWeight(2f);
        item4.SetVolume(3f);

        Item item5 = new Item(Libs.RequestNewUID);
        item5.SetName("helmet1");
        item5.SetCat(ItemCat.equip);
        item5.SetAmount(1);
        item5.SetWeight(0.2f);
        item5.SetVolume(1f);

        Item item6 = new Item(Libs.RequestNewUID);
        item6.SetName("leggings1");
        item6.SetCat(ItemCat.equip);
        item6.SetAmount(1);
        item6.SetWeight(0.5f);
        item6.SetVolume(5f);

        Item item7 = new Item(Libs.RequestNewUID);
        item7.SetName("chest1");
        item7.SetCat(ItemCat.equip);
        item7.SetAmount(1);
        item7.SetWeight(1f);
        item7.SetVolume(5f);

        Item item8 = new Item(Libs.RequestNewUID);
        item8.SetName("boots1");
        item8.SetCat(ItemCat.equip);
        item8.SetAmount(1);
        item8.SetWeight(0.2f);
        item8.SetVolume(1f);

        Item item9 = new Item(Libs.RequestNewUID);
        item9.SetName("gloves1");
        item9.SetCat(ItemCat.equip);
        item9.SetAmount(1);
        item9.SetWeight(0.2f);
        item9.SetVolume(1f);

        Item item10 = new Item(Libs.RequestNewUID);
        item10.SetName("stack1");
        item10.SetCat(ItemCat.stackable);
        item10.SetAmount(10);
        item10.SetWeight(0.10f);
        item10.SetVolume(0.20f);

        //inventory.DEBUG_ShowAllItems();

        //Debug.Log("add item1 = " + inventory.AddItem(ref item1));
        //Debug.Log("add item2 = " + inventory.AddItem(ref item2));
        //Debug.Log("add item3 = " + inventory.AddItem(ref item3));
        Debug.Log("add item4 = " + inventory.AddItem(ref item4));
        Debug.Log("add item5 = " + inventory.AddItem(ref item5));
        Debug.Log("add item6 = " + inventory.AddItem(ref item6));
        Debug.Log("add item7 = " + inventory.AddItem(ref item7));
        Debug.Log("add item8 = " + inventory.AddItem(ref item8));
        Debug.Log("add item9 = " + inventory.AddItem(ref item9));
        Debug.Log("add item1 = " + inventory.AddItem(ref item1));
        Debug.Log("add item2 = " + inventory.AddItem(ref item2));
        Debug.Log("add item3 = " + inventory.AddItem(ref item3));

        //inventory.UseItem(1);
        //inventory.UseItem(2);



        //Debug.Log("add item10 = " + inventory.AddItem(ref item10));

        //inventory.ShowAllItems();
        //Debug.Log(item10.GetAmount);

        Inventory inventory2 = new Inventory(ref inventory);

        Debug.Log("use i1 10 = " + inventory.UseItem(10));
        Debug.Log("use i1 10 = " + inventory.UseItem(10));
        Debug.Log("use i1 10 = " + inventory.UseItem(10));
        Debug.Log("use i1 10 = " + inventory.UseItem(10));
        Debug.Log("use i1 10 = " + inventory.UseItem(10));
        Debug.Log("use i1 10 = " + inventory.UseItem(10));
        Debug.Log("use i2 10 = " + inventory2.UseItem(10));
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
    }
}
