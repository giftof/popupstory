using UnityEngine;
using System.Collections;
using System;
using System.Text;
using Popup.Configs;
using Popup.Framework;





namespace Popup.Library
{
    using Configs = Configs.Configs;
    public static class Libs
    {
        //private utils() { }

        //private static readonly Lazy<utils> instance = new Lazy<utils>(() => new utils());

        //public static utils Instance
        //{
        //    get { return instance.Value; }
        //}

        public static void Quit()
        {
            Debug.Log("call quit on not UNITY_EDITOR");
            Application.Quit();
// #if UNITY_EDITOR
//             Debug.Log("call quit on UNITY_EDITOR");
//             UnityEditor.EditorApplication.isPlaying = false;
// #else
//             Debug.Log("call quit on not UNITY_EDITOR");
//             Application.Quit();
// #endif
        }

        public static bool   	IsInclude<T>    (int     index, ref T[] array  )    => 0 <= index && index < array.Length;
        public static bool   	IsInclude       (int     index, int     maxSize)    => index < maxSize;
        public static void   	IncreaseValue	(ref int dest,  int     amount )    => dest += amount;
        public static bool   	IsUnder         (int     dest,  int     cap    )    => dest < cap;
        public static T      	ConvertTo<T>    (object  item                  )    => (T)item;
        public static T      	FromJson<T>     (string  source                )    => JsonUtility.FromJson<T>(source);
        public static string	ToJson<T>       (T       source                )    => JsonUtility.ToJson(source);
		public static int		Round			(float	 value                 )    => (int)Math.Round(value);
        public static bool      IsEnablePair    (bool    _lock, bool    _key   )    => !_lock || (_lock && _key);
        

   		public static int[] TextToIntArray(string text, int falseValue = -1)
		{
			int[] result = null;

			if (text != null)
			{
				string[] split = text.Split(',');
				result = new int[split.Length];

				for (int index = 0; index < split.Length; ++index)
				{
					if (!int.TryParse(split[index], out result[index]))
					{
						result[index] = -1;
					}
				}
			}

			return result;
		}

        public static int FindEmptyIndex<T>(ref T[] array, int startIndex = 0)
        {
            int index = startIndex;

            for (; index < array.Length; ++index)
            {
                if (array[index] == null) break;
            }

            return index;
        }

        
    }

    public static class Guard
    {
        public class Error: Exception
        {
            public Error(string message)
            {
                Debug.LogError(message);
                Libs.Quit();
            }
        }


        private static string MakeString(params string[] args)
        {
            StringBuilder stringBuilder = new StringBuilder(string.Empty, Configs.stringSize);

            foreach (string element in args)
            {
                stringBuilder.Append(element);
            }
            return stringBuilder.ToString();
        }


        public static void MustInRange<T>(int index, ref T[] array, string caller)
        {
            if (!Libs.IsInclude(index, ref array))
            {
                throw new Error(MakeString(index.ToString(), " is out of range(0 ~ ", array.Length.ToString(), ") - " + caller) );
            }
        }


        public static ref T MustInclude<T>(int uid, ref T[] array, string caller) where T: IPopupObject
        {
            for(int i = 0; i < array.Length; ++i)
            {
                if (array[i].GetUID().Equals(uid))
                {
                    return ref array[i];
                }
            }
            throw new Error("Error: not include - " + caller);
        }


        public static void MustInclude(int index, int maxSize, string caller)
        {
            if (!Libs.IsInclude(index, maxSize))
            {
                throw new Error("Error: not include - " + caller);
            }
        }


        public static void MustNotInclude(int index, int maxSize, string caller)
        {
            if (Libs.IsInclude(index, maxSize))
            {
                throw new Error("Error: include - " + caller);
            }
        }


        public static T MustConvertTo<T> (object item, string caller) where T: class
        {
            if (item == null) { return null; }
            if (item is T t)  { return t; }
            throw new Error("Convert Fail - " + caller);
        }


        public static void MustNotNull(object obj, string caller)
        {
            if (obj == null)
                throw new Error("This is null - " + caller);
        }
    }

    public static class Alignment
    {
        public static void ToInclude(ref int value, (int, int) range)
        {
            if (value < range.Item1) { value = range.Item1; }
            if (range.Item2 < value) { value = range.Item2; }
        }
    }

    public static class DebugC
    {
        public static void Log(string message, Color color = default)
        {
            Debug.Log(string.Format("<color=#{0:X2}{1:X2}{2:X2}>{3}</color>", (byte)(color.r * 255f), (byte)(color.g * 255f), (byte)(color.b * 255f), message));
        }
        
    }
}
