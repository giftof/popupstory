using Popup.Library;
using Popup.Defines;
using Popup.Framework;
using Popup.Configs;
using System;
using Newtonsoft.Json;



namespace Popup.Items
{
	public abstract class Item : PopupObject {
		[JsonIgnore]
		public Action updateIcon;
		[JsonIgnore]
		public Action updateUseableConut;
		[JsonIgnore]
		public Action removeEmptySlot;

		[JsonProperty]
		public int ItemId { get; protected set; }
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

		public void Reload() {
			updateIcon?.Invoke();
			updateUseableConut?.Invoke();
		}



		/********************************/
		/* Abstract funcs              	*/
		/********************************/

        [JsonIgnore]
        public abstract int UseableCount { get; }
        public abstract bool HaveSpace(int? _ = null);
        public abstract bool Use();
		public abstract float TWeight();
		public abstract float TVolume();
	}
	


	public class SolidItem : Item
    {
		[JsonProperty]
		public int Durability { get; protected set; }
		[JsonProperty]
		public Spell[] SpellArray { get; protected set; }

		public int SpellAmount => SpellArray == null ? 0 : SpellArray.Length;
        public Spell Spell(int uid) => Guard.MustInclude(uid, SpellArray, "[GetSpell in EquipItem]");



		/********************************/
		/* Implement Abstract funcs		*/
		/********************************/

		public override bool IsExist => 0 < Durability;
		public override object DeepCopy(int? uid, int? durability) {
			SolidItem equipItem = (SolidItem)MemberwiseClone();
			equipItem.Uid = uid ?? 0;
			equipItem.Durability = durability ?? 0;
			return equipItem;
		}

		public override int UseableCount => Durability;
		public override bool HaveSpace(int? _ = null) => false;
		public override bool Use() {
			--Durability;
			updateUseableConut?.Invoke();
			if (UseableCount <= 0)
				removeEmptySlot?.Invoke();
			
			return 0 < Durability;
		}
		public override float TWeight() => Weight;
		public override float TVolume() => Volume;
	}



	public class StackableItem : Item
	{
		[JsonProperty]
		public int Amount { get; protected set; }
		[JsonIgnore]
		private int MaxAmount { get; set; } = Config.maxStack;

		private int Decrease(int count) {
			int decrease = Math.Min(count, Amount);

			Amount -= decrease;
			updateUseableConut?.Invoke();
			if (UseableCount <= 0)
				removeEmptySlot?.Invoke();

			return decrease;
		}
		private int Increase(int count) {
			int increase = Math.Min(count, Space);

			Amount += increase;
			updateUseableConut?.Invoke();

			return increase;
		}
		private int Space => MaxAmount - Amount;

		public bool AddStack(StackableItem item) {
			Increase(item.Decrease(Space));
			return item.Amount.Equals(0);
		}



		/********************************/
		/* Implement Abstract funcs		*/
		/********************************/

		public override bool IsExist => 0 < Amount;
		public override object DeepCopy(int? uid, int? amount) {
			StackableItem toolItem = (StackableItem)MemberwiseClone();
			toolItem.Uid = uid ?? 0;
			toolItem.Amount = amount ?? 0;
			return toolItem;
		}

		public override int UseableCount => Amount;
		public override bool HaveSpace(int? nameId = null) => (nameId == null || this.NameId.Equals(nameId)) && Amount < MaxAmount;
		public override bool Use() {
			Decrease(1);
			return 0 < Amount;
		}
		public override float TWeight() => Amount * Weight;
		public override float TVolume() => Amount * Volume;
	}
}
