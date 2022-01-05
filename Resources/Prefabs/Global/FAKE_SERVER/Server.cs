using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using System;

public partial class Server : MonoBehaviour {
    static int UID = 0;

    Dictionary<int, object> item  = new Dictionary<int, object>();
    Dictionary<int, object> buff  = new Dictionary<int, object>();
    Dictionary<int, object> spell = new Dictionary<int, object>();

    //NullReferenceException: Object reference not set to an instance of an object.


    void Start() {
        MakeItemDictionary(ReadFile("objects.json"));
    }

    void MakeItemDictionary(string source) {
        Debug.Log($"source = {source}");
        item = JsonConvert.DeserializeObject<Dictionary<int, object>>(source);
    }

    string ReadFile(string fileName) {
        string path = Path.Combine(Application.streamingAssetsPath, fileName);

        if (Application.platform.Equals(RuntimePlatform.Android)) {
            UnityWebRequest reader = UnityWebRequest.Get(path);

            reader.SendWebRequest();
            while (!reader.isDone) ;
            return string.IsNullOrEmpty(reader.error) ? reader.downloadHandler.text : null;
        }
        else {
            StreamReader streamReader = new StreamReader(path);
            StringBuilder stringBuilder = new StringBuilder(string.Empty);

            while (!streamReader.EndOfStream)
                stringBuilder.Append(streamReader.ReadLine());

            streamReader.Close();
            return stringBuilder.ToString();
        }
    }

    public int RequestNewUID => UID++;

    public string[] RequestNewItem(params int[] itemIdArray) {
        List<string> list = new List<string>();

        foreach(int itemId in itemIdArray) {
            if (item.ContainsKey(itemId))
                list.Add(item[itemId].ToString());
        }

        return list.ToArray();
    }
}
