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
	//using ServerJob = ServerJob.ServerJob;
	//public class Inventory : IInventory
	//{
	//	Dictionary<int, Item> inventory;
	//	int[] usedSlot;
	//	int useableCount;


	//	public Inventory(int maxSize)
	//	{
	//		InitInventory();
	//		InitUsedSlot(maxSize);
	//	}


	//	private void InitInventory() => inventory = new Dictionary<int, Item>();

	//	private void InitUsedSlot(int maxSize)
	//	{
	//		usedSlot = new int[maxSize];
	//		useableCount = maxSize;
	//	}

	//	private bool HaveNewSpace => 0 < useableCount;
	//	private bool IsExhaust(Item item) => Libs.IsExhaust(item) && (PopForce(item.uid) == item);
	//	private void Copy(SortedDictionary<int, Item> itemDict) => inventory = new Dictionary<int, Item>(itemDict);
	//	private void Merge(Inventory inventory) => Merge(inventory.inventory);
	//	private void Merge(Dictionary<int, Item> itemDict)
	//	{
	//		foreach(KeyValuePair<int, Item> item in itemDict)
	//		{
	//			_ = inventory.ContainsKey(item.Key) ? false : Add(item.Value);
	//		}
	//	}

	//	private bool UpdateUseableCount(int flag) => (useableCount += flag == 0 ? 1 : -1) != -1;
	//	private bool ReverseFlag(int index) => UpdateUseableCount(usedSlot[index] = usedSlot[index] == 1 ? 0 : 1);

	//	private int CandidateId()
	//	{
	//		return usedSlot
	//				.Select((flag, index) => (flag, index))
	//				.FirstOrDefault(t => (t.flag == 0 && ReverseFlag(t.index)) || ++t.index < 0).index;
	//	}

	//	private bool AddNew(Item item)
	//	{
	//		if (!inventory.ContainsKey(item.uid) && HaveNewSpace)
	//		{
	//			inventory.Add(item.uid, item);
	//			item.slotId = CandidateId();
	//			return true;
	//		}
	//		return false;
	//	}

	//	private bool AddStackable(Item item)
	//	{
	//		var pick = from element in inventory
	//				   where element.Value.HaveSpace(item.Name)
	//				   select element;

	//		foreach (var value in pick)
	//		{
	//			if (((ToolItem)value.Value).AddStack(item))
	//				return true;
	//		}

	//		while (HaveNewSpace)
	//		{
	//			Item newSpace = (ToolItem)item.DeepCopy(ServerJob.RequestNewUID);
	//			newSpace.slotId = CandidateId();
	//			inventory.Add(newSpace.uid, newSpace);
	//			if (((ToolItem)newSpace).AddStack(item))
	//				return true;
	//		}

	//		return false;
	//	}


	//	public bool Add(Item item)
	//	{
	//		if (!Libs.IsExist(item)) return false;
	//		return item.Category.Equals(ItemCat.tool)
	//			? AddStackable(item)
	//			: AddNew(item);
	//	}


	//	public void Swap(Item item1, Item item2)
	//	{
	//		int tempSlotId = item1.slotId;

	//		item1.slotId = item2.slotId;
	//		item2.slotId = tempSlotId;
	//	}


	//	public void EraseExhaustedSlot()
	//	{
	//		var removeList = from pair in inventory
	//				   where !pair.Value.IsExist
	//				   select pair;

	//		foreach (KeyValuePair<int, Item> pair in removeList)
	//		{
	//			inventory.Remove(pair.Key);
	//			ReverseFlag(pair.Value.slotId);
	//		}
	//	}


	//       public Item Pick(int UID) => inventory[UID];


	//	public bool Use(Item item) => Use(item.uid);
	//	public bool Use(int UID)
	//	{
	//		if (inventory.TryGetValue(UID, out Item item))
	//		{
	//			item.Use();
	//			IsExhaust(item);
	//			return true;
	//		}
	//		return false;
	//	}


	//	public Item Pop(int UID) => inventory.ContainsKey(UID) ? PopForce(UID) : null;
	//	public Item PopForce(int UID)
	//	{
	//		Item item = inventory[UID];
	//		inventory.Remove(item.uid);
	//		ReverseFlag(item.slotId);
	//		return item;
	//	}






	//	public void DEBUG_ShowAllItems()
	//	{
	//		Debug.Log("----- show all items start -----");
	//		int index = 0;

	//		foreach (KeyValuePair<int, Item> item in inventory)
	//		{
	//			while (index++ < item.Value.slotId)
	//			{
	//				DebugC.Log($"[Empty Slot: {index - 1}]", Color.green);
	//			}

	//			Debug.Log("UID = " + item.Value.uid + ", name = " + item.Value.Name + ", slotId = " + item.Value.slotId + ", amt = " + item.Value.UseableCount + ", w = " + item.Value.TWeight() + ", v = " + item.Value.TVolume());
	//		}

	//		while (index++ < usedSlot.Length)
	//		{
	//			DebugC.Log($"[Empty Slot: {index - 1}]", Color.green);
	//		}
	//		Debug.Log("----- show all items end -----");
	//	}
	//}

	using ServerJob = ServerJob.ServerJob;
	public abstract class Inventory : IInventory
    {
		protected int maxSize;

		public Inventory(int maxSize)
        {
			this.maxSize = maxSize;
			InitializeInventory(maxSize);
        }

		protected abstract void InitializeInventory(int maxSize);
		protected abstract bool AddStackable(Item item);
		protected abstract bool AddNew(Item item);
		protected abstract bool HaveSpace();
		public abstract void EraseExhaustedSlot();
		//public abstract bool Use(int slotId);
		public abstract bool Use(Item item);


        public bool Add(Item item)
        {
            if (Libs.IsExhaust(item)) return false;
            return item.Category.Equals(ItemCat.tool)
                ? AddStackable(item)
                : AddNew(item);
        }



        public abstract void DEBUG_ShowAllItems();
	}



	public class WareHouse : Inventory
	{
		Dictionary<int, Item> wareHouse;

		public WareHouse(int maxSize) : base(maxSize) { }

		protected override void InitializeInventory(int _) => wareHouse = new Dictionary<int, Item>();
		protected override bool HaveSpace() => wareHouse.Count < maxSize;

		protected override bool AddNew(Item item)
        {
			Guard.MustNotInclude(item.uid, wareHouse, "[AddNew - in WareHouse]");
			if (wareHouse.Count < maxSize)
            {
				wareHouse.Add(item.uid, item);
				return true;
            }
			return false;
        }

		protected override bool AddStackable(Item item)
        {
			var stackableList = wareHouse
								.Where(e => e.Value.Category.Equals(item.Category) && e.Value.HaveSpace(item.Name))
								.OrderBy(e => e.Value.SlotId);

			foreach (var pair in stackableList)
            {
				if (((ToolItem)pair.Value).AddStack(item)) return true;
            }

			while (item.IsExist && HaveSpace())
            {
				ToolItem newItem = (ToolItem)item.DeepCopy(ServerJob.RequestNewUID, null);
				newItem.AddStack(item);
				wareHouse.Add(newItem.uid, newItem);
            }

			return !item.IsExist;
        }

		public override void EraseExhaustedSlot()
		{
			KeyValuePair<int, Item>[] empty = wareHouse.Where(e => !e.Value.IsExist).ToArray();

			for (int i = 0; i < empty.Length; ++i)
			{
				wareHouse.Remove(empty[i].Key);
			}
		}

		public override bool Use(Item item)
		{
			Item use = wareHouse.FirstOrDefault(e => e.Value.uid.Equals(item.uid)).Value;

			if (use?.Use() ?? false)
			{
				if (!use.IsExist)
					wareHouse.Remove(use.uid);
				return true;
			}
			return false;
		}

		//public override bool Use(int slotId)
  //      {
		//	Item use = wareHouse.FirstOrDefault(e => e.Value.SlotId.Equals(slotId) && e.Value.IsExist).Value;

		//	if (use?.Use() ?? false)
  //          {
		//		if (!use.IsExist)
		//			wareHouse.Remove(use.uid);
		//	}
		//	return false;
  //      }



		public override void DEBUG_ShowAllItems()
        {
			List<int> keys = wareHouse.Keys.ToList();
			foreach (int key in keys)
            {
				Item item = wareHouse[key];
				Debug.Log("UID = " + item.uid + ", name = " + item.Name + ", slotId = " + item.SlotId + ", amt = " + item.UseableCount + ", w = " + item.TWeight() + ", v = " + item.TVolume());
			}
		}
    }



	//public class Pouch : Inventory
	//{
	//    Item[] pouch;

	//    public Pouch(int maxSize) : base(maxSize) { }

	//    protected override void InitializeInventory(int maxSize) => pouch = new Item[maxSize];
	//    protected override bool AddStackable(Item item) => false;
	//    protected override bool AddNew(Item item) => false;

	//    private int FirstEmptySlot() => pouch.Select((e, i) => (e, i)).FirstOrDefault(t => t.e == null || ++t.i < 0).i;
	//}



	//public class BackPack : Inventory
	//{
	//    Item[] backPack;

	//    public BackPack(int maxSize) : base(maxSize) { }

	//    protected override void InitializeInventory(int maxSize) => backPack = new Item[maxSize];
	//    protected override bool AddStackable(Item item) => false;
	//    protected override bool AddNew(Item item) => false;

	//    private int FirstEmptySlot() => backPack.Select((e, i) => (e, i)).FirstOrDefault(t => t.e == null || ++t.i < 0).i;
	//}
}
