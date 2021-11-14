using UnityEngine;
using System.Collections;
using Popup.Utils;
using Popup.Defines;
using System;
using System.Data;





namespace Popup.Items
{
	using Cfg = Configs.Configs;
	using ServerJob = ServerJob.ServerJob;
	[Serializable]
	public class Item
	{
		[SerializeField]
		string		name;
        [SerializeField]
        int uid;
        [SerializeField]
		float		weight;
		[SerializeField]
		float		volume;
		[SerializeField]
		int			amount;
		int			maxAmount = int.MaxValue;
		[SerializeField]
		Grade		grade;
		[SerializeField]
		int[]		magicIdArray;
		[SerializeField]
		ItemCat		category;
		[SerializeField]
		int			durability;



		public 	string 	GetName 		=> name;
		public 	Grade 	GetGrade 		=> grade;
		public 	int 	GetUID 			=> uid;
        public 	int 	GetSpellAmount 	=> magicIdArray == null ? 0 : magicIdArray.Length;
		public 	float 	GetWeight 		=> weight * amount;
		public 	float 	GetVolume 		=> volume * amount;
		public 	int 	GetAmount 		=> amount;
		public 	int 	GetMaxAmount 	=> maxAmount;
		public 	ItemCat GetCat 			=> category;
		public	int		GetDurability	=> durability;
		public 	bool 	IsStackable 	=> 0 < (category & ItemCat.stackable);

		private	int 	GetSpace 		=> maxAmount - amount;

/* test func start */
		public void SetCat(ItemCat cat) => category = cat; // test
		public void SetName(string name) => this.name = name; // test
		public void SetAmount(int amount) => this.amount = amount; // test
		public void SetUID(int UID) => uid = UID;   // test

        public void SetWeight(float weight)                 // test
		{
			this.weight = weight;

			if (HaveAttribute(ItemCat.stackable))
			{
				int limit = Libs.Round(Cfg.slotWeightCapacity / weight);
				maxAmount = Math.Min(maxAmount, limit);
			}
		}

		public void SetVolume(float volume)                 // test
		{
			this.volume = volume;

			if (HaveAttribute(ItemCat.stackable))
			{
				int limit = Libs.Round(Cfg.slotVolumeCapacity / volume);
				maxAmount = Math.Min(maxAmount, limit);
			}
		}
/* test func end */


		public Item(int uid) => this.uid = uid;

        //Item() // fix
        //{

        //}

        //public bool Compare(string name)    => this.name == name;

        // public 	bool CompareUID		(int uid) 			=> this.uid == uid;
        public 	void DecreaseAmount	(int count = 1)		=> amount -= count;
		private bool HaveAttribute	(ItemCat attribute) => 0 < (category & attribute);
		public	bool IsExist							=> 0 < durability && 0 < amount;


		public bool HasSpace()
		{
			if (HaveAttribute(ItemCat.stackable))
			{
				return amount < maxAmount;
			}

			return false;
		}



		public (bool, int) GetSpell(int index)
		{
			if (0 < (category & ItemCat.hasSpell))
			{
				Guard.InRange(index, ref magicIdArray);
				return (true, magicIdArray[index]);
			}
			return (false, 0);
		}



		public bool AddStack(ref Item item)
		{
			int enableCount = Math.Min(item.amount, GetSpace);

			amount += enableCount;
			item.DecreaseAmount(enableCount);

			return item.amount.Equals(0);
		}



		public Item Clone()
		{
			Item other = (Item)MemberwiseClone();
			other.SetAmount(0);
			other.SetUID(ServerJob.RequestNewUID);
            return other;
		}


		//public static Delegate.ConvertFromString<Item> convert = new Delegate.ConvertFromString<Item>(DataConvert);


		//private static Item DataConvert(IDataReader data)
		//{
		//	//Item item = new Item(data.GetInt32(0));
		//	Item item = new Item();

		//	item.name = data.GetString(1);
		//	item.weight = data.GetFloat(2);
		//	item.volume = data.GetFloat(3);
		//	item.amount = data.GetInt32(4);
		//	item.grade = (Grade)data.GetInt32(5);
		//	item.magicIdArray = Libs.TextToIntArray(data.GetString(6));
		//	item.category = (ItemCat)data.GetInt32(7);
		//	item.durability = data.GetInt32(8);

		//	return item;
		//}
	}





	// public class EquipItem : Item
	// {
	// 	int durability;

	// 	public int GetDurability => durability;

	// 	EquipItem(int uid) => this.uid = uid;

	// 	public override object Clone()
	// 	{
	// 		EquipItem other = (EquipItem)MemberwiseClone();
	// 		other.SetAmount(0);
	// 		other.SetUID(Libs.RequestNewUID);
	// 		return other;
	// 	}
	// }





	// public class ToolItem : Item
	// {

	// 	ToolItem(int uid) => this.uid = uid;

	// 	public override object Clone()
	// 	{
	// 		ToolItem other = (ToolItem)MemberwiseClone();
	// 		other.SetAmount(0);
	// 		other.SetUID(Libs.RequestNewUID);
	// 		return other;
	// 	}

	// }


}


