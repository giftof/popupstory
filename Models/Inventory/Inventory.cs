using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
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
		Item[]  inventory;
		int     maxSize;
		// Item        lastItem = null;  // cacheing to save few searching


		public Inventory(ref Item[] itemArray, int maxSize) => Init(ref itemArray, maxSize);
		public Inventory(	 Item[] itemArray, int maxSize) => Init(ref itemArray, maxSize);
		public Inventory(ref Inventory inventory)           => Init(ref inventory.inventory, inventory.maxSize);


		public void SetMaxSize  (int size)		=> maxSize = size;


        private void Init(ref Item[] itemArray, int maxSize)
        {
			InitInventory(maxSize);
			if (itemArray != null)
				Insert(ref itemArray);
        }


		private void InitInventory(int maxSize)
		{
			this.maxSize = maxSize;
			inventory = new Item[maxSize];
		}


		private void Insert(ref Item[] item)
		{
			int index = 0;

			foreach (Item element in item)
			{
				index = Libs.FindEmptyIndex(ref inventory, index);
				
				if (!Libs.IsInclude(index, maxSize))
				{
					break;
				}

				if (element != null && element.IsExist())
				{
					inventory[index++] = element;
				}
			}
		}


		public void EraseDummySlot()
		{
			for (int index = 0; index < maxSize; ++index)
			{
				if (inventory[index] != null && !inventory[index].IsExist())
				{
					inventory[index] = null;
				}
			}
		}


		private (int, int) Search(int uid, bool mustHaveSpace = true)
		{
			(int, int) 	result = (0, maxSize);
			Item 		item;

			for (; result.Item1 < maxSize; ++result.Item1)
			{
				item = inventory[result.Item1];

				if (item == null)
				{
					result.Item2 = Math.Min(result.Item1, result.Item2);
				}
				else if (item.GetUID().Equals(uid) && Libs.IsEnablePair(mustHaveSpace, item.HasSpace()))
				{
					result.Item2 = Libs.FindEmptyIndex(ref inventory, result.Item1);
					break;
				}
			}

			return result;
		}


		private bool AddNewItem(ref Item item)
		{
			(int, int) result = Search(item.GetUID(), false);

			Guard.MustNotInclude(result.Item1, maxSize, "[AddNewItem in inventory]");

			if (Libs.IsInclude(result.Item2, maxSize))
			{
				inventory[result.Item2] = item;
				return true;
			}

			return false;
		}


		private bool AddStackableItem(ref Item item)
		{
			(int, int) result = Search(item.GetUID(), true);

			if (result.Item1.Equals(result.Item2) && result.Item2.Equals(maxSize))
			{
				return false;
			}

			if (Libs.IsInclude(result.Item1, maxSize) && ((ToolItem)inventory[result.Item1]).AddStack(ref item))
			{
				return true;
			}

			if (Libs.IsInclude(result.Item2, maxSize))
			{
				inventory[result.Item2] = (ToolItem)item.Clone();
				((ToolItem)inventory[result.Item2]).AddStack(ref item);
			}

			if (0 < ((ToolItem)item).GetAmount)
			{
				return AddStackableItem(ref item);
			}

			return true;
		}


		public bool AddItem(ref Item item)
		{
			if (item.GetLeftOver() == 0)
			{
				return false;
			}
			
			if (item.GetCategory() == ItemCat.tool)
			{
				return AddStackableItem(ref item);
			}

			return AddNewItem(ref item);
		}


        public Item PickItem(int UID)
        {
            (int, int) result = Search(UID, false);

            if (result.Item1 < maxSize)
            {
                return Guard.MustConvertTo<Item>(inventory[UID], "[PickItem in inventory]");
            }

            throw new NotImplementedException();
        }


        private bool CheckEmptySlot(ref Item item)
		{
			if (!item.IsExist())
			{
				item = null;
				return true;
			}
			return false;
		}


		// public bool UseItem(int UID)
		public bool ExhaustItem(int UID)
		{
			(int, int) result = Search(UID, false);

			if (Libs.IsInclude(result.Item1, maxSize))
			{
				if (CheckEmptySlot(ref inventory[result.Item1])) 
				{
					return false;
				}
				inventory[result.Item1].Exhaust();
				CheckEmptySlot(ref inventory[result.Item1]);
				return true;
			}
			return false;
		}

		public bool ExhaustItem (ref Item item) => item.Exhaust();

		public bool PopItem(int UID)
		{
			Guard.MustInclude(UID, ref inventory, "[PopItem in inventory]") = null;
			return true;
		}


		public void DEBUG_ShowAllItems()
		{
			Debug.Log("----- show all items start -----");

			foreach (Item item in inventory)
			{
				if (item != null)
				{
					Debug.Log("UID = " + item.GetUID() + ", name = " + item.GetName() + ", amt = " + item.GetLeftOver() + ", w = " + item.GetWeight() + ", v = " + item.GetVolume());
				}
				else
				{
					Debug.Log("here is empty slot");
				}
			}
			Debug.Log("----- show all items end -----");
		}
	}
}
