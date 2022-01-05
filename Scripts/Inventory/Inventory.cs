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

	public abstract class Inventory /*: IInventory*/ {

		//public Dictionary<int, Item> inventory = new Dictionary<int, Item>();
		public Inventory(uint maxSize) => MaxSize = maxSize;
		public uint MaxSize { get; protected set; }

		/********************************/
		/* Behaviours funcs             */
		/********************************/

		//public bool HaveSpace() => inventory.Count < MaxSize;
		//public bool HaveItem(int key) => inventory.ContainsKey(key);
		//public bool HaveItem(Item item) => inventory.ContainsKey(item.Uid);



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





	public class WareHouse : Inventory {

		public WareHouse(uint maxSize) : base(maxSize) { }

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
