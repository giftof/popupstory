using UnityEngine;
using System.Collections;
using Popup.Library;
using Popup.Defines;
using Popup.Framework;
using System;
using System.Data;



namespace Popup.Items
{
	using Cfg = Configs.Configs;
	using ServerJob = ServerJob.ServerJob;
	[Serializable]
	public abstract class Item : IItem
	{
		[SerializeField]
		protected string   name;
        [SerializeField]
		protected int 	   uid;
        [SerializeField]
		protected float	   weight;
		[SerializeField]
		protected float	   volume;
		[SerializeField]
		protected ItemCat  category;
		[SerializeField]
		protected int	   amount;
		[SerializeField]
		protected int	   maxAmount = int.MaxValue;


        public 	int 	GetUID       () => uid;
		public 	string 	GetName      () => name;
		public 	float 	GetWeight    ()	=> 0;
		public 	float 	GetVolume    ()	=> 0;
		public  bool	IsExist      ()	=> 0 < amount;
		public  int 	GetLeftOver  ()	=> amount;
		public  bool 	Use			 ()	=> 0 < amount--;
		public  ItemCat GetCategory  () => category;


/* test func start */
		public void SetCat	  (ItemCat cat )   => category    = cat;
		public void SetName	  (string  name)   => this.name   = name;
		public void SetUID	  (int 	   UID )   => uid         = UID;
		public void SetAmount (int     amount) => this.amount = amount;
		public void SetWeight (float   weight)
		{
			this.weight = weight;
			int limit   = Libs.Round(Cfg.slotWeightCapacity / weight);
			maxAmount   = Math.Min(maxAmount, limit);
		}
		public void SetVolume (float   volume)
		{
			this.volume = volume;
			int limit   = Libs.Round(Cfg.slotVolumeCapacity / volume);
			maxAmount   = Math.Min(maxAmount, limit);
		}
/* test func end */


		public Item(int uid) => this.uid = uid;


		protected bool HaveAttribute	(ItemCat attribute) => 0 < (category & attribute);
		public 	abstract bool	HasSpace();


		public abstract object Duplicate();
		public abstract object DuplicateNew();
		public abstract object DuplicateEmpty();
		public abstract object DuplicateEmptyNew();
	}





	public class EquipItem : Item
	{
		[SerializeField]
		Grade		grade;
		[SerializeField]
		Spell[]		spellArray;


		public EquipItem(int uid) : base(uid)   => category = ItemCat.equip;


		public override bool HasSpace() => false;


		public  Grade GetGrade 		    => grade;
		public 	int   GetSpellAmount    => spellArray == null ? 0 : spellArray.Length;
		public  Spell GetSpell(int uid) => Guard.MustInclude(uid, ref spellArray, "[GetSpell in EquipItem]");


		public override object Duplicate() => MemberwiseClone();
		public override object DuplicateNew()
        {
			EquipItem other = (EquipItem)Duplicate();
			other.SetUID(ServerJob.RequestNewUID);
			return other;
        }
		public override object DuplicateEmpty()
		{
			EquipItem other = (EquipItem)Duplicate();
			other.SetAmount(0);
			return other;
		}
		public override object DuplicateEmptyNew()
		{
			EquipItem other = (EquipItem)Duplicate();
			other.SetAmount(0);
			other.SetUID(ServerJob.RequestNewUID);
			return other;
		}
	}





	public class ToolItem : Item
	{
		public ToolItem(int uid) : base(uid)   => category = ItemCat.tool;


		public override bool HasSpace() 	   => amount < maxAmount;


		private	void	Decrease (int count)   => amount -= count;
		private	void	Increase (int count)   => amount += count;
		private int 	GetSpace			   => maxAmount - amount;


		public  bool	AddStack(ref Item item)
		{
			int enableStack = Math.Min(item.GetLeftOver(), GetSpace);

			((ToolItem)item).Decrease(enableStack);
			Increase(enableStack);
			return item.GetLeftOver().Equals(0);
		}


		public override object Duplicate() => MemberwiseClone();
		public override object DuplicateNew()
		{
			ToolItem other = (ToolItem)Duplicate();
			other.SetUID(ServerJob.RequestNewUID);
			return other;
		}
		public override object DuplicateEmpty()
		{
			ToolItem other = (ToolItem)Duplicate();
			other.SetAmount(0);
			return other;
		}
		public override object DuplicateEmptyNew()
		{
            ToolItem other = (ToolItem)Duplicate();
            other.SetAmount(0);
            other.SetUID(ServerJob.RequestNewUID);
            return other;
		}
	}
}










// namespace Popup.Items
// {
// 	using Cfg = Configs.Configs;
// 	using ServerJob = ServerJob.ServerJob;
// 	[Serializable]
// 	public class Item : IPopupObject
// 	{
// 		[SerializeField]
// 		string		name;
//         [SerializeField]
//         int 		uid;
//         [SerializeField]
// 		float		weight;
// 		[SerializeField]
// 		float		volume;
// 		[SerializeField]
// 		int			amount;
// 		int			maxAmount = int.MaxValue;
// 		[SerializeField]
// 		Grade		grade;
// 		[SerializeField]
// 		int[]		magicIdArray;
// 		[SerializeField]
// 		ItemCat		category;
// 		[SerializeField]
// 		int			durability;



