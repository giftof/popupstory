using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Popup.Configs;
using Popup.Items;
using Popup.Library;
using Popup.Framework;
using Popup.Defines;





namespace Popup.Inventory {

	public abstract class m_inventory: PopupObject {

		public readonly List<int> slotInstanceIdList;

		public m_inventory(uint maxSize)
		{
			MaxSize = maxSize;
			slotInstanceIdList = new List<int>();
		}

		public uint MaxSize { get; protected set; }

		public bool Destructible { get; set; }

		/********************************/
		/* Abstract						*/
		/********************************/

		public override bool IsExist
		{
			get { return 0 < slotInstanceIdList.Count; }
		}

		/********************************/
		/* Checker						*/
		/********************************/

		public bool HaveSpace
		{
			get { return slotInstanceIdList.Count < MaxSize; }
		}

		/********************************/
		/* Implement Detail				*/
		/********************************/

		//      private void AddItem(Item item) {
		//          if (!inventory.ContainsKey(item.Uid))
		//              inventory.Add(item.Uid, item);
		//      }

		//      public Item InsertItem(Item item) {
		//	if (inventory.ContainsKey(item.Uid) || !item.IsExist)
		//		return null;
		//	if (item.HaveAttribute(ItemCat.stackable))
		//		return StackItem(item as StackableItem);
		//	return NewItem(item);
		//}

		//public void Dispose() {
		//	int[] array = inventory.Where(e => !e.Value.IsExist).Select(e => e.Key).ToArray();

		//	foreach (int key in array)
		//		inventory.Remove(key);
		//}

		//private Item StackItem(StackableItem item) {
		//	var array = inventory
		//		.Where(e => e.Value.NameId.Equals(item.NameId))
		//		.Select(e => (StackableItem)e.Value)
		//		.OrderBy(e => e.SlotId);

		//	//foreach (var element in array) {
		//	//	element.ChargeWith(item);
		//	//	if (!item?.IsExist ?? true)
		//	//		return null;
		//	//}

		//	return NewItem(item);
		//}

		//private Item NewItem(Item item) {
		//	if (inventory.Count >= MaxSize)
		//		return null;

		//	Item newItem;

		//	if (item.HaveAttribute(ItemCat.stackable))
		//		newItem = (StackableItem)item.DeepCopy(Manager.Instance.network.REQ_NEW_ID());
		//	else
		//		newItem = (SolidItem)item.DeepCopy(Manager.Instance.network.REQ_NEW_ID());

		//	//newItem.ChargeWith(item);
		//	//AddItem(newItem);
		//	return newItem;
		//}

		/********************************/
		/* TEST funcs              	    */
		/********************************/

		//public abstract void DEBUG_ShowAllItems();
	}





	public class WareHouse : m_inventory {

		public WareHouse(uint maxSize) : base(maxSize) { }

        public override object DeepCopy(int uid)
        {
			WareHouse wareHouse = (WareHouse)MemberwiseClone();

			wareHouse.Uid = uid;
			return wareHouse;
        }




        /********************************/
        /* TEST funcs              	    */
        /********************************/

        //public override void DEBUG_ShowAllItems() {
        //	List<int> keys = inventory.Keys.ToList();
        //	foreach (int key in keys) {
        //		Item item = inventory[key];
        //		Debug.Log("UID = " + item.Uid + ", name = " + item.Name + ", slotId = " + item.SlotId + ", amt = " + item.UseableCount + ", w = " + item.TWeight() + ", v = " + item.TVolume());
        //	}
        //	Debug.Log("-------------------------------------------------");
        //}
    }
}
