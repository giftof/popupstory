using System;
using UnityEngine;
using Newtonsoft.Json;

using Popup.Delegate;
using Popup.Defines;
using Popup.Framework;




namespace Popup.Items
{
    public abstract class m_item : PopupObject
    {
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

        protected m_item() { }

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
        public abstract m_item MakeItem(string json);

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

        public void DoChangeUseableCountHandler()
        {
            ChangeUseableCountHandler?.Invoke(this, this);
        }

        public void Repair(int amount)
        {
            this.Increment(amount);
        }

        public void Charge(int amount)
        {
            this.Increment(amount);
        }

        public void Charge(m_item item)
        {
            this.Increment(item.Decrement(Space));
        }

        /********************************/
        /* Events						*/
        /********************************/

        public EventHandler<m_item> AddChangeUseableCountListener
        {
            set { ChangeUseableCountHandler += new EventHandler<m_item>(value); }
        }

        public event EventHandler<m_item> ChangeUseableCountHandler;
    }
}
