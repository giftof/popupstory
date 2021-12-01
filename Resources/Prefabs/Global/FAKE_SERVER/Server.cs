using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;



public partial class Server : MonoBehaviour
{
    int UID = 0;
    string objectsPath = "Assets/Resources/Prefabs/Global/FAKE_SERVER/objects.json";

    Dictionary<int, object> item  = new Dictionary<int, object>();
    Dictionary<int, object> buff  = new Dictionary<int, object>();
    Dictionary<int, object> spell = new Dictionary<int, object>();



    void Awake()
    {
        MakeItemDictionary();
    }

    void MakeItemDictionary()
    {
        string source = ReadFile(objectsPath);
        item = JsonConvert.DeserializeObject<Dictionary<int, object>>(source);
    }

    string ReadFile(string path)
    {
        StreamReader streamReader = new StreamReader(path);
        StringBuilder stringBuilder = new StringBuilder(string.Empty);

        while (!streamReader.EndOfStream)
            stringBuilder.Append(streamReader.ReadLine());

        streamReader.Close();

        Debug.Log(stringBuilder);

        return stringBuilder.ToString();
    }
}

public partial class Server
{
    public int RequestNewUID => UID++;

    public string[] RequestNewItem(params int[] itemIdArray)
    {
        List<string> list = new List<string>();

        foreach(int itemId in itemIdArray)
        {
            if (item.ContainsKey(itemId))
                list.Add(item[itemId].ToString());
        }

        return list.ToArray();
    }
}
