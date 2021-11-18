using Popup.Library;
using Popup.Defines;
using Popup.Framework;
using System;
using Newtonsoft.Json;



namespace Popup.Items
{
	using Cfg = Configs.Configs;
	public abstract class Item : IItem
	{
		[JsonProperty]
		public string name { get; protected set; }
		[JsonProperty]
		public int uid { get; protected set; }
		[JsonProperty]
		public int slotId { get; set; }
		[JsonProperty]
		public float weight { get; protected set; }
		[JsonProperty]
		public float volume { get; protected set; }
		[JsonProperty]
		public ItemCat category { get; protected set; }

		protected bool HaveAttribute (ItemCat attribute) => 0 < (category & attribute);

		[JsonIgnore]
		public abstract bool IsExist { get; }
		[JsonIgnore]
		public abstract bool HasSpace { get; }
		[JsonIgnore]
		public abstract int UseableCount { get; }
		public abstract bool HaveSpace(string _);
		public abstract bool Use();
		public abstract float Weight();
		public abstract float Volume();
		public abstract object DeepCopy(int? _ = null, int? __ = null);
	}



	public class EquipItem : Item
	{
		[JsonProperty]
		public Grade grade { get; protected set; }
		[JsonProperty]
		public Spell[] spellArray { get; protected set; }
		[JsonProperty]
		public int durability { get; protected set; }

		public int SpellAmount => spellArray == null ? 0 : spellArray.Length;
		public Spell Spell(int uid) => Guard.MustInclude(uid, spellArray, "[GetSpell in EquipItem]");

		public override bool IsExist => 0 < durability;
		public override bool HasSpace => false;
		public override int UseableCount => durability;
		public override bool HaveSpace(string _) => false;
		public override bool Use() => 0 < durability--;
		public override float Weight() => weight;
		public override float Volume() => volume;
		public override object DeepCopy(int? uid, int? durability)
		{
			EquipItem equipItem = (EquipItem)MemberwiseClone();
			equipItem.uid = uid ?? 0;
			equipItem.durability = durability ?? 0;
			return equipItem;
		}
	}



	public class ToolItem : Item
	{
		[JsonProperty]
		public int amount { get; protected set; }
		private int maxAmount { get; set; } = int.MaxValue;

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
		public override bool HaveSpace(string name) => this.name.Equals(name) && HasSpace;
		public override bool Use() => 0 < amount--;
		public override float Weight() => amount * weight;
		public override float Volume() => amount * volume;
		public override object DeepCopy(int? uid, int? amount)
		{
			ToolItem toolItem = (ToolItem)MemberwiseClone();
			toolItem.uid = uid ?? 0;
			toolItem.amount = amount ?? 0;
			return toolItem;
		}
	}
}
