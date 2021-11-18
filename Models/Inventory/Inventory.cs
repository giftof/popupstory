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
		List<int> candidate;

		public Inventory(int maxSize)
		{
			InitInventory();
			InitCandidate(maxSize);
		}

		private void InitInventory() => inventory = new Dictionary<int, Item>();

		private void InitCandidate(int maxSize)
		{
			candidate = new List<int>();
			for (int i = 0; i < maxSize; ++i) candidate.Add(i);
		}

		private bool HaveNewSpace => 0 < candidate.Count;
        private bool IsExhaust(Item item) => (Libs.IsExhaust(item)) ? (PopForce(item.uid) == item) : false;
		private void Copy(SortedDictionary<int, Item> itemDict) => inventory = new Dictionary<int, Item>(itemDict);
		private void Merge(Inventory inventory) => Merge(inventory.inventory);
		private void Merge(Dictionary<int, Item> itemDict)
		{
			foreach(KeyValuePair<int, Item> item in itemDict)
			{
				_ = inventory.ContainsKey(item.Key) ? false : Add(item.Value);
			}
		}

		private int CandidateId() //  => candidate.FirstOrDefault(e => true);
		{
			int id = candidate.ElementAt(0);
			candidate.RemoveAt(0);
			return id;
		}

		private void CandidateAppend(int id)
		{
			int index = candidate
						.Select((value, index) => (value, index))
						.FirstOrDefault(pair=> id < pair.value && 0 < ++pair.index).index;

			index = (0 < candidate.Count && index == 0) ? candidate.Count: index;
			candidate.Insert(index, id);
		}

		private bool AddNew(Item item)
		{
			if (!inventory.ContainsKey(item.uid) && HaveNewSpace)
			{
				inventory.Add(item.uid, item);
				item.slotId = CandidateId();
				return true;
			}
			return false;
		}

		private bool AddStackable(Item item)
		{
			var pick = from element in inventory
					   where element.Value.HaveSpace(item.name)
					   select element;

			foreach (var value in pick)
			{
				if (((ToolItem)value.Value).AddStack(item))
					return true;
			}

			while (HaveNewSpace)
			{
				Item newSpace = (ToolItem)item.DuplicateEmptyNew();
				inventory.Add(newSpace.uid, newSpace);
				if (((ToolItem)newSpace).AddStack(item))
					return true;
			}

			return false;
		}


		public bool Add(Item item)
		{
			if (!Libs.IsExist(item)) return false;
			return item.category.Equals(ItemCat.tool)
				? AddStackable(item)
				: AddNew(item);
		}


		public void EraseExhaustedSlot()
		{
			var removeList = from pair in inventory
					   where !pair.Value.IsExist
					   select pair;

			foreach (KeyValuePair<int, Item> pair in removeList)
			{
				inventory.Remove(pair.Key);
				CandidateAppend(pair.Value.slotId);
			}
		}


        public Item Pick(int UID) => inventory[UID];


		public bool Use(Item item) => Use(item.uid);
		public bool Use(int UID)
		{
			if (inventory.TryGetValue(UID, out Item item))
			{
				item.Use();
				IsExhaust(item);
				return true;
			}
			return false;
		}


		public Item Pop(int UID) => inventory.ContainsKey(UID) ? PopForce(UID) : null;
		public Item PopForce(int UID)
		{
			Item item = inventory[UID];
			inventory.Remove(item.uid);
			CandidateAppend(item.slotId);
			return item;
		}


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
