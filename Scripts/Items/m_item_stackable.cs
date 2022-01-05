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
            get => Amount;
            set => Amount = value;
        }
        public override int Capacity
        {
            get => MaxAmount;
            protected set => MaxAmount = value;
        }
        public override bool HaveSpace(int? nameId = null) => (nameId == null || NameId.Equals(nameId)) && Amount < MaxAmount;
        public override float TWeight() => Amount * Weight;
        public override float TVolume() => Amount * Volume;
        public override Item MakeItem(string json) => FromJson.ToItem(json);
    }
}
