using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;



public class Server : MonoBehaviour
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

        foreach (var pair in item)
        {
            Debug.Log($"key = {pair.Key}, value = {pair.Value}");
        }
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

    public int RequestNewUID => UID++;
}
