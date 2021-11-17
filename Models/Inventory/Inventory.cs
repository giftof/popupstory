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





namespace Popup.Inventory
{

	public class Inventory : IInventory
	{
		Dictionary<int, Item> inventory;
		// public Item[]  inventory;
		int     maxSize;
		// Item        lastItem = null;  // cacheing to save few searching


		// public Inventory() => Init(null, Configs.Configs.warehouseSize); // test
		public Inventory(Dictionary<int, Item> itemDict, int maxSize) => Init(itemDict, maxSize);
		public Inventory(Inventory inventory) => Init(inventory.inventory, inventory.maxSize);


		public void SetMaxSize(int size) => maxSize = size;

        private void Init(Dictionary<int, Item> itemDict, int maxSize)
        {
			InitInventory(maxSize);
			if (!itemDict?.Count.Equals(0) ?? false)
				Copy(itemDict);
        }


		private void InitInventory(int maxSize)
		{
			this.maxSize = maxSize;
			inventory = new Dictionary<int, Item>();
		}


		private bool HaveNewSpace => inventory.Count < maxSize;
		private void Copy(Dictionary<int, Item> itemDict) => inventory = new Dictionary<int, Item>(itemDict);
		private void Merge(Dictionary<int, Item> itemDict)
		{
			foreach(KeyValuePair<int, Item> item in itemDict)
			{
				_ = inventory.ContainsKey(item.Key) ? false : AddItem(item.Value);
			}
		}


		public void EraseDummySlot()
		{
			for (int index = 0; index < maxSize; ++index)
			{
				if (inventory[index] != null && !inventory[index].IsExist)
				{
					inventory[index] = null;
				}
			}
		}


		private bool AddNewItem(Item item)
		{
			if (!inventory.ContainsKey(item.uid) && HaveNewSpace)
			{
				inventory.Add(item.uid, item);
				return true;
			}
			return false;
		}


		private bool AddStackableItem(Item item)
		{
			var pick = from element in inventory
					   where (element.Value.HaveSpace(item.name) == true)
					   select element;

			foreach (var value in pick)
			{
				if (((ToolItem)value.Value).AddStack(item)) return true;
			}

			while (HaveNewSpace)
			{
				Item newSpace = (ToolItem)item.DuplicateEmptyNew();
				bool result = ((ToolItem)newSpace).AddStack(item);
				inventory.Add(newSpace.uid, newSpace);

				if (result) return true;
			}

			return false;
		}


		public bool AddItem(Item item)
		{
			if (!Libs.IsExist(item)) return false;
			return item.category.Equals(ItemCat.tool)
				? AddStackableItem(item)
				: AddNewItem(item);
		}


        public Item PickItem(int UID) => inventory[UID];


        private void CheckEmpty(Item item)
		{
			if (Libs.IsSoldOut(item)) PopItemForce(item.uid);
		}


		public bool UseItem(int UID)
		{
			if (inventory.TryGetValue(UID, out Item item))
			{
				item.Use();
				CheckEmpty(item);
				return true;
			}
			return false;
		}

		public bool UseItem (Item item) => item.Use();

		public bool PopItem(int UID) => inventory.ContainsKey(UID) ? inventory.Remove(UID) : false;
		public bool PopItemForce(int UID) => inventory.Remove(UID);


		public void DEBUG_ShowAllItems()
		{
			Debug.Log("----- show all items start -----");

			foreach (KeyValuePair<int, Item> item in inventory)
			{
				Debug.Log("UID = " + item.Value.uid + ", name = " + item.Value.name + ", amt = " + item.Value.UseableCount + ", w = " + item.Value.Weight() + ", v = " + item.Value.Volume());
			}
			Debug.Log("----- show all items end -----");
		}
	}
}
