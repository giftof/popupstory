using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Popup.Items;
using Popup.Library;
using Popup.Defines;



public abstract partial class SquadBase : MonoBehaviour {
    [SerializeField] protected PCustomButton inventoryBtn;
    [SerializeField] protected PInventoryBase inventoryBase;

    private void Start() => SetButtonAction();

    //public void Insert(params PItemBase[] itemBaseArray) => inventoryBase.Insert(itemBaseArray.Select(e => e.Item).ToArray());
    //public void Insert(params Item[] itemArray) => inventoryBase.Insert(itemArray);
    //public void Remove(PItemBase item) => inventoryBase.Remove(item);
    public void InventoryPosition(GUIPosition position) => inventoryBase.gameObject.PositionOnCanvas(position);
    public PInventoryBase Inventory => inventoryBase;





    protected void ToggleInventory() => inventoryBase.gameObject.SetActive(!inventoryBase.gameObject.activeSelf);





    protected abstract void SetButtonAction();
}




public abstract partial class SquadBase {
    // DEBUG_TEST
}
