using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class Inventory : MonoBehaviour
{
    /// <summary>
    /// Instantiate class object
    /// </summary>
    public static Inventory Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Inventory>();
            }

            return Inventory.instance;
        }
    }
    private static Inventory instance;

    /// <summary>
    /// GUI
    /// </summary>
    public GameObject GUI_Panel_Inventory;

    /// <summary>
    /// GUI
    /// </summary>
    public GameObject GUI_Panel_Slot;

    /// <summary>
    /// GUI
    /// </summary>
    public GameObject GUI_Item_Holder;

    /// <summary>
    /// GUI
    /// </summary>
    [HideInInspector]
    public Text TotalGold;

    /// <summary>
    /// GUI
    /// </summary>
    public int SlotCount = 0;

    /// <summary>
    /// GUI
    /// </summary>
    public GameObject InventorySlot;

    /// <summary>
    /// GUI
    /// </summary>
    public GameObject InventoryItem;

    /// <summary>
    /// GUI
    /// </summary>
    public GameObject GUI_Panel_Split_Stack;

    /// <summary>
    /// GUI
    /// </summary>
    public ToolTip GUI_Panel_ToolTip;

    /// <summary>
    /// 
    /// </summary>
    private List<ItemSellValue> ItemSellValue = new List<ItemSellValue>();

    /// <summary>
    /// 
    /// </summary>
    private List<MineralSellValue> MineralSellValue = new List<MineralSellValue>();

    /// <summary>
    /// 
    /// </summary>
    //[HideInInspector]
    public List<GameObject> slots = new List<GameObject>();

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public List<Item> Items = new List<Item>();

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public List<int> SlotsToCheck = new List<int>();

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public bool boolToogleButton = false;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    private void Start()
    {
        ItemSellValue = SaveLoad.Instance.Item_Sell_Value;
        MineralSellValue = SaveLoad.Instance.Mineral_Sell_Value;

        for (int i = 0; i < SlotCount; i++)
        {
            slots.Add(Instantiate(InventorySlot));
            InISlot(GUI_Panel_Slot, i, SlotType.General);
        }

        CreateSplitStack();
    }

    /// <summary>
    /// 
    /// </summary>
    public List<int> ReturnSlotIDOfType(SlotType ItemType)
    {
        List<int> value = new List<int>();

        for (int i = 0; i < slots.Count; i++)
        {
            Slot slot = slots[i].GetComponent<Slot>();

            if (slot.SlotType == ItemType)
            {
                value.Add(slot.ID);
            }
        }
        return value;
    }

    /// <summary>
    /// 
    /// </summary>
    public void AddSlot(GameObject SlotPanelParent, SlotType slotType)
    {
        slots.Add(Instantiate(InventorySlot));

        int id = slots.Count() - 1;

        InISlot(SlotPanelParent, id, slotType);
        SlotsToCheck.Add(id);
    }

    /// <summary>
    /// 
    /// </summary>
    public void InISlot(GameObject parentObject, int slotID, SlotType slotType)
    {
        slots[slotID].transform.SetParent(parentObject.transform); 
        slots[slotID].transform.localScale = new Vector3(1, 1, 1);
        slots[slotID].transform.position = parentObject.transform.position;
        slots[slotID].GetComponent<Slot>().SlotType = slotType;
        slots[slotID].GetComponent<Slot>().ID = slotID;
        slots[slotID].name = slotID.ToString();
        Items.Add(new Item());
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    public void Update()
    {
        TotalGold.text = CurrencyConverter.Instance.GetCurrencyIntoString(
            Game.Instance.TotalGold);
    }

    /// <summary>
    /// 
    /// </summary>
    public void ActivateToolTip(ItemData item)
    {
        GUI_Panel_ToolTip.gameObject.SetActive(true);
        GUI_Panel_ToolTip.Activate(item);
        StartCoroutine(GUI_Panel_ToolTip.UpdateCount());

        if (Input.mousePosition.x > Screen.width / 2)
        {
            ChangePivot(1);
        }
        if (Input.mousePosition.x < Screen.width / 2)
        {
            ChangePivot(0);
        }

        GUI_Panel_ToolTip.transform.position = item.transform.position;
    }

    /// <summary>
    /// 
    /// </summary>
    public void DeActivateToolTip()
    {
        GUI_Panel_ToolTip.gameObject.SetActive(false);
        GUI_Panel_ToolTip.DeActivate();
        GUI_Panel_ToolTip.transform.position = Vector3.zero;
        StopCoroutine(GUI_Panel_ToolTip.UpdateCount());
    }

    /// <summary>
    /// Change Pivot (0 left, 1 right)
    /// </summary>
    public void ChangePivot(int pivotValue)
    {
        RectTransform rectTransform = GUI_Panel_ToolTip.gameObject.GetComponent<RectTransform>();

        rectTransform.offsetMin = new Vector2(pivotValue, 1);
        rectTransform.offsetMax = new Vector2(pivotValue, 1);

        rectTransform.pivot = new Vector2(pivotValue, 1);
    }

    #region Item_Split_Stack

    /// <summary>
    /// 
    /// </summary>
    private void CreateSplitStack()
    {
        GUI_Panel_Split_Stack.transform.SetAsLastSibling();
        GUI_Panel_Split_Stack.transform.localScale = new Vector3(1, 1, 1);
        GUI_Panel_Split_Stack.SetActive(false);
    }

    /// <summary>
    /// 
    /// </summary>
    public void ShowSplitStack(ItemData item, GameObject slot)
    {
        if (item.count > 0)
        {
            GUI_Panel_Split_Stack.SetActive(true);
            GUI_Panel_Split_Stack.transform.position = item.transform.position;
            GUI_Panel_Split_Stack.GetComponent<SplitStack>().ItemData =  slot;
            GUI_Panel_Split_Stack.GetComponent<SplitStack>().GUI_Text_Count.text 
                = CurrencyConverter.Instance.GetCurrencyIntoStringNoSign(item.count);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void SplitItem(ItemData itemToAdd, int newValue)
    {
        RemoveItem(itemToAdd.Item.ID, (int)newValue, slots[itemToAdd.slot].GetComponent<Slot>());
        AddItemToSlot(GetEmptySlot(), itemToAdd.Item.ID, (int)newValue);
    }

    /// <summary>
    /// 
    /// </summary>
    public void HideSplitStack()
    {
        GUI_Panel_Split_Stack.SetActive(false);
        GUI_Panel_Split_Stack.GetComponent<SplitStack>().ItemData = null;
    }

    #endregion

    /// <summary>
    /// 
    /// </summary>
    public int GetEmptySlot()
    {
        //find general slot
        for (int x = 0; x < slots.Count(); x++)
        {
            Slot slot = slots[x].GetComponent<Slot>();

            if (slot.transform.childCount <= 0)
            {
                switch (slot.SlotType)
                {
                    case SlotType.General:
                        return x;
                }
            }
        }

        return 0;
    }

    /// <summary>
    /// 
    /// </summary>
    public int GetSlotWithItem(SlotType itemType)
    {
        //find general slot
        for (int x = 0; x < slots.Count(); x++)
        {
            Slot slot = slots[x].GetComponent<Slot>();

            if (slot.transform.childCount > 0 && slot.SlotType == itemType)
            {
                return x;
            }
        }

        return -1;
    }

    /// <summary>
    /// 
    /// </summary>
    public int GetEmptySlot(SlotType itemType)
    {
        //find general slot
        for (int x = 0; x < slots.Count(); x++)
        {
            Slot slot = slots[x].GetComponent<Slot>();

            if (slot.transform.childCount <= 0 && slot.SlotType == itemType)
            {
                return x;
            }
        }

        return -1;
    }

    /// <summary>
    /// 
    /// </summary>
    public void AddAmoutToItem(int amount, int slot)
    {
        ItemData data = slots[slot].transform.GetChild(0).GetComponent<ItemData>();

        data.count += amount;
        data.transform.Find("Count").GetComponent<Text>().text = CurrencyConverter.Instance.GetCurrencyIntoStringNoSign(data.count);
    }

    /// <summary>
    /// 
    /// </summary>
    public Item GetItemFromSlot(int slot_id)
    {
        if (slots[slot_id].transform.childCount > 0)
        {
            Item data = slots[slot_id].transform.GetChild(0).GetComponent<ItemData>().Item;
            return data;
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void MoveItemToSlot(GameObject from_slot, GameObject to_slot)
    {
        GameObject local_item = from_slot.transform.GetChild(0).gameObject;
        local_item.transform.SetParent(GameObject.Find(to_slot.name).transform);
        local_item.transform.localPosition = Vector2.zero;
        local_item.transform.localScale = new Vector3(1, 1, 1);
    }

    /// <summary>
    /// 
    /// </summary>
    public void AddItemToSlot(int slot, int id, int amount)
    {
        Item itemToAdd = ItemDatabase.Instance.FetchItemByID(id);

        //merg items that are the same
        Item slotItem = GetItemFromSlot(slot);
        if (slotItem != null && slotItem.ID == id)
        {
            AddAmoutToItem(amount, slot);
            return;
        }

        GameObject itemObject = Instantiate(InventoryItem);

        itemObject.transform.SetParent(slots[slot].transform);
        itemObject.transform.position = Vector2.zero;
        itemObject.transform.localScale = new Vector3(1, 1, 1);

        itemObject.GetComponent<RectTransform>().offsetMin = Vector2.zero;
        itemObject.GetComponent<RectTransform>().offsetMax = Vector2.zero;
        itemObject.transform.position = slots[slot].transform.position;

        itemObject.GetComponent<ItemData>().Item = itemToAdd;
        itemObject.transform.Find("ItemImage").GetComponent<Image>().sprite = itemToAdd.BorderImage;
        Color myColor = new Color();
        ColorUtility.TryParseHtmlString(Resource.ItemRarityColor(itemToAdd.ItemRarity), out myColor);

        itemObject.transform.Find("BorderImage").GetComponent<Image>().color = new Color(myColor.r, myColor.g, myColor.b);
        itemObject.GetComponent<ItemData>().slot = slot;
        itemObject.name = itemToAdd.Name;

        GameObject toggle = itemObject.transform.Find("Toggle").gameObject;
        toggle.SetActive(false);
        toggle.GetComponent<Toggle>().isOn = false;

        ItemData data = slots[slot].transform.GetChild(0).GetComponent<ItemData>();
        data.count += amount;
        data.transform.Find("Count").GetComponent<Text>().text = CurrencyConverter.Instance.GetCurrencyIntoStringNoSign(data.count);

        Items[slot] = itemToAdd;
    }

    /// <summary>
    /// 
    /// </summary>
    public void AddItem(int id, int amount)
    {
        int emptySlot = GetEmptySlot();

        //get the revelent items data from id
        Item itemToAdd = ItemDatabase.Instance.FetchItemByID(id);
        if (itemToAdd == null)
        {
            Debug.Log("no item by that id exists " + id);
            return;
        }

        //if have the the item in the inventory already
        if (itemToAdd.Stackable == true && CheckInventory(itemToAdd))
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].ID == id)
                {
                    if (slots[i].transform.childCount > 0)
                    {
                        ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
                        data.count += amount;
                        data.transform.Find("Count").GetComponent<Text>().text = CurrencyConverter.Instance.GetCurrencyIntoStringNoSign(data.count);
                        break;
                    }
                }
            }
        }
        else
        {
            //no item in inventory
            for (int i = 0; i < Items.Count(); i++)
            {
                //gets the first slot thats empty
                if (Items[i].ID == 000)
                {
                    //add the item to the empty slot
                    Items[i] = itemToAdd;

                    //match the Item Type ie weapons go into weapon slot
                    int refineEmptySlot = -1;
                    //find general slot
                    for (int x = 0; x < slots.Count(); x++)
                    {//Items[i].ItemType
                        if (slots[x].GetComponent<Slot>().SlotType == SlotType.General && slots[x].GetComponent<Slot>().transform.childCount <= 0)
                        {
                            refineEmptySlot = x;
                            break;
                        }
                    }

                    if (refineEmptySlot == -1)
                    {
                        Debug.Log("no slot");
                        break;
                    }
                    GameObject itemObject = Instantiate(InventoryItem);

                    itemObject.transform.SetParent(slots[refineEmptySlot].transform);
                    itemObject.transform.position = Vector2.zero;
                    itemObject.transform.localScale = new Vector3(1, 1, 1);

                    itemObject.GetComponent<RectTransform>().offsetMin = Vector2.zero;
                    itemObject.GetComponent<RectTransform>().offsetMax = Vector2.zero;
                    itemObject.transform.position = slots[refineEmptySlot].transform.position;

                    itemObject.GetComponent<ItemData>().Item = itemToAdd;
                    itemObject.transform.Find("ItemImage").GetComponent<Image>().sprite = itemToAdd.BorderImage;
                    Color myColor = new Color();
                    ColorUtility.TryParseHtmlString(Resource.ItemRarityColor(itemToAdd.ItemRarity), out myColor);

                    itemObject.transform.Find("BorderImage").GetComponent<Image>().color = new Color(myColor.r, myColor.g, myColor.b);
                    itemObject.GetComponent<ItemData>().slot = refineEmptySlot;
                    itemObject.name = itemToAdd.Name;

                    GameObject toggle = itemObject.transform.Find("Toggle").gameObject;
                    toggle.SetActive(false);
                    toggle.GetComponent<Toggle>().isOn = false;

                    ItemData itemData = itemObject.GetComponent<ItemData>();
                    itemData.count = 1;
                    break;
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public bool InventoryFull()
    {
        int MaxCount = 0;
        int itemCount = 0;

        for (int i = 0; i < SaveLoad.Instance.Item_Database.Count(); i++)
        {
            for (int x = 0; x < slots.Count; x++)
            {
                if (slots[x].GetComponent<Slot>().SlotType == SlotType.General)
                {
                    MaxCount++;

                    //check all slots
                    if (slots[x].transform.childCount > 0)
                    {
                        itemCount++;
                    }
                }
            }
        }

        if (MaxCount == itemCount)
            return true;
        else
            return false;
    }

    /// <summary>
    /// 
    /// </summary>
    public int CheckItemCount(int id)
    {
        for (int i = 0; i < SaveLoad.Instance.Item_Database.Count(); i++)
        {
            for (int x = 0; x < slots.Count; x++)
            {
                //check all slots
                if (slots[x].transform.childCount > 0)
                {
                    if (SaveLoad.Instance.Item_Database[i].ID == id && slots[x].transform.GetChild(0).GetComponent<ItemData>().Item.ID == id)
                    {
                        //we have found out item in the slot
                        ItemData data = slots[x].transform.GetChild(0).GetComponent<ItemData>();
                        return data.count;
                    }
                }
            }
        }

        return 0;
    }

    /// <summary>
    /// 
    /// </summary>
    public void RemoveItem(int id, int amount, Slot slot)
    {
        if(id == 000)
            return;

        Item itemToRemove = ItemDatabase.Instance.FetchItemByID(id);
        if (itemToRemove != null && itemToRemove.Stackable && CheckInventory(itemToRemove))
        {
            ItemData item_data_in_slot = slot.transform.GetChild(0).GetComponent<ItemData>();

            if (item_data_in_slot.Item.ID == id)
            {
                item_data_in_slot.count -= amount;

                item_data_in_slot.transform.Find("Count").GetComponent<Text>().text = CurrencyConverter.Instance.GetCurrencyIntoStringNoSign(item_data_in_slot.count);
                if (item_data_in_slot.count <= 0)
                {
                    //Destroy(slot.transform.GetChild(0).gameObject);
                    //Items[slot.ID] = new Item();
                }
                else if (item_data_in_slot.count == 1)
                {
                    slot.transform.GetChild(0).transform.Find("Count").GetComponent<Text>().text = "";
                }
                else { }
            }
        }
        else
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].ID != 000 && Items[i].ID == id)
                {
                    Destroy(slots[i].transform.GetChild(0).gameObject);
                    break;
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void DestroyItemFromMerg(ItemData dropedItem)
    {
        if (dropedItem != null)
        {
            ItemData data = GUI_Item_Holder.transform.GetChild(0).GetComponent<ItemData>();

            Destroy(GUI_Item_Holder.transform.GetChild(0).gameObject);
            Items[dropedItem.slot] = new Item();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public int GetSlotByItemID(int itemID)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].ID == itemID)
            {
                if (slots[i].transform.childCount > 0)
                {
                    return i;
                }
            }
        }

        return -1;
    }

    /// <summary>
    /// 
    /// </summary>
    private bool CheckInventory(Item item)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].ID == item.ID)
            {
                return true;
            }
        }
        return false;
    }

    #region 

    /// <summary>
    /// 
    /// </summary>
    private List<int> slotWithItem = new List<int>();

    /// <summary>
    /// 
    /// </summary>
    public int SellItemCount()
    {
        return slotWithItem.Count();
    }

    /// <summary>
    /// 
    /// </summary>
    public void ConfirmSell()
    {
        for (int x = 0; x < slotWithItem.Count(); x++)
        {
            GameObject slot = slots[slotWithItem[x]];
            GameObject item = slot.transform.GetChild(0).gameObject;
            ItemData itemdata = item.GetComponent<ItemData>();

            Toggle toggle = slot.transform.GetChild(0).Find("Toggle").GetComponent<Toggle>();

            if (toggle.isOn == true)
            {
                Item itemToRemove = ItemDatabase.Instance.FetchItemByID(itemdata.Item.ID);
                int itemcount = CheckItemCount(itemToRemove.ID);

                #region SellWeapons

                //if (itemdata.Item.ItemType == ItemType.DrillItem || itemdata.Item.ItemType == ItemType.ScavangerItem)
                //{
                //    for (int i = 0; i < ItemSellValue.Count(); i++)
                //    {
                //        if (ItemSellValue[i].ResourceType == itemdata.Item.ResourceType)
                //        {
                //            if (ItemSellValue[i].ItemRarity == itemdata.Item.ItemRarity)
                //            {
                //                float rewardRate = ScavangerManager.Instance.GetRewardRate();

                //                float value = 0;
                //                if (rewardRate == 0)
                //                {
                //                    value = ItemSellValue[i].Value;
                //                }
                //                else
                //                {
                //                    value = ItemSellValue[i].Value * ScavangerManager.Instance.GetRewardRate();
                //                }

                //                Game.Instance.AddGold(value);
                //                Debug.Log("Gold added: " + value);
                //                RemoveItem(itemToRemove.ID, itemcount);
                //                slotWithItem.Clear();
                //            }
                //        }
                //    }
                //}

                #endregion

                //if (itemdata.Item.ItemType == ItemType.Ore)
                //{
                //    for (int i = 0; i < MineralSellValue.Count(); i++)
                //    {
                //        if (MineralSellValue[i].ResourceType == itemdata.Item.ResourceType)
                //        {
                //            float value = MineralSellValue[i].Value * itemcount;

                //            Game.Instance.AddGold(value);
                //            Debug.Log("Sold " + itemdata.Item.ResourceType + " Gold added: " + value);
                //            RemoveItem(itemToRemove.ID, itemcount);
                //            slotWithItem.Clear();
                //            UnSelectAll();
                //        }
                //    }
                //}
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void SelectAll()
    {
        for (int x = 0; x < slotWithItem.Count(); x++)
        {
            GameObject slot = slots[slotWithItem[x]];

            Toggle toggle = slot.transform.GetChild(0).transform.Find("Toggle").GetComponent<Toggle>();

            if (toggle.IsActive() == true)
            {
                toggle.isOn = true;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void UnSelectAll()
    {
        for (int x = 0; x < slotWithItem.Count(); x++)
        {
            GameObject slot = slots[slotWithItem[x]];

            GameObject toggleObject = slot.transform.GetChild(0).transform.Find("Toggle").gameObject;
            Toggle toggle = toggleObject.GetComponent<Toggle>();

            if (toggle.IsActive() == true)
            {
                toggle.isOn = false;
                toggleObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void UseComsumable(int ID)
    {
        if (GUI_Panel_Slot.transform.childCount > 0)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                GameObject slot = slots[i];

                if (slot.GetComponent<Slot>().SlotType == SlotType.General)
                {
                    GameObject item = slot.transform.GetChild(0).gameObject;
                    ItemData itemdata = item.GetComponent<ItemData>();

                    if (itemdata.Item.SlotType == SlotType.Consumable)
                    {
                        if (ID == itemdata.Item.ID)
                        {
                            Debug.Log("Comsumed Potion");
                            //RemoveItem(itemdata.Item.ID, 1);
                            break;
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void SellItem()
    {
        slotWithItem.Clear();

        //get all items in main weapon panel
        if (GUI_Panel_Slot.transform.childCount > 0)
        {
            ////make sure the item type are the same weapons to weaponslot
            //for (int i = 0; i < slots.Count; i++)
            //{
            //    //make sure only inventory items are selected
            //    if (slots[i].GetComponent<Slot>().ItemType == ItemType.General)
            //    {
            //        //remove slots without weapons in it
            //        if (slots[i].transform.childCount > 0)
            //        {
            //            if (slots[i].transform.GetChild(0).GetComponent<ItemData>().Item.ItemType == ItemType.WorkerItem ||
            //                slots[i].transform.GetChild(0).GetComponent<ItemData>().Item.ItemType == ItemType.ScavangerItem ||
            //                slots[i].transform.GetChild(0).GetComponent<ItemData>().Item.ItemType == ItemType.Ore)
            //            {
            //                slotWithItem.Add(i);
            //            }
            //        }
            //    }
            //}

            //no items in inventory
            if (slots.Count == 0)
                return;

            for (int x = 0; x < slotWithItem.Count(); x++)
            {
                Slot slot = slots[slotWithItem[x]].GetComponent<Slot>();

                //get the toggleComponent Slot->Item->Toggle
                GameObject toggle = slot.transform.GetChild(0).Find("Toggle").gameObject;
                toggle.SetActive(true);
            }
        }
    }

    #endregion

    /// <summary>
    ///   
    /// </summary>
    public int ItemValue(Item item)
    {
        ItemData itemData = GetItemData(item);

        if (itemData == null)
            return 0;

        SlotType slotType = itemData.Item.SlotType;
        ResourceType resourceType = itemData.Item.ResourceType;

        for (int i = 0; i < MineralSellValue.Count(); i++)
        {
            if (MineralSellValue[i].ResourceType == resourceType)
            {
                if (itemData.count <= 0)
                {
                    return MineralSellValue[i].Value;
                }
                else
                {
                    return MineralSellValue[i].Value * itemData.count;
                }
            }
        }

        return 0;
    }

    public ItemData GetItemData(Item item)
    {
        if (item == null)
            return null;

        int slotID = GetSlotByItemID(item.ID);

        if (slotID == -1)
            return null;

        GameObject slot = slots[slotID];

        if (slot == null)
            return null;

        if (slot.transform.childCount > 0)
        {
            ItemData itemData = slot.transform.GetChild(0).GetComponent<ItemData>();

            return itemData;
        }

        return null;
    }

    /// <summary>
    /// Set amount to 0 to sell all
    /// </summary>
    public void SellItem(Item item, int amount)
    {
        ItemData itemData = GetItemData(item);

        if (slots[itemData.slot].transform.childCount > 0)
        {
            int count = 0;

            int value = itemData.count - amount;

            if(value > 0)
            {
                count = amount;
            }
            else
            {
                count = itemData.count;
            }

            SlotType slotType = itemData.Item.SlotType;
            ResourceType resourceType = itemData.Item.ResourceType;

            for (int i = 0; i < MineralSellValue.Count(); i++)
            {
                if (MineralSellValue[i].ResourceType == resourceType)
                {
                    float sellValue = MineralSellValue[i].Value * count;

                    Game.Instance.AddGold(sellValue);
                    //Debug.Log("Sold " + count + "@" + MineralSellValue[i].Value + " " + resourceType + " Gold added: " + sellValue + " from slot " + SlotID);
                    RemoveItem(itemData.Item.ID, count, slots[itemData.slot].GetComponent<Slot>());
                    break;
                }
            }
        }
        else
        {
            //no items in slot
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void SellItem(int SlotID, int amount, int sellValue)
    {
        if (slots[SlotID].transform.childCount > 0)
        {
            ItemData itemData = slots[SlotID].transform.GetChild(0).GetComponent<ItemData>();

            int count = 0;

            int value = itemData.count - amount;

            if (value >= 0)
            {
                count = amount;
            }
            else
            {
                count = itemData.count;
            }

            SlotType slotType = itemData.Item.SlotType;
            ResourceType resourceType = itemData.Item.ResourceType;

            float v = sellValue * count;

            Game.Instance.AddGold(v);
            //RemoveItem(itemData.Item.ID, count);
        }
        else
        {
            //no items in slot
        }
    }
}