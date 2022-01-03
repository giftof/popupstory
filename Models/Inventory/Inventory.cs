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

		public void Use(Item item) {
			if (inventory.ContainsKey(item.Uid))
				item.Use();
		}

		public void Insert(Item item) {
			if (item != null && !inventory.ContainsKey(item.Uid))
				AddDictionary(item);
		}

		public void Remove(Item item) {
			if (item != null)
				inventory.Remove(item.Uid);
		}

		public List<Item> UnslotedList() {
			return inventory
				.Where(pair => pair.Value.SlotId.Equals(Config.unSlot))
				.Select(pair => pair.Value)
				.ToList();
		}

		private void AddDictionary(Item item) {
			if (!inventory.ContainsKey(item.Uid)) {
				inventory.Add(item.Uid, item);
				item.AddDelegate(Dispose);
			}
		}

		/********************************/
		/* Implement Observer			*/
		/********************************/

		public void Dispose() {
			RemoveExhaustedElement();
		}

		private void RemoveExhaustedElement() {
			var array = inventory.Where(e => !e.Value.IsExist).Select(e => e.Key).ToArray();

			foreach (var key in array)
				inventory.Remove(key);
		}



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
