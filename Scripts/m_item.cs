using System;
using Newtonsoft.Json;

using Popup.Defines;
using Popup.Framework;




namespace Popup.Items {

	public abstract class Item : PopupObject {
		[JsonProperty]
		public int PositionId { get; protected set; }
		[JsonProperty]
		public int ItemId { get; protected set; }
		[JsonProperty]
		public float Weight { get; protected set; }
		[JsonProperty]
		public float Volume { get; protected set; }
		[JsonProperty]
		public ItemCat Category { get; protected set; }
		[JsonProperty]
		public int Icon { get; set; }

		protected Item()
		{
			ChangeUseableCountHandler += new EventHandler<Item>(c_item.Instance.UpdateCount);
		}

		/********************************/
		/* Abstract		              	*/
		/********************************/

		[JsonIgnore]
		public abstract int UseableCount { get; set; }
		[JsonIgnore]
		public abstract int Capacity { get; protected set; }
		public abstract bool HaveSpace(int? _ = null);
		public abstract float TWeight();
		public abstract float TVolume();
		public abstract Item MakeItem(string json);

		/********************************/
		/* Checker						*/
		/********************************/

		public bool HaveAttribute(ItemCat attribute)
		{
			return 0 < (Category & attribute);
		}

		public override bool IsExist
		{
			get { return 0 < UseableCount; }
		}

		public int Space
		{
			get { return Capacity - UseableCount; }
		}

		/********************************/
		/* Behaviour					*/
		/********************************/

		public int Increment(int amount)
		{
			int increment = Math.Min(Space, amount);
			UseableCount += increment;
			ChangeUseableCountHandler?.Invoke(this, this);
			return increment;
		}

		public int Decrement(int amount)
		{
			int decrement = Math.Min(UseableCount, amount);
			UseableCount += decrement;
			ChangeUseableCountHandler?.Invoke(this, this);
			return decrement;
		}

		public void Repair(int amount)
		{
			Increment(amount);
		}

		public void Charge(int amount)
		{
			Increment(amount);
		}

		/********************************/
		/* Events						*/
		/********************************/

		event EventHandler<Item> ChangeUseableCountHandler;
	}
}
