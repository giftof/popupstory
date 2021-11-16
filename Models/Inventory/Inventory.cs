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
			(int matchSlotIndex, int emptySlotIndex) result = (0, maxSize);
			Item item;

			for (; result.matchSlotIndex < maxSize; ++result.matchSlotIndex)
			{
				item = inventory[result.matchSlotIndex];

				if (item == null)
				{
					result.emptySlotIndex = Math.Min(result.matchSlotIndex, result.emptySlotIndex);
				}
				else if (item.GetUID().Equals(uid) && Libs.IsEnablePair(mustHaveSpace, item.HasSpace()))
				{
					result.emptySlotIndex = Libs.FindEmptyIndex(ref inventory, result.matchSlotIndex);
					break;
				}
			}

			return result;
		}


		private bool AddNewItem(ref Item item)
		{
			(int matchSlotIndex, int emptySlotIndex) = Search(item.GetUID(), false);

			Guard.MustNotInclude(matchSlotIndex, maxSize, "[AddNewItem in inventory]");

			if (Libs.IsInclude(emptySlotIndex, maxSize))
			{
				inventory[emptySlotIndex] = item;
				return true;
			}

			return false;
		}


		private bool AddStackableItem(ref Item item)
		{
			(int matchSlotIndex, int emptySlotIndex) = Search(item.GetUID(), true);

			if (matchSlotIndex.Equals(emptySlotIndex) && emptySlotIndex.Equals(maxSize))
			{
				return false;
			}

			if (Libs.IsInclude(matchSlotIndex, maxSize) && ((ToolItem)inventory[matchSlotIndex]).AddStack(ref item))
			{
				return true;
			}

			if (Libs.IsInclude(emptySlotIndex, maxSize))
			{
				inventory[emptySlotIndex] = (ToolItem)item.DuplicateEmptyNew();
				((ToolItem)inventory[emptySlotIndex]).AddStack(ref item);
			}

			if (0 < item.GetLeftOver())
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
            (int matchSlotIndex, _) = Search(UID, false);

            if (matchSlotIndex < maxSize)
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


		public bool ExhaustItem(int UID)
		{
			(int matchSlotIndex, _) = Search(UID, false);

			if (Libs.IsInclude(matchSlotIndex, maxSize))
			{
				if (CheckEmptySlot(ref inventory[matchSlotIndex])) 
				{
					return false;
				}
				inventory[matchSlotIndex].Exhaust();
				CheckEmptySlot(ref inventory[matchSlotIndex]);
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
					DebugC.Log("[here is empty slot]", Color.yellow);
				}
			}
			Debug.Log("----- show all items end -----");
		}
	}
}
