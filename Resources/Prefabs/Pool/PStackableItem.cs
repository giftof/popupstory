using UnityEngine;
using Popup.Items;
using TMPro;



public class PStackableItem : PItemBase {
    [SerializeField] TextMeshProUGUI amount;

    public override void SetAmount(m_item item)
    {
        amount.text = item.UseableCount.ToString();
    }
}
