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
	using ServerJob = ServerJob.ServerJob;
	public abstract class Inventory : IInventory
    {
		protected int maxSize;

		public Inventory(int maxSize)
        {
			this.maxSize = maxSize;
			InitializeInventory(maxSize);
        }

		public bool Add(Item item)
		{
			if (Libs.IsExhaust(item)) return false;
			return item.HaveAttribute(ItemCat.tool)
				? AddStackable(item)
				: AddNew(item);
		}

		protected abstract void InitializeInventory(int maxSize);
		protected abstract bool AddStackable(Item item);
		protected abstract bool AddNew(Item item);
		protected abstract bool HaveSpace();
		public abstract Dictionary<int, Item> Source { get; }
		public abstract void EraseExhaustedSlot();
		public abstract bool Use(Item item);
		public abstract void Insert(Item item);
		public abstract void Remove(Item item);
		public abstract List<Item> UnSlotedList();
        public abstract void DEBUG_ShowAllItems();
		public abstract Item[] PopAll();
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

				DEBUG_ShowAllItems();
				return true;
			}

			DEBUG_ShowAllItems();
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

			DEBUG_ShowAllItems();
			return !item.IsExist;
		}

		public override Dictionary<int, Item> Source => wareHouse;

		public override void Insert(Item item)
		{
			if (!wareHouse.ContainsKey(item.uid))
				wareHouse.Add(item.uid, item);
		}
		public override void Remove(Item item) => wareHouse.Remove(item.uid);

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

		public override List<Item> UnSlotedList() => (from pair in wareHouse
													  where pair.Value.SlotId.Equals(Config.unSlot)
													  select pair.Value).ToList();

		public override Item[] PopAll()
        {
			Item[] array = wareHouse.Values.ToArray();
			wareHouse.Clear();

			return array;
        }

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
