using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using Popup.Items;
using Popup.Framework;
using Popup.Configs;
using Popup.Defines;
using Newtonsoft.Json;



//public class ItemMaker {
//    public Item item;
//    public void Stackable(Item item) => this.item = item;
//    public void Solid(Item item) => this.item = item;
//    public Item RawData(string json) {
//        return JsonConvert.DeserializeObject<SolidItem>(json);
//    }
//    //Item item8 = JsonConvert.DeserializeObject<SolidItem>(itemDef8);
//}


public abstract class PItemBase : MonoBehaviour, IItemHandler {

//    void Awake() {
//        ItemMaker itemMaker = new ItemMaker();
///* begin test code */
//        //itemMaker.RawData(json);
//        itemMaker.RawData("");
//        //if (Item == null)
//        //    itemMaker.Stackable(new StackableItem());
//        //else
//        //    itemMaker.Solid(new SolidItem());
//        /* end test code */
//        Item = itemMaker.item;
//    }

    [SerializeField]
    private Image image = null;
    public PItemSlot LastParentSlot { get; set; }
    private Action useAction = null;
    private Vector2 offset = default;
    public Item Item { get; set; }

    /********************************/
    /* Transfer						*/
    /********************************/

    public void Use() => Decrease(1);
    protected int Decrease(int count) {
        int decrease = Math.Min(count, Item.UseableCount);

        Item.UseableCount -= decrease;
        return decrease;
    }
    protected int Increase(int count) {
        int increase = Math.Min(count, Item.UseableCount);

        Item.UseableCount += increase;
        return increase;
    }
    public void ChargeWith(PItemBase itemBase) => Increase(itemBase.Decrease(itemBase.Item.Space));
    public void Repair(int value) => Increase(value);






    public void SetUseAction(Action action) => useAction = action;
    public void AddUseAction(Action action) => useAction += action;
    public void RemoveUseAction(Action action) => useAction -= action;
    public abstract Prefab Type { get; }



    /********************************/
    /* Delegate Action              */
    /********************************/

    public abstract void SetAmount();
    public void SetIconImage() => image.sprite = Resources.Load<Sprite>($"{Path.icon}{Item.Icon}");
    public void ReleaseObject() => ObjectPool.Instance.Release(Type, gameObject);



    /********************************/
    /* IItemHandler                 */
    /********************************/

    public void OnDrag(PointerEventData eventData) => transform.position = eventData.position + offset;

    public void OnBeginDrag(PointerEventData eventData) {
        Debug.Log($"[TO DEBUG] eventData.selectedObject is? ({eventData.selectedObject})");
        eventData.selectedObject = gameObject;
        image.raycastTarget = false;
    }

    public void OnEndDrag(PointerEventData eventData) {
        image.raycastTarget = true;
    }

    public void OnPointerDown(PointerEventData eventData) {
        offset = (Vector2)transform.position - eventData.position;
        transform.SetParent(Manager.Instance.guiGuide.pickCanvas.transform);
    }

    public void OnPointerUp(PointerEventData eventData) {
        if (LastParentSlot != null) {
            transform.SetParent(LastParentSlot.transform);
            transform.localPosition = Vector3.zero;
        }
    }

    int clickCount = 0;
    float clickTime = 0f;
    public void OnPointerClick(PointerEventData eventData) {
        if (0 < clickCount && eventData.clickTime - clickTime < Config.doubleClickInterval) {
            clickCount = 0;
            useAction.Invoke();
        }
        else {
            clickCount = 1;
            clickTime = eventData.clickTime;
        }
    }
}
