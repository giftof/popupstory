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
		public string Name { get; protected set; }
		[JsonProperty]
		public int uid { get; protected set; }
		[JsonProperty]
		public int slotId { get; set; }
		[JsonProperty]
		public float Weight { get; protected set; }
		[JsonProperty]
		public float Volume { get; protected set; }
		[JsonProperty]
		public ItemCat Category { get; protected set; }

		protected bool HaveAttribute (ItemCat attribute) => 0 < (Category & attribute);

		[JsonIgnore]
		public abstract bool IsExist { get; }
		[JsonIgnore]
		public abstract bool HasSpace { get; }
		[JsonIgnore]
		public abstract int UseableCount { get; }
		public abstract bool HaveSpace(string _);
		public abstract bool Use();
		public abstract float TWeight();
		public abstract float TVolume();
		public abstract object DeepCopy(int? _ = null, int? __ = null);
	}



	public class EquipItem : Item
	{
		[JsonProperty]
		public Grade Grade { get; protected set; }
		[JsonProperty]
		public Spell[] SpellArray { get; protected set; }
		[JsonProperty]
		public int Durability { get; protected set; }

		public int SpellAmount => SpellArray == null ? 0 : SpellArray.Length;
		public Spell Spell(int uid) => Guard.MustInclude(uid, SpellArray, "[GetSpell in EquipItem]");

		public override bool IsExist => 0 < Durability;
		public override bool HasSpace => false;
		public override int UseableCount => Durability;
		public override bool HaveSpace(string _) => false;
		public override bool Use() => 0 < Durability--;
		public override float TWeight() => Weight;
		public override float TVolume() => Volume;
		public override object DeepCopy(int? uid, int? durability)
		{
			EquipItem equipItem = (EquipItem)MemberwiseClone();
			equipItem.uid = uid ?? 0;
			equipItem.Durability = durability ?? 0;
			return equipItem;
		}
	}



	public class ToolItem : Item
	{
		[JsonProperty]
		public int Amount { get; protected set; }
        private int MaxAmount { get; set; } = int.MaxValue;

		private	void Decrease(int count) => Amount -= count;
		private	void Increase(int count) => Amount += count;
		private int Space => MaxAmount - Amount;

		private void SetMaxAmount()
		{
			if (MaxAmount.Equals(int.MaxValue))
			{
				MaxAmount = Math.Min(
					Libs.Round(Cfg.slotWeightCapacity / Weight),
					Libs.Round(Cfg.slotVolumeCapacity / Volume));
			}
		}

		public bool	AddStack(Item item)
		{
			SetMaxAmount();

			int enableStack = Math.Min(((ToolItem)item).Amount, Space);

			((ToolItem)item).Decrease(enableStack);
			Increase(enableStack);
			return item.UseableCount.Equals(0);
		}

		public override bool IsExist => 0 < Amount;
		public override bool HasSpace => Amount < MaxAmount;
		public override int UseableCount => Amount;
		public override bool HaveSpace(string name) => this.Name.Equals(name) && HasSpace;
		public override bool Use() => 0 < Amount--;
		public override float TWeight() => Amount * Weight;
		public override float TVolume() => Amount * Volume;
		public override object DeepCopy(int? uid, int? amount)
		{
			ToolItem toolItem = (ToolItem)MemberwiseClone();
			toolItem.uid = uid ?? 0;
			toolItem.Amount = amount ?? 0;
			return toolItem;
		}
	}
}