// 		public 	string 	GetName 		=> name;
// 		public 	Grade 	GetGrade 		=> grade;
//         public 	int 	GetUID() 		=> uid;
//         public 	int 	GetSpellAmount 	=> magicIdArray == null ? 0 : magicIdArray.Length;
// 		public 	float 	GetWeight 		=> weight * amount;
// 		public 	float 	GetVolume 		=> volume * amount;
// 		public 	int 	GetAmount 		=> amount;
// 		public 	int 	GetMaxAmount 	=> maxAmount;
// 		public 	ItemCat GetCat 			=> category;
// 		public	int		GetDurability	=> durability;
// 		public 	bool 	IsStackable 	=> 0 < (category & ItemCat.stackable);

// 		private	int 	GetSpace 		=> maxAmount - amount;

// /* test func start */
// 		public void SetCat(ItemCat cat) => category = cat; // test
// 		public void SetName(string name) => this.name = name; // test
// 		public void SetAmount(int amount) => this.amount = amount; // test
// 		public void SetUID(int UID) => uid = UID;   // test

//         public void SetWeight(float weight)                 // test
// 		{
// 			this.weight = weight;

// 			if (HaveAttribute(ItemCat.stackable))
// 			{
// 				int limit = Libs.Round(Cfg.slotWeightCapacity / weight);
// 				maxAmount = Math.Min(maxAmount, limit);
// 			}
// 		}

// 		public void SetVolume(float volume)                 // test
// 		{
// 			this.volume = volume;

// 			if (HaveAttribute(ItemCat.stackable))
// 			{
// 				int limit = Libs.Round(Cfg.slotVolumeCapacity / volume);
// 				maxAmount = Math.Min(maxAmount, limit);
// 			}
// 		}
// /* test func end */


// 		public Item(int uid) => this.uid = uid;



//         public 	void DecreaseAmount	(int count = 1)		=> amount -= count;
// 		private bool HaveAttribute	(ItemCat attribute) => 0 < (category & attribute);
// 		public	bool IsExist							=> 0 < durability && 0 < amount;


// 		public bool HasSpace()
// 		{
// 			if (HaveAttribute(ItemCat.stackable))
// 			{
// 				return amount < maxAmount;
// 			}

// 			return false;
// 		}



// 		public (bool, int) GetSpell(int index)
// 		{
// 			if (0 < (category & ItemCat.hasSpell))
// 			{
// 				Guard.MustInRange(index, ref magicIdArray, "[GetSpell in item]");
// 				return (true, magicIdArray[index]);
// 			}
// 			return (false, 0);
// 		}



// 		public bool AddStack(ref Item item)
// 		{
// 			int enableCount = Math.Min(item.amount, GetSpace);

// 			amount += enableCount;
// 			item.DecreaseAmount(enableCount);

// 			return item.amount.Equals(0);
// 		}



// 		public Item Clone()
// 		{
// 			Item other = (Item)MemberwiseClone();
// 			other.SetAmount(0);
// 			other.SetUID(ServerJob.RequestNewUID);
//             return other;
// 		}


// 		//public static Delegate.ConvertFromString<Item> convert = new Delegate.ConvertFromString<Item>(DataConvert);


// 		//private static Item DataConvert(IDataReader data)
// 		//{
// 		//	//Item item = new Item(data.GetInt32(0));
// 		//	Item item = new Item();

// 		//	item.name = data.GetString(1);
// 		//	item.weight = data.GetFloat(2);
// 		//	item.volume = data.GetFloat(3);
// 		//	item.amount = data.GetInt32(4);
// 		//	item.grade = (Grade)data.GetInt32(5);
// 		//	item.magicIdArray = Libs.TextToIntArray(data.GetString(6));
// 		//	item.category = (ItemCat)data.GetInt32(7);
// 		//	item.durability = data.GetInt32(8);

// 		//	return item;
// 		//}
// 	}





// 	// public class EquipItem : Item
// 	// {
// 	// 	int durability;

// 	// 	public int GetDurability => durability;

// 	// 	EquipItem(int uid) => this.uid = uid;

// 	// 	public override object Clone()
// 	// 	{
// 	// 		EquipItem other = (EquipItem)MemberwiseClone();
// 	// 		other.SetAmount(0);
// 	// 		other.SetUID(Libs.RequestNewUID);
// 	// 		return other;
// 	// 	}
// 	// }





// 	// public class ToolItem : Item
// 	// {

// 	// 	ToolItem(int uid) => this.uid = uid;

// 	// 	public override object Clone()
// 	// 	{
// 	// 		ToolItem other = (ToolItem)MemberwiseClone();
// 	// 		other.SetAmount(0);
// 	// 		other.SetUID(Libs.RequestNewUID);
// 	// 		return other;
// 	// 	}

// 	// }


// }


