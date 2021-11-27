using UnityEngine;
using System.Collections;
using System;
using System.Text;
using Popup.Configs;
using Popup.Defines;
using Popup.Framework;
using Newtonsoft.Json;
using System.Linq;
using System.Runtime.CompilerServices;





namespace Popup.Library
{
    using Config = Configs.Config;
    public static class Libs
    {
        public static void Quit()
        {
#if UNITY_EDITOR
             Debug.Log("call quit on UNITY_EDITOR");
             UnityEditor.EditorApplication.isPlaying = false;
#else
            Debug.Log("call quit on not UNITY_EDITOR");
            Application.Quit();
#endif
        }

        public static bool IsInclude<T>(int index, T[] array) => 0 <= index && index < array.Length;
        public static bool IsInclude(int index, int maxSize) => index < maxSize;
        public static void IncreaseValue(int dest, int amount) => dest += amount;
        public static bool IsUnder(int dest, int cap) => dest < cap;
        public static T ConvertTo<T>(object item) => (T)item;
        public static T FromJson<T>(string source) => JsonConvert.DeserializeObject<T>(source);
        public static string ToJson<T>(T source) => JsonConvert.SerializeObject(source);
        // public static T      	FromJson<T>     (string  source                )    => JsonUtility.FromJson<T>(source);
        // public static string	ToJson<T>       (T       source                )    => JsonUtility.ToJson(source);
        public static int Round(float value) => (int)Math.Round(value);
        public static bool IsEnablePair(bool _lock, bool _key) => !_lock || (_lock && _key);


        //public static int[] TextToIntArray(string text, int falseValue = -1)
        //{
        //    int[] result = null;

        //    if (text != null)
        //    {
        //        string[] split = text.Split(',');
        //        result = new int[split.Length];

        //        for (int index = 0; index < split.Length; ++index)
        //        {
        //            if (!int.TryParse(split[index], out result[index]))
        //            {
        //                result[index] = -1;
        //            }
        //        }
        //    }

        //    return result;
        //}


        // public static T FindFirstEmpty<T>(T[] array) => array.FirstOrDefault(e => e == null);
        // public static T FindMatchElement<T>(int uid, T[] array) where T: IPopupObject => array.FirstOrDefault(e=> e.uid == uid);
        // public static T FindSpace<T>(int uid, T[] array) where T: IItem => array.FirstOrDefault(e => e.HasSpace);

        public static bool IsExist<T>(T obj) where T : IPopupObject => obj?.IsExist ?? false;
        public static bool IsExhaust<T>(T obj) where T : IPopupObject => obj != null && !obj.IsExist;
        public static int FindEmptyIndex<T>(T[] array, int startIndex = 0)
        {
            int index = startIndex;

            for (; index < array.Length; ++index)
            {
                if (array[index] == null) break;
            }

            return index;
        }


        public static void PutCenter(Transform parent, Transform child)
        {
            child.SetParent(parent);
            //child.parent = parent;
            child.localPosition = Vector3.zero;
        }
    }



    public static class Guard
    {
        public class Error : Exception
        {
            public Error(string message)
            {
                Debug.LogError(message);
                Libs.Quit();
            }
        }


        public static void MustInRange<T>(int index, T[] array, [CallerMemberName] string caller = "")
        {
            if (!Libs.IsInclude(index, array))
            {
                throw new Error($"{index} is out of range (0~ {array.Length}) - {caller}");
            }
        }


        public static T MustInclude<T>(int uid, T[] array, [CallerMemberName] string caller = "") where T : IPopupObject
        {
            for (int i = 0; i < array.Length; ++i)
            {
                if (array[i].uid.Equals(uid))
                    return array[i];
            }
            throw new Error($"Error: not include - {caller}");
        }


        public static void MustInclude(int index, int maxSize, [CallerMemberName] string caller = "")
        {
            if (!Libs.IsInclude(index, maxSize))
                throw new Error($"Error: not include - {caller}");
        }


        public static void MustInclude(int key, IDictionary dictionary, [CallerMemberName] string caller = "")
        {
            if (!dictionary.Contains(key))
                throw new Error($"Error: not include - {caller}");
        }

        public static void MustNotInclude(int index, int maxSize, [CallerMemberName] string caller = "")
        {
            if (Libs.IsInclude(index, maxSize))
                throw new Error($"Error: include - {caller}");
        }

        public static void MustNotInclude(object key, IDictionary dictionary, [CallerMemberName] string caller = "")
        {
            if (dictionary.Contains(key))
                throw new Error($"Error: include - {caller}");
        }

        public static T MustConvertTo<T>(object item, [CallerMemberName] string caller = "") where T : class
        {
            if (item == null) { return null; }
            if (item is T t) { return t; }
            throw new Error($"Convert Fail - {caller}");
        }


        public static void MustNotNull(object obj, [CallerMemberName] string caller = "")
        {
            if (obj == null)
                throw new Error($"This is null - {caller}");
        }
    }



    public static class Alignment
    {
        public static void ToInclude(ref int value, (int min, int max) range)
        {
            if (value < range.min) { value = range.min; }
            if (range.max < value) { value = range.max; }
        }
    }



    public static class DebugC
    {
        public static void Log(string message, Color color = default)
        {
            Debug.Log(string.Format("<color=#{0:X2}{1:X2}{2:X2}>{3}</color>", (byte)(color.r * 255f), (byte)(color.g * 255f), (byte)(color.b * 255f), message));
        }
    }



    public static class Extensions
    {
        public static void PositionOnParent(this GameObject myObj, GUIPosition anchor, GameObject parent = null)
        {
            parent = parent ?? Manager.Instance.guiGuide.canvas.gameObject;
            myObj.transform.SetParent(parent.transform);
            myObj.transform.localPosition = Manager.Instance.guiGuide.Position(myObj, anchor);
        }

        public static void PositionOnParent(this GameObject myObj, GUIPosition anchor, Vector2 rectSize, GameObject parent = null)
        {
            parent = parent ?? Manager.Instance.guiGuide.canvas.gameObject;
            myObj.transform.SetParent(parent.transform);
            myObj.transform.localPosition = Manager.Instance.guiGuide.Position(myObj, anchor);
        }
    }
}
