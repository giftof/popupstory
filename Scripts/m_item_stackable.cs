using Newtonsoft.Json;

using Popup.Converter;
using Popup.Configs;




namespace Popup.Items
{
    public class StackableItem : Item
    {
        [JsonProperty]
        public int Amount { get; protected set; }
        [JsonIgnore]
        private int MaxAmount { get; set; } = Config.maxStack;

        protected StackableItem() : base() { }

        /********************************/
        /* Implement Abstract funcs		*/
        /********************************/

        public override object DeepCopy(int? uid)
        {
            StackableItem toolItem = (StackableItem)MemberwiseClone();
            toolItem.Uid = uid ?? 0;
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

        public override Item MakeItem(string json)
        {
            return FromJson.ToItem(json);
        }
    }
}
