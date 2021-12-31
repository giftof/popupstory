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
	public abstract partial class Inventory /*: IInventory*/
    {
		public uint MaxSize { get; protected set; }

		public Inventory(uint maxSize)
        {
			MaxSize = maxSize;
			InitializeInventory(maxSize);
        }

		public bool Add(Item item)
		{
			if (Libs.IsExhaust(item)) return false;
			return item.HaveAttribute(ItemCat.stackable)
				? AddStackable(item as StackableItem)
				: AddNew(item);
		}
	}

	public partial class WareHouse : Inventory
	{
		Dictionary<int, Item> wareHouse;

		public WareHouse(uint maxSize) : base(maxSize) { }
    }





	public abstract partial class Inventory
	{
		protected abstract void InitializeInventory(uint maxSize);
		protected abstract bool AddStackable(StackableItem item);
		protected abstract bool AddNew(Item item);
		protected abstract bool HaveSpace();
		public abstract Dictionary<int, Item> Source { get; }
		public abstract void EraseExhaustedSlot();
		public abstract bool Use(Item item);
		public abstract void Insert(Item item);
		public abstract void Remove(Item item);
		public abstract List<Item> UnslotedList();
		public abstract Item[] PopAll();
	}

	public partial class WareHouse
    {
		protected override void InitializeInventory(uint _) => wareHouse = new Dictionary<int, Item>();

		protected override bool AddStackable(StackableItem item)
		{
			var stackableList = wareHouse
								.Where(e => e.Value.Category.Equals(item.Category) && e.Value.HaveSpace(item.Name))
								.OrderBy(e => e.Value.SlotId);

			foreach (var pair in stackableList)
			{
				if (((StackableItem)pair.Value).AddStack(item)) return true;
			}

			while (item.IsExist && HaveSpace())
			{
				StackableItem newItem = (StackableItem)item.DeepCopy(Manager.Instance.network.REQ_NEW_ID(), null);
				newItem.AddStack(item);
				wareHouse.Add(newItem.Uid, newItem);
			}

			// DEBUG_ShowAllItems();
			return !item.IsExist;
		}

		protected override bool AddNew(Item item)
		{
			Guard.MustNotInclude(item.Uid, wareHouse, "[AddNew - in WareHouse]");

			if (wareHouse.Count < MaxSize)
			{
				wareHouse.Add(item.Uid, item);

				// DEBUG_ShowAllItems();
				return true;
			}

			// DEBUG_ShowAllItems();
			return false;
		}

		protected override bool HaveSpace() => wareHouse.Count < MaxSize;

		public override Dictionary<int, Item> Source => wareHouse;

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
			Item use = wareHouse.FirstOrDefault(e => e.Value.Uid.Equals(item.Uid)).Value;

			if (use?.Use() ?? false)
			{
				if (!use.IsExist)
					wareHouse.Remove(use.Uid);
				return true;
			}
			return false;
		}

		public override void Insert(Item item)
		{
			if (!wareHouse.ContainsKey(item.Uid))
				wareHouse.Add(item.Uid, item);
		}

		public override void Remove(Item item) => wareHouse.Remove(item.Uid);

		public override List<Item> UnslotedList() => (from pair in wareHouse
													  where pair.Value.SlotId.Equals(Config.unSlot)
													  select pair.Value).ToList();

		public override Item[] PopAll()
		{
			Item[] array = wareHouse.Values.ToArray();
			wareHouse.Clear();

			return array;
		}
	}





	public abstract partial class Inventory
	{
		public abstract void DEBUG_ShowAllItems();
	}

	public partial class WareHouse
	{
		public override void DEBUG_ShowAllItems()
		{
			List<int> keys = wareHouse.Keys.ToList();
			foreach (int key in keys)
			{
				Item item = wareHouse[key];
				Debug.Log("UID = " + item.Uid + ", name = " + item.Name + ", slotId = " + item.SlotId + ", amt = " + item.UseableCount + ", w = " + item.TWeight() + ", v = " + item.TVolume());
			}
		}
	}
}
