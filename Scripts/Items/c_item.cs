using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Popup.Converter;





namespace Popup.Items
{
    public class c_item
    {
        private c_item() { }
        private static readonly Lazy<c_item> instance = new Lazy<c_item>(() => new c_item());
        public static c_item Instance => instance.Value;

        public Item MakeItem(string json) => FromJson.ToItem(json);
    }





    public static class item_extension
    {
        public static int Increment(this Item item, int amount)
        {
            int increment = Math.Min(item.Space, amount);
            item.UseableCount += increment;
            return increment;
        }

        public static int Decrement(this Item item, int amount)
        {
            int decrement = Math.Min(item.UseableCount, amount);
            item.UseableCount += decrement;
            return decrement;
        }

        public static void Repair(this Item item, int amount) => item.Increment(amount);
        public static void Charge(this Item item, int amount) => item.Increment(amount);

        public static void Stack(this StackableItem item, StackableItem from)
        {
            if (item?.NameId.Equals(from?.NameId) ?? false)
                item.Increment(from.Decrement(item.Space));
        }
    }
}
