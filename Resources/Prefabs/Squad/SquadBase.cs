using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Popup.Items;
using Popup.Library;
using Popup.Defines;



public abstract partial class SquadBase : MonoBehaviour
{
    [SerializeField]
    protected CustomButtonPrefab inventoryBtn;
    [SerializeField]
    protected InventoryBase inventoryBase;


    private void Start()
    {
        SetButtonAction();
    }

    public void Insert(params ItemBase[] itemBaseArray)
    {
        inventoryBase.Insert(itemBaseArray.Select(e => e.Item).ToArray());
    }
    public void Insert(params Item[] itemArray)
    {
        inventoryBase.Insert(itemArray);
    }

    public void InventoryPosition(GUIPosition position)
    {
        //inventoryBase.gameObject.PositionOnParent(position);
        inventoryBase.gameObject.PositionOnCanvas(position);
    }

    public InventoryBase Inventory => inventoryBase;
}



public abstract partial class SquadBase
{
    protected void ToggleInventory() => inventoryBase.gameObject.SetActive(!inventoryBase.gameObject.activeSelf);

}



public abstract partial class SquadBase
{
    //public abstract void LinkInventory(InventoryBase inventory);
    protected abstract void SetButtonAction();
}




public abstract partial class SquadBase
{
    // DEBUG_TEST
}
