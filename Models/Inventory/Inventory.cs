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

	public abstract class Inventory: IPopupObserver /*: IInventory*/ {

		protected Dictionary<int, Item> inventory = new Dictionary<int, Item>();
		public uint MaxSize { get; protected set; }
		public Inventory(uint maxSize) => MaxSize = maxSize;



		/********************************/
		/* Behaviours funcs             */
		/********************************/

		public bool HaveItem(int key) => inventory.ContainsKey(key);
		public bool HaveItem(Item item) => inventory[item.Uid] != null;
		public bool HaveSpace() => inventory.Count < MaxSize;

		public Item InsertItem(Item item) {
			if (inventory.ContainsKey(item.Uid))
				return null;
			// if (item.HaveAttribute(ItemCat.stackable))
			// 	return StackItem(item);
			// if (inventory.Count <= MaxSize)
			// 	return NewItem(item);
			return null;
		}

		public void Use(int key) {
			if (inventory.ContainsKey(key))
				inventory[key].Use();
		}



		/********************************/
		/* Implement Observer			*/
		/********************************/

		public void Dispose() {
			RemoveExhaustedElement();
		}

		private void RemoveExhaustedElement() {
			int[] array = inventory.Where(e => !e.Value.IsExist).Select(e => e.Key).ToArray();

			foreach (int key in array)
				inventory.Remove(key);
		}

/*fix begin*/
		public void Insert(Item item) {
			if (item != null && !inventory.ContainsKey(item.Uid))
				AddItem(item);
		}

		public void Remove(Item item) {
			if (item != null)
				inventory.Remove(item.Uid);
		}
/*fix end*/

		private void AddItem(Item item) {
			if (!inventory.ContainsKey(item.Uid)) {
				inventory.Add(item.Uid, item);
				item.AddDelegate(Dispose);
			}
		}



		/********************************/
		/* Implement Detail				*/
		/********************************/

		private Item StackItem(StackableItem item) {
			var array = inventory
				.Where(e => e.Value.NameId.Equals(item.NameId))
				.Select(e => (StackableItem)e.Value)
				.OrderBy(e => e.SlotId);

			foreach (var element in array) {
				element.AddStack(item);
				if (!item?.IsExist ?? true)
					return null;
			}

			if (inventory.Count < MaxSize)
				return NewItem(item);
			return null;
		}

		private Item NewItem(Item item) {
			if (inventory.Count >= MaxSize)
				return null;



			return null;
		}

/*fix begin*/
		public void AddStackable(StackableItem item) {
			var list = inventory
				.Where(e => e.Value.NameId.Equals(item.NameId))
				.Select(e => (StackableItem)e.Value)
				.OrderBy(e => e.SlotId);

			foreach (var element in list) {
				element.AddStack(item);
				if (!item?.IsExist ?? true)
					return;
			}
		}

		public (Item added, Item remain) AddNew(Item item) {
			Guard.MustNotInclude(item.Uid, inventory, "[AddNew - in WareHouse]");

			if (inventory.Count < MaxSize) {

				if (item.HaveAttribute(ItemCat.stackable)) {
					StackableItem newStack = (StackableItem)item.DeepCopy(Manager.Instance.network.REQ_NEW_ID());
					newStack.AddStack(item as StackableItem);
					AddItem(newStack);
					return (newStack, (0 < item.UseableCount ? item : null));
				}
				else {
					AddItem(item);
					return (item, null);
				}
			}
			return (null, item);
		}
/*fix end*/



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
