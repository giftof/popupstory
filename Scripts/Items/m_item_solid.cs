using Newtonsoft.Json;

using Popup.Converter;





namespace Popup.Items
{
	public class SolidItem : Item
	{
		[JsonProperty]
		public int Durability { get; protected set; }
		[JsonProperty]
		public int MaxDurability { get; protected set; }
		[JsonProperty]
		public int[] SpellIdArray { get; protected set; } = { };
		[JsonProperty]
		public Spell[] SpellArray { get; protected set; }

		/********************************/
		/* Define Checker				*/
		/********************************/

		public int SpellAmount => SpellArray?.Length ?? 0;

		/********************************/
		/* Implement Abstract funcs		*/
		/********************************/

		public override object DeepCopy(int? uid)
		{
			SolidItem equipItem = (SolidItem)MemberwiseClone();
			equipItem.Uid = uid ?? 0;
			equipItem.Durability = 0;
			return equipItem;
		}
		public override int UseableCount
		{
			get => Durability;
			set => Durability = value;
		}
		public override int Capacity
		{
			get => MaxDurability;
			protected set => MaxDurability = value;
		}
		public override bool HaveSpace(int? _ = null) => false;
		public override float TWeight() => Weight;
		public override float TVolume() => Volume;
		public override Item MakeItem(string json) => FromJson.ToItem(json);
	}
}
