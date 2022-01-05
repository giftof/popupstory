using Newtonsoft.Json;

using Popup.Defines;
using Popup.Framework;





namespace Popup.Items {

	public abstract class Item : PopupObject {
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

		/********************************/
		/* Checker						*/
		/********************************/

		public bool HaveAttribute(ItemCat attribute) => 0 < (Category & attribute);
		public override bool IsExist => 0 < UseableCount;
		public int Space => Capacity - UseableCount;

		/********************************/
		/* Abstract funcs              	*/
		/********************************/

		[JsonIgnore]
		public abstract int UseableCount { get; set; }
		[JsonIgnore]
		public abstract int Capacity { get; protected set; }
		public abstract bool HaveSpace(int? _ = null);
		public abstract float TWeight();
		public abstract float TVolume();
		public abstract Item MakeItem(string json);
	}
}
