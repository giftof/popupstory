using UnityEngine;
using System.Collections;
using System;
using System.Text;
using Popup.Configs;





namespace Popup.Utils
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


        public static void InRange<T>(int index, ref T[] array)
        {
            if (!Libs.IsInclude(index, ref array))
            {
                throw new Error(MakeString(index.ToString(), " is out of range(0 ~ ", array.Length.ToString(), ")") );
            }
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


        public static T MustConvertTo<T> (object item) where T: class
        {
            if (item == null) { return null; }
            if (item is T t)  { return t; }
            throw new Error("Convert Fail");
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

    public static class Extension
    {
    }
}
