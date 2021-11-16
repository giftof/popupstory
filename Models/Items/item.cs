using Popup.Library;
using Popup.Defines;
using Popup.Framework;
using System;
using Newtonsoft.Json;




namespace Popup.Items
{
	using Cfg = Configs.Configs;
	using ServerJob = ServerJob.ServerJob;
	public abstract class Item : IItem
	{
		[JsonProperty]
		public string name { get; protected set; }
		[JsonProperty]
		public int uid { get; protected set; }
		[JsonProperty]
		public float weight { get; protected set; }
		[JsonProperty]
		public float volume { get; protected set; }
		[JsonProperty]
		public ItemCat category { get; protected set; }

		// public Item(int uid) => this.uid = uid;

		protected bool HaveAttribute (ItemCat attribute) => 0 < (category & attribute);

		[JsonIgnore]
		public abstract bool IsExist { get; }
		[JsonIgnore]
		public abstract bool HasSpace { get; }
		[JsonIgnore]
		public abstract int UseableCount { get; }
		public abstract bool Use();
		public abstract float Weight();
		public abstract float Volume();
		public abstract object Duplicate();
		public abstract object DuplicateNew();
		public abstract object DuplicateEmpty();
		public abstract object DuplicateEmptyNew();
	}





	public class EquipItem : Item
	{
		[JsonProperty]
		public Grade grade { get; protected set; }
		[JsonProperty]
		public Spell[] spellArray { get; protected set; }
		[JsonProperty]
		public int durability { get; protected set; }

		// public EquipItem(int uid) : base(uid) => category = ItemCat.equip;

		public int SpellAmount => spellArray == null ? 0 : spellArray.Length;
		public Spell Spell(int uid) => Guard.MustInclude(uid, spellArray, "[GetSpell in EquipItem]");

		public override bool IsExist => 0 < durability;
		public override bool HasSpace => false;
		public override int UseableCount => durability;
		public override bool Use() => 0 < durability--;
		public override float Weight() => weight;
		public override float Volume() => volume;
		public override object Duplicate() => MemberwiseClone();
		public override object DuplicateNew() => (((EquipItem)Duplicate()).uid = ServerJob.RequestNewUID);
        // {
		// 	EquipItem other = (EquipItem)Duplicate();
		// 	other.uid = ServerJob.RequestNewUID;
		// 	return other;
        // }
		public override object DuplicateEmpty() => (((EquipItem)Duplicate()).durability = 0);
		// {
		// 	EquipItem other = (EquipItem)Duplicate();
		// 	other.durability = 0;
		// 	return other;
		// }
		public override object DuplicateEmptyNew()
		{
			EquipItem other = (EquipItem)Duplicate();
			other.durability = 0;
			other.uid = ServerJob.RequestNewUID;
			return other;
		}
	}





	public class ToolItem : Item
	{
		[JsonProperty]
		public int amount { get; protected set; }
		private int maxAmount { get; set; } = int.MaxValue;
	
		// public ToolItem(int uid) : base(uid) => category = ItemCat.tool;

		private	void Decrease(int count) => amount -= count;
		private	void Increase(int count) => amount += count;
		private int Space => maxAmount - amount;

		private void SetMaxAmount()
		{
			if (maxAmount.Equals(int.MaxValue))
			{
				maxAmount = Math.Min(
					Libs.Round(Cfg.slotWeightCapacity / weight),
					Libs.Round(Cfg.slotVolumeCapacity / volume));
			}
		}

		public bool	AddStack(Item item)
		{
			SetMaxAmount();

			int enableStack = Math.Min(((ToolItem)item).amount, Space);

			((ToolItem)item).Decrease(enableStack);
			Increase(enableStack);
			return item.UseableCount.Equals(0);
		}

		public override bool IsExist => 0 < amount;
		public override bool HasSpace => amount < maxAmount;
		public override int UseableCount => amount;
		public override bool Use() => 0 < amount--;
		public override float Weight() => amount * weight;
		public override float Volume() => amount * volume;
		public override object Duplicate() => MemberwiseClone();
		public override object DuplicateNew() => (((ToolItem)Duplicate()).uid = ServerJob.RequestNewUID);
		// {
		// 	return (((ToolItem)Duplicate()).uid = ServerJob.RequestNewUID);
		// 	// ToolItem other = (ToolItem)Duplicate();
		// 	// other.uid = ServerJob.RequestNewUID;
		// 	// return other;
		// }
		public override object DuplicateEmpty() => (((ToolItem)Duplicate()).amount = 0);
		// {
		// 	return (((ToolItem)Duplicate()).amount = 0);
		// 	// ToolItem other = (ToolItem)Duplicate();
		// 	// other.amount = 0;
		// 	// return other;
		// }
		public override object DuplicateEmptyNew()
		{
            ToolItem other = (ToolItem)Duplicate();
            other.amount = 0;
            other.uid = ServerJob.RequestNewUID;
            return other;
		}
	}
}
