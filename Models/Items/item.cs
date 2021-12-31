using Popup.Library;
using Popup.Defines;
using Popup.Framework;
using Popup.Configs;
using Popup.Delegate;
using System;
using UnityEngine;
using Newtonsoft.Json;



namespace Popup.Items
{
	public abstract partial class Item : PopupObject {
		//public delegate void UpdateIcon(int num);
		//public UpdateIcon _updateIcon;
		[JsonIgnore]
		public Action updateIcon;
		[JsonIgnore]
		public Action updateUseableConut;

		[JsonProperty]
		public float Weight { get; protected set; }
		[JsonProperty]
		public float Volume { get; protected set; }
		[JsonProperty]
		public ItemCat Category { get; protected set; }
		[JsonProperty]
		public int Icon {
			get => m_icon;
			set {
				m_icon = value;
				updateIcon?.Invoke();
			}
		}
		[JsonIgnore]
		private int m_icon;

		public bool HaveAttribute(ItemCat attribute) => 0 < (Category & attribute);
		public bool IsAttribute(ItemCat attribute) => Category.Equals(attribute);

		public void reload() {
			updateIcon?.Invoke();
			updateUseableConut?.Invoke();
		}
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
		public int Durability { get; protected set; }
		[JsonProperty]
		public Spell[] SpellArray { get; protected set; }

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
		public override bool Use() {
			Durability--;
			updateUseableConut?.Invoke();
			return 0 < Durability;
		}
		public override float TWeight() => Weight;
		public override float TVolume() => Volume;
	}



	public partial class StackableItem : Item
	{
		[JsonProperty]
		public int Amount { get; protected set; }
		[JsonIgnore]
		private int MaxAmount { get; set; } = Config.maxStack;

		private int Decrease(int count) {
			int decrease = Math.Min(count, Amount);

			Amount -= decrease;
			updateUseableConut?.Invoke();

			return decrease;
		}
		private int Increase(int count) {
			int increase = Math.Min(count, Space);

			Amount += increase;
			updateUseableConut?.Invoke();

			return increase;
		}
		//private void Decrease(int count) => Amount -= count;
		//private void Increase(int count) => Amount += count;
		private int Space => MaxAmount - Amount;

		public bool AddStack(StackableItem item) {
			Increase(item.Decrease(Space));
			return item.Amount.Equals(0);
			//int enableStack = Math.Min(item.Amount, Space);

			//item.Decrease(enableStack);
			//Increase(enableStack);

			//return item.Amount.Equals(0);
		}
	}

	public partial class StackableItem
    {
		public override bool IsExist => 0 < Amount;
		public override object DeepCopy(int? uid, int? amount) {
			StackableItem toolItem = (StackableItem)MemberwiseClone();
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
