using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    public int ID;

    public ItemType ItemType;

    void SetNewItem(ItemData dropedItem)
    {
        //move item to new slot
        dropedItem.transform.SetParent(this.transform);
        dropedItem.transform.position = this.transform.position;
        Inventory.Instance.Items[dropedItem.slot] = new Item();
        Inventory.Instance.Items[ID] = dropedItem.Item;
        dropedItem.slot = ID;
    }

    void SwapItem(ItemData dropedItem)
    {
        if (this.transform.childCount > 0)
        {
            Transform item = this.transform.GetChild(0);
            item.GetComponent<ItemData>().slot = dropedItem.slot;
            item.transform.SetParent(Inventory.Instance.slots[dropedItem.slot].transform);
            item.transform.position = Inventory.Instance.slots[dropedItem.slot].transform.position;

            Inventory.Instance.Items[dropedItem.slot] = item.GetComponent<ItemData>().Item;
            Inventory.Instance.Items[ID] = dropedItem.Item;
            dropedItem.slot = ID;
            dropedItem.transform.SetParent(this.transform);
            dropedItem.transform.position = this.transform.position;
        }
    }

    bool InventoryType(int slotID, ItemData item, ItemType itemType)
    {
        if (item.Item.ItemType == ItemType
            || ItemType == ItemType.General)
        {
            return true;
        }

        return false;
    }

    //public void CheckDrillDataItemSlot()
    //{
    //    //2 slots one is overiding the other.

    //    if (this.transform.childCount <= 0) //what does this do???
    //    {
    //        //get the parent item of the slot
    //        GameObject parent = this.gameObject.transform.parent.transform.parent.gameObject;
    //        //get the purchasable data from parent so we can modify it.
    //        PurchasableData DrillData = parent.GetComponent<PurchasableData>();

    //        return;
    //    }
    //    if (this.transform.childCount > 0)
    //    {
    //        //get the parent item of the slot
    //        GameObject parent = this.gameObject.transform.parent.transform.parent.gameObject;
    //        //get the purchasable data from parent so we can modify it.
    //        PurchasableData DrillData = parent.GetComponent<PurchasableData>();

    //        //get the item in our child
    //        GameObject child = this.gameObject.transform.GetChild(0).gameObject;
    //        //get child item data
    //        Item childItem = child.GetComponent<ItemData>().Item;

    //        //this will throw error for slots that are not part of an parent with Purchasable, ie inventory items

    //        if (DrillData != null)
    //        {
    //            DrillData.Purchasable.AddRewards(childItem);
    //        }
    //        return;

    //    }
    //    else { }
    //}

    //public void CheckScavangerDataItemSlot()
    //{
    //    //2 slots one is overiding the other.

    //    if (this.transform.childCount <= 0)
    //    {
    //        //get the parent item of the slot
    //        GameObject parent = this.gameObject.transform.parent.transform.parent.gameObject;
    //        //get the purchasable data from parent so we can modify it.
    //        ScavangerData ScavangerData = parent.GetComponent<ScavangerData>();

    //        if (ScavangerData != null)
    //        {
    //            ScavangerData.Purchasable.RemoveRewards();
    //        }
    //        return;
    //    }
    //    if (this.transform.childCount > 0)
    //    {
    //        //get the parent item of the slot
    //        GameObject parent = this.gameObject.transform.parent.transform.parent.gameObject;
    //        //get the purchasable data from parent so we can modify it.
    //        ScavangerData ScavangerData = parent.GetComponent<ScavangerData>();

    //        //get the item in our child
    //        GameObject child = this.gameObject.transform.GetChild(0).gameObject;
    //        //get child item data
    //        Item childItem = child.GetComponent<ItemData>().Item;

    //        //this will throw error for slots that are not part of an parent with Purchasable, ie inventory items

    //        if (ScavangerData != null)
    //        {
    //            ScavangerData.Purchasable.AddRewards(childItem);
    //        }
    //        return;

    //    }
    //    else { }
    //}

    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData>();
        if (droppedItem == null) return;

        int SlotID = droppedItem.slot;
        //slot empty add droped item to it
        if (Inventory.Instance.Items[ID].ID == 000)
        {
            #region todo
            switch (droppedItem.Item.ItemType)
            {
                case ItemType.General:
                    if (InventoryType(SlotID, droppedItem, ItemType.General)) { SetNewItem(droppedItem); }
                    break;
                case ItemType.Consumable:
                    if (InventoryType(SlotID, droppedItem, ItemType.General)) { SetNewItem(droppedItem); }
                    break;
            }
            #endregion
        }
        else
        {
            //Swap item
            switch (droppedItem.Item.ItemType)
            {
                case ItemType.General:
                    if (InventoryType(SlotID, droppedItem, ItemType.General)) { SwapItem(droppedItem); }
                    break;
                case ItemType.Consumable:
                    if (InventoryType(SlotID, droppedItem, ItemType.General) ||
                        InventoryType(SlotID, droppedItem, ItemType.Consumable))
                    { SwapItem(droppedItem); }
                    break;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        for (int x = 0; x < Inventory.Instance.SlotsToCheck.Count; x++)
        {
            //change this to slot type?
            if (Inventory.Instance.SlotsToCheck[x] == ID)
            {
                Tabs.Instance.Open_Inventory();
            }
        }
    }
}
