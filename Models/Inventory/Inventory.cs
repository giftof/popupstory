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

		protected Dictionary<int, Item> inventory = new Dictionary<int, Item>();
		public uint MaxSize { get; protected set; }

		public Inventory(uint maxSize) => MaxSize = maxSize;



		/********************************/
		/* Behaviours funcs             */
		/********************************/

		public bool HaveItem(int key) => inventory.ContainsKey(key);
		public bool HaveItem(Item item) => inventory[item.Uid] != null;

		public bool AddStackable(StackableItem item) {
			var list = inventory
				.Where(e => e.Value.NameId.Equals(item.NameId))
				.Select(e => (StackableItem)e.Value)
				.OrderBy(e => e.SlotId);

			foreach (var element in list)
				if (element.AddStack(item)) return true;

			return false;
		}

		public (Item added, Item remain) AddNew(Item item) {
			Guard.MustNotInclude(item.Uid, inventory, "[AddNew - in WareHouse]");

			if (inventory.Count < MaxSize) {

				if (item.HaveAttribute(ItemCat.stackable)) {
					StackableItem newStack = (StackableItem)item.DeepCopy(Manager.Instance.network.REQ_NEW_ID());
					newStack.AddStack(item as StackableItem);
					AddDictionary(newStack);
					return (newStack, (0 < item.UseableCount ? item : null));
				}
				else {
					AddDictionary(item);
					return (item, null);
				}
			}
			return (null, item);
		}

		public bool HaveSpace() => inventory.Count < MaxSize;

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

		public void Insert(Item item) {
			if (item == null)
				return;

			if (!inventory.ContainsKey(item.Uid)) 
				AddDictionary(item);
		}

		public void Remove(Item item) {
			if (item == null)
				return;

			inventory.Remove(item.Uid);
		}

		public List<Item> UnslotedList() {
			return inventory
				.Where(pair => pair.Value.SlotId.Equals(Config.unSlot))
				.Select(pair => pair.Value)
				.ToList();
		}

		private void AddDictionary(Item item) {
			inventory.Add(item.Uid, item);
			item.removeEmptyItem = RemoveElement;
		}

		private void RemoveElement(Item item) => inventory.Remove(item.Uid);

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
			Debug.Log("-------------------------------------------------");
		}
	}
}
