using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Popup.Configs;
using Popup.Items;
using Popup.Library;
using Popup.Delegate;
using Popup.Framework;





namespace Popup.Inventory
{
	// public interface ICompare
	// {
	// 	(int, int) TestSearch<T>(T value);
	// }

	//public interface IInventory
	//{
	//	//ModelBase PickItem(int uid);
	//	Item PickItem	(int uid);
	//	bool UseItem    (int uid);
	//	bool UseItem    (Item item);
	//	bool PopItem    (int uid);
	//	bool AddItem    (ref Item item);
	//	void SetMaxSize (int size);

	//}

	public class Inventory : IInventory
	{
		Item[]  inventory;
		int     maxSize;
		// Item        lastItem = null;  // cacheing to save few searching


		public Inventory(ref Item[] itemArray, int maxSize) => Init(ref itemArray, maxSize);
		public Inventory(Item[]     itemArray, int maxSize) => Init(ref itemArray, maxSize);
		public Inventory(ref Inventory inventory)           => Init(ref inventory.inventory, inventory.maxSize);


		public bool UseItem     (ref Item item) => UseItem(item.GetUID());
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
				//index = GetEmptySlot(index);
				if (!Libs.IsInclude(index, maxSize)) break;
				//if (maxSize <= index) break;
				if (element != null && element.IsExist)
				{
					inventory[index++] = element;
				}
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


		//private int GetEmptySlot(int fromIndex = 0)
		//{
		//	int index = maxSize;

		//	if (fromIndex.Equals(index)) return index;

		//	for (; fromIndex < maxSize; ++fromIndex)
		//	{
		//		if (inventory[fromIndex] == null)
		//		{
		//			index = fromIndex;
		//			break;
		//		}
		//	}

		//	return index;
		//}


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
					//result.Item2 = GetEmptySlot(result.Item1);
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

			if (Libs.IsInclude(result.Item1, maxSize) && inventory[result.Item1].AddStack(ref item))
			{
				return true;
			}

			if (Libs.IsInclude(result.Item2, maxSize))
			{
				inventory[result.Item2] = (Item)item.Clone();
				inventory[result.Item2].AddStack(ref item);
			}

			if (0 < item.GetAmount)
			{
				return AddStackableItem(ref item);
			}

			return true;
		}


		public bool AddItem(ref Item item)
		{
			if (item.IsStackable)
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
			if (!item.IsExist)
			{
				item = null;
				return true;
			}
			return false;
		}


		public bool UseItem(int UID)
		{
			(int, int) result = Search(UID, false);

			if (Libs.IsInclude(result.Item1, maxSize))
			{
				if (CheckEmptySlot(ref inventory[result.Item1])) 
				{
					return false;
				}
				inventory[result.Item1].DecreaseAmount();
				CheckEmptySlot(ref inventory[result.Item1]);
				return true;
			}
			return false;
		}


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
					Debug.Log("UID = " + item.GetUID() + ", name = " + item.GetName + ", amt = " + item.GetAmount + ", w = " + item.GetWeight + ", v = " + item.GetVolume);
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
