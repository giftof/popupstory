using UnityEngine;
using Popup.Items;
using TMPro;



public class PStackableItem : PItemBase {
    [SerializeField] TextMeshProUGUI amount;

    public override void SetAmount(Item item)
    {
        amount.text = item.UseableCount.ToString();
    }
}
