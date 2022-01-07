using Newtonsoft.Json;

using Popup.Converter;





namespace Popup.Items
{
	public class m_solid_item : m_item
	{
		[JsonProperty]
		public int Durability { get; protected set; }
		[JsonProperty]
		public int MaxDurability { get; protected set; }
		[JsonProperty]
		public int[] SpellIdArray { get; protected set; } = { };
		[JsonProperty]
		public Spell[] SpellArray { get; protected set; }

		protected m_solid_item() : base() { }

		/********************************/
		/* Checker						*/
		/********************************/

		public int SpellAmount
		{
			get { return SpellArray?.Length ?? 0; }
		}

		/********************************/
		/* Abstract						*/
		/********************************/

		public override object DeepCopy(int uid)
		{
			m_solid_item equipItem = (m_solid_item)MemberwiseClone();
			equipItem.Uid = uid;
			equipItem.Durability = 0;
			return equipItem;
		}

		public override int UseableCount
		{
			get { return Durability; }
			set { Durability = value; }
		}

		public override int Capacity
		{
			get { return MaxDurability; }
			protected set { MaxDurability = value; }
		}

		public override bool HaveSpace(int? _ = null)
		{
			return false;
		}

		public override float TWeight()
		{
			return Weight;
		}

		public override float TVolume() 
		{
            return Volume;
        } 
		
		public override m_item MakeItem(string json)
		{
			return FromJson.ToItem(json);
		}
	}
}
