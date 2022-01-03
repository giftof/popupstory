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
		public void Use() => Decrease(1);
		public override bool IsExist => 0 < UseableCount;

		protected int Decrease(int count) {
			int decrease = Math.Min(count, UseableCount);

			UseableCount -= decrease;
			Notify();
			return decrease;
		}
		protected int Increase(int count) {
			int increase = Math.Min(count, UseableCount);

			UseableCount += increase;
			Notify();
			return increase;
		}



		/********************************/
		/* Abstract funcs              	*/
		/********************************/

        [JsonIgnore]
        public abstract int UseableCount { get; protected set; }
		[JsonIgnore]
		public abstract int Capacity { get; protected set; }
        public abstract bool HaveSpace(int? _ = null);
		public abstract float TWeight();
		public abstract float TVolume();
		public abstract int Space { get; }
        


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
		public int MaxDurability { get; protected set; }
		[JsonProperty]
		public Spell[] SpellArray { get; protected set; }



		/********************************/
		/* Define Behaviours			*/
		/********************************/

		public int SpellAmount => SpellArray?.Length ?? 0;
		public Spell Spell(int uid) => SpellArray.FirstOrDefault(e => e.Uid.Equals(uid));
		public override int Space => MaxDurability - Durability;
		public void Repair(int value) => Increase(value);



		/********************************/
		/* Implement Abstract funcs		*/
		/********************************/

		public override object DeepCopy(int? uid) {
			SolidItem equipItem = (SolidItem)MemberwiseClone();
			equipItem.Uid = uid ?? 0;
			equipItem.Durability = 0;
			// Notify();
			return equipItem;
		}
		public override int UseableCount {
			get => Durability;
			protected set => Durability = value;
		}
		public override int Capacity {
			get => MaxDurability;
			protected set => MaxDurability = value;
		}
		public override bool HaveSpace(int? _ = null) => false;
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

		public override int Space => MaxAmount - Amount;
		public void AddStack(StackableItem item) => Increase(item.Decrease(Space));



		/********************************/
		/* Implement Abstract funcs		*/
		/********************************/

		public override object DeepCopy(int? uid) {
			StackableItem toolItem = (StackableItem)MemberwiseClone();
			toolItem.Uid = uid ?? 0;
			toolItem.Amount = 0;
			// Notify();
			return toolItem;
		}
		public override int UseableCount {
			get => Amount;
			protected set => Amount = value;
		}
		public override int Capacity {
			get => MaxAmount;
			protected set => MaxAmount = value;
		}
		public override bool HaveSpace(int? nameId = null) => (nameId == null || this.NameId.Equals(nameId)) && Amount < MaxAmount;
		public override float TWeight() => Amount * Weight;
		public override float TVolume() => Amount * Volume;
	}
}
