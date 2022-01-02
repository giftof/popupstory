using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Popup.Configs;
using Popup.Items;
using Popup.Library;
using Popup.Delegate;
using Popup.Framework;
using Popup.Defines;





namespace Popup.Inventory {

	public abstract class Inventory /*: IInventory*/ {

		public Dictionary<int, Item> inventory = new Dictionary<int, Item>();
		public uint MaxSize { get; protected set; }

		public Inventory(uint maxSize) => MaxSize = maxSize;



		/********************************/
		/* Behaviours funcs             */
		/********************************/

		public bool Add(Item item) {
			if (Libs.IsExhaust(item))
				return false;

			return item.HaveAttribute(ItemCat.stackable)
				? AddStackable(item as StackableItem)
				: AddNew(item);
		}

		private bool AddStackable(StackableItem item) {
			var list = inventory
				.Where(e => e.Value.NameId.Equals(item.NameId))
				.Select(e => (StackableItem)e.Value)
				.OrderBy(e => e.SlotId);

			foreach (var element in list)
				if (element.AddStack(item)) return true;

			while (item.IsExist && HaveSpace()) {
				StackableItem newItem = (StackableItem)item.DeepCopy(Manager.Instance.network.REQ_NEW_ID(), null);
				newItem.AddStack(item);
				inventory.Add(newItem.Uid, newItem);
			}
			return !item.IsExist;
		}

		private bool AddNew(Item item) {
			Guard.MustNotInclude(item.Uid, inventory, "[AddNew - in WareHouse]");

			if (inventory.Count < MaxSize) {
				inventory.Add(item.Uid, item);
				return true;
			}

			return false;
		}



		private bool HaveSpace() => inventory.Count < MaxSize;

		public void EraseExhaustedSlot() {
			var empty = inventory.Where(e => !e.Value.IsExist).Select(e => e.Value).ToArray();

			for (int i = 0; i < empty.Length; ++i) {
				empty[i].removeEmptySlot?.Invoke();
				inventory.Remove(empty[i].Uid);
			}
		}

		public bool Use(Item item) {
			if (inventory.ContainsKey(item.Uid) && item.Use()) {
				if (!item.IsExist) inventory.Remove(item.Uid);
				return true;
			}
			return false;
		}

		//public void Insert(Item item) {
		//	if (item == null)
		//		return;

		//	if (!inventory.ContainsKey(item.Uid))
		//		inventory.Add(item.Uid, item);
		//}

		public void Remove(Item item) {
			if (item == null)
				return;

			inventory.Remove(item.Uid);
		}

		//public Item[] TakeAll() {
		//	Item[] array = inventory.Values.ToArray();
		//	inventory.Clear();
		//	return array;
		//}

		public List<Item> UnslotedList() {
			return inventory
				.Where(pair => pair.Value.SlotId.Equals(Config.unSlot))
				.Select(pair => pair.Value)
				.ToList();
		}



		/********************************/
		/* Abstract funcs              	*/
		/********************************/



		/********************************/
		/* TEST funcs              	    */
		/********************************/

		public abstract void DEBUG_ShowAllItems();
	}




	public class WareHouse : Inventory {

		public WareHouse(uint maxSize) : base(maxSize) { }



		/********************************/
		/* Implement Abstract funcs		*/
		/********************************/





		/********************************/
		/* TEST funcs              	    */
		/********************************/

		public override void DEBUG_ShowAllItems() {
			List<int> keys = inventory.Keys.ToList();
			foreach (int key in keys) {
				Item item = inventory[key];
				Debug.Log("UID = " + item.Uid + ", name = " + item.Name + ", slotId = " + item.SlotId + ", amt = " + item.UseableCount + ", w = " + item.TWeight() + ", v = " + item.TVolume());
			}
		}
	}
}
