using Popup.Library;
using Popup.Defines;
using Popup.Framework;
using Popup.Configs;
using System;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;



namespace Popup.Items {
	public abstract class Item : PopupObject, IPopupObserved {

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
				Notify();
			}
		}
		[JsonIgnore]
		private int m_icon;



		/********************************/
		/* Define Behaviours			*/
		/********************************/

		public bool HaveAttribute(ItemCat attribute) => 0 < (Category & attribute);
		public bool IsAttribute(ItemCat attribute) => Category.Equals(attribute);



		/********************************/
		/* Abstract funcs              	*/
		/********************************/

        [JsonIgnore]
        public abstract int UseableCount { get; }
        public abstract bool HaveSpace(int? _ = null);
        public abstract void Use();
		public abstract float TWeight();
		public abstract float TVolume();



		/********************************/
		/* Implement Observed			*/
		/********************************/

		private readonly List<Action> monitorList = new List<Action>();
        public void AddDelegate(Action action) => monitorList.Add(action);
        public void RemoveDelegate(Action action) => monitorList.Remove(monitorList.FirstOrDefault(e => e.Equals(action)));
		public void Notify() {
			foreach (Action func in monitorList)
				func?.Invoke();
		}
	}
	


	public class SolidItem : Item {
		[JsonProperty]
		public int Durability { get; protected set; }
		[JsonProperty]
		public Spell[] SpellArray { get; protected set; }



		/********************************/
		/* Define Behaviours			*/
		/********************************/

		public int SpellAmount => SpellArray == null ? 0 : SpellArray.Length;
        public Spell Spell(int uid) => Guard.MustInclude(uid, SpellArray, "[GetSpell in EquipItem]");



		/********************************/
		/* Implement Abstract funcs		*/
		/********************************/

		public override bool IsExist => 0 < Durability;
		public override object DeepCopy(int? uid) {
			SolidItem equipItem = (SolidItem)MemberwiseClone();
			equipItem.Uid = uid ?? 0;
			equipItem.Durability = 0;
			return equipItem;
		}

		public override int UseableCount => Durability;
		public override bool HaveSpace(int? _ = null) => false;
		public override void Use() {
			--Durability;
			Notify();
		}
		public override float TWeight() => Weight;
		public override float TVolume() => Volume;
	}



	public class StackableItem : Item {
		[JsonProperty]
		public int Amount { get; protected set; }
		[JsonIgnore]
		private int MaxAmount { get; set; } = Config.maxStack;



		/********************************/
		/* Define Behaviours			*/
		/********************************/

		private int Decrease(int count) {
			int decrease = Math.Min(count, Amount);

			Amount -= decrease;
			Notify();
			return decrease;
		}
		private int Increase(int count) {
			int increase = Math.Min(count, Space);

			Amount += increase;
			Notify();
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
		public override object DeepCopy(int? uid) {
			StackableItem toolItem = (StackableItem)MemberwiseClone();
			toolItem.Uid = uid ?? 0;
			toolItem.Amount = 0;
			return toolItem;
		}

		public override int UseableCount => Amount;
		public override bool HaveSpace(int? nameId = null) => (nameId == null || this.NameId.Equals(nameId)) && Amount < MaxAmount;
		public override void Use() => Decrease(1);
		public override float TWeight() => Amount * Weight;
		public override float TVolume() => Amount * Volume;
	}
}
