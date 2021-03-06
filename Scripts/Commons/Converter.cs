using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Popup.Framework;
using Popup.Defines;
using Popup.Items;
using Newtonsoft.Json.Linq;


namespace Popup.Converter {

    public static class FromJson {

        //public static string FromEnum<T>(T eName) where T : struct {
        //    string name = Enum.GetName(typeof(T), eName);
        //    return name;
        //}

        public static m_item ToItem (string json) {
            JObject obj = JObject.Parse(json);
            string type = obj[ParamNames.type].ToString();

            if (type.Equals(ParamNames.solid))
                return obj[ParamNames.contents].ToObject<m_solid_item>();
            if (type.Equals(ParamNames.stackable))
                return obj[ParamNames.contents].ToObject<m_stackable_item>();

            return null;
        }
    }

}
