using Newtonsoft.Json;

using Popup.Converter;
using Popup.Configs;




namespace Popup.Items
{
    public class m_stackable_item : m_item
    {
        [JsonProperty]
        public int Amount { get; protected set; }
        [JsonIgnore]
        private int MaxAmount { get; set; } = Config.maxStack;

        protected m_stackable_item() : base() { }

        /********************************/
        /* Implement Abstract funcs		*/
        /********************************/

        public override object DeepCopy(int uid)
        {
            m_stackable_item toolItem = (m_stackable_item)MemberwiseClone();
            toolItem.Uid = uid;
            toolItem.Amount = 0;
            return toolItem;
        }

        public override int UseableCount
        {
            get { return Amount; }
            set { Amount = value; }
        }

        public override int Capacity
        {
            get { return MaxAmount; }
            protected set { MaxAmount = value; }
        }

        public override bool HaveSpace(int? nameId = null)
        {
            return (nameId == null || NameId.Equals(nameId)) && Amount < MaxAmount;
        }

        public override float TWeight()
        {
            return Amount * Weight;
        }

        public override float TVolume() 
        {
            return Amount * Volume;
        }

        public override m_item MakeItem(string json)
        {
            return FromJson.ToItem(json);
        }
    }
}
