using Popup.Library;
using Popup.Defines;
using Popup.Framework;
using Popup.Configs;
using System;
using Newtonsoft.Json;



namespace Popup.Items
{
	public abstract partial class Item : PopupObject {
		[JsonProperty]
		public float Weight { get; protected set; }
		[JsonProperty]
		public float Volume { get; protected set; }
		[JsonProperty]
		public ItemCat Category { get; protected set; }
		[JsonProperty]
		public int Icon { get; set; }

		public bool HaveAttribute(ItemCat attribute) => 0 < (Category & attribute);
		public bool IsAttribute(ItemCat attribute) => Category.Equals(attribute);
		
	}

	public abstract partial class Item {
        [JsonIgnore]
        public abstract int UseableCount { get; }
        public abstract bool HaveSpace(string _ = null);
        public abstract bool Use();
		public abstract float TWeight();
		public abstract float TVolume();
	}
	


	public partial class EquipItem : Item
    {
        [JsonProperty]
		public Grade Grade { get; protected set; }
		[JsonProperty]
		public Spell[] SpellArray { get; protected set; }
		[JsonProperty]
		public int Durability { get; protected set; }

		public int SpellAmount => SpellArray == null ? 0 : SpellArray.Length;
        public Spell Spell(int uid) => Guard.MustInclude(uid, SpellArray, "[GetSpell in EquipItem]");
	}

	public partial class EquipItem
	{
		public override bool IsExist => 0 < Durability;
		public override object DeepCopy(int? uid, int? durability) {
			EquipItem equipItem = (EquipItem)MemberwiseClone();
			equipItem.Uid = uid ?? 0;
			equipItem.Durability = durability ?? 0;
			return equipItem;
		}

		public override int UseableCount => Durability;
		public override bool HaveSpace(string _ = null) => false;
		public override bool Use() => 0 < Durability--;
		public override float TWeight() => Weight;
		public override float TVolume() => Volume;
	}



	public partial class ToolItem : Item
	{
		[JsonProperty]
		public int Amount { get; protected set; }
		[JsonIgnore]
		private int MaxAmount { get; set; } = Config.maxStack;

		private void Decrease(int count) => Amount -= count;
		private void Increase(int count) => Amount += count;
		private int Space => MaxAmount - Amount;

		public bool AddStack(Item item) {
			int enableStack = Math.Min(((ToolItem)item).Amount, Space);
			((ToolItem)item).Decrease(enableStack);
			Increase(enableStack);
			return item.UseableCount.Equals(0);
		}
	}

	public partial class ToolItem
    {
		public override bool IsExist => 0 < Amount;
		public override object DeepCopy(int? uid, int? amount) {
			ToolItem toolItem = (ToolItem)MemberwiseClone();
			toolItem.Uid = uid ?? 0;
			toolItem.Amount = amount ?? 0;
			return toolItem;
		}

		public override int UseableCount => Amount;
		public override bool HaveSpace(string name = null) => (name == null || this.Name.Equals(name)) && Amount < MaxAmount;
		public override bool Use() => 0 < Amount--;
		public override float TWeight() => Amount * Weight;
		public override float TVolume() => Amount * Volume;
	}
}
