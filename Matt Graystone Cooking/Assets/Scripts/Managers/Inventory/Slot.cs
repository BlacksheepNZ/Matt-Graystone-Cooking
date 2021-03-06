﻿using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 
/// </summary>
public class Slot : MonoBehaviour, 
                    IDropHandler, 
                    IPointerClickHandler
{
    /// <summary>
    /// 
    /// </summary>
    //[HideInInspector]
    public int ID;

    /// <summary>
    /// 
    /// </summary>
    //[HideInInspector]
    public SlotType SlotType;

    /// <summary>
    /// 
    /// </summary>
    private void SetNewItem(ItemData dropedItem)
    {
        //move item to new slot
        dropedItem.transform.SetParent(this.transform);
        dropedItem.transform.position = this.transform.position;
        Inventory.Instance.Items[dropedItem.slot] = new Item();
        Inventory.Instance.Items[ID] = dropedItem.Item;
        dropedItem.slot = ID;
    }

    /// <summary>
    /// 
    /// </summary>
    private void MergItem(ItemData dropedItem)
    {
        if (this.transform.childCount > 0)
        {
            ItemData item_in_slot = transform.GetChild(0).GetComponent<ItemData>();
            if(item_in_slot.Item.ID == dropedItem.Item.ID)
            {
                Inventory.Instance.AddAmoutToItem(dropedItem.count, item_in_slot.slot);
                Inventory.Instance.DestroyItemFromMerg(dropedItem);
                dropedItem.OnEndDrag(null);

            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void SwapItem(ItemData dropedItem)
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

    /// <summary>
    /// 
    /// </summary>
    private bool InventoryType(int slotID, ItemData item, SlotType slotType)
    {
        if (item.Item.SlotType == SlotType
            || SlotType == SlotType.General)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData>();
        if (droppedItem == null) return;

        int SlotID = droppedItem.slot;
        if (Inventory.Instance.Items[ID].ID == 000)
        {
            //add new item
            #region todo
            switch (droppedItem.Item.SlotType)
            {
                case SlotType.General:
                    if (InventoryType(SlotID, droppedItem, SlotType.General)) { SetNewItem(droppedItem); }
                    break;
                case SlotType.Consumable:
                    if (InventoryType(SlotID, droppedItem, SlotType.General)) { SetNewItem(droppedItem); }
                    break;
                case SlotType.Item:
                    if (InventoryType(SlotID, droppedItem, SlotType.General)) { SetNewItem(droppedItem); }
                    break;
            }
            #endregion
        }
        else if (Inventory.Instance.Items[ID].ID == droppedItem.Item.ID)
        {
            //merg item
            switch (droppedItem.Item.SlotType)
            {
                case SlotType.General:
                    if (InventoryType(SlotID, droppedItem, SlotType.General)) { MergItem(droppedItem); }
                    break;
                case SlotType.Consumable:
                    if (InventoryType(SlotID, droppedItem, SlotType.General)) { MergItem(droppedItem); }
                    break;
                case SlotType.Item:
                    if (InventoryType(SlotID, droppedItem, SlotType.General)) { MergItem(droppedItem); }
                    break;
            }
        }
        else if (Inventory.Instance.Items[ID].ID != droppedItem.Item.ID)
        {
            //Swap item
            switch (droppedItem.Item.SlotType)
            {
                case SlotType.General:
                    if (InventoryType(SlotID, droppedItem, SlotType.General))
                    {
                        SwapItem(droppedItem);
                    }
                    break;
                case SlotType.Consumable:
                    if (InventoryType(SlotID, droppedItem, SlotType.General) ||
                        InventoryType(SlotID, droppedItem, SlotType.Consumable))
                    {
                        SwapItem(droppedItem);
                    }
                    break;
                case SlotType.Item:
                    if (InventoryType(SlotID, droppedItem, SlotType.General) ||
                        InventoryType(SlotID, droppedItem, SlotType.Item))
                    {
                        //CheckSlotForItem();
                        SwapItem(droppedItem);
                    }
                    break;
            }
        }
        else { }
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        //for (int x = 0; x < Inventory.Instance.SlotsToCheck.Count; x++)
        //{
        //    //change this to slot type?
        //    if (Inventory.Instance.SlotsToCheck[x] == ID)
        //    {
        //        Tabs.Instance.Open_Inventory();
        //    }
        //}
    }

    public void CheckSlotForItem()
    {
        if (this.transform.childCount <= 0)
        {
            //get the parent item of the slot
            GameObject parent = this.gameObject.transform.parent.transform.parent.gameObject;
            //get the purchasable data from parent so we can modify it.
            PurchasableData purchasable = parent.GetComponent<PurchasableData>();

            if (purchasable != null)
            {
                purchasable.Purchasable.RemoveBonus();
            }
            return;
        }
        if (this.transform.childCount > 0)
        {
            //get the parent item of the slot
            GameObject parent = this.gameObject.transform.parent.transform.parent.gameObject;
            //get the purchasable data from parent so we can modify it.
            PurchasableData purchasable = parent.GetComponent<PurchasableData>();
            if (purchasable != null)
            {
                //get the item in our child
                GameObject child = this.gameObject.transform.GetChild(0).gameObject;

                //get child item data
                Item childItem = child.GetComponent<ItemData>().Item;

                //this will throw error for slots that are not part of an parent with Purchasable, ie inventory items
                purchasable.Purchasable.AddBonus(childItem);
            }
            return;

        }
        else { }
    }
}
