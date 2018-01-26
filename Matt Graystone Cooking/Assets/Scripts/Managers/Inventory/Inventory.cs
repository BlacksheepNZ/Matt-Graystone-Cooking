using LitJson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private static Inventory instance;
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

    public GameObject Inventory_Panel;
    public GameObject SlotPanel;
    public GameObject ItemHolder;

    public Text TotalGold;

    public int SlotCount = 0;

    public GameObject InventorySlot;
    public List<GameObject> slots = new List<GameObject>();

    public GameObject InventoryItem;
    public List<Item> Items = new List<Item>();

    public List<int> SlotsToCheck = new List<int>();

    private List<ItemSellValue> ItemSellValue = new List<global::ItemSellValue>();
    private List<MineralSellValue> MineralSellValue = new List<global::MineralSellValue>();

    //public List<GameObject> Consumable_Slot;
    //public int Consumable_Slot_Count;
    //public GameObject Parent_Consumable_Slot;
    //public GameObject Parent_ShieldGrid_Slot;
    //public GameObject Parent_WeaponsArray_Slot;
    //public GameObject Parent_Sensors_Slot;
    //public GameObject Parent_Transporters_Slot;

    private void Start()
    {
        ItemSellValue = SaveLoad.Instance.Item_Sell_Value;
        MineralSellValue = SaveLoad.Instance.Mineral_Sell_Value;

        for (int i = 0; i < SlotCount; i++)
        {
            slots.Add(Instantiate(InventorySlot));
            InISlot(SlotPanel, i, ItemType.General);
        }
    }

    public List<int> ReturnSlotIDOfType(ItemType ItemType)
    {
        List<int> value = new List<int>();

        for (int i = 0; i < slots.Count; i++)
        {
            Slot slot = slots[i].GetComponent<Slot>();

            if (slot.ItemType == ItemType)
            {
                value.Add(slot.ID);
            }
        }
        return value;
    }

    public void AddSlot(GameObject SlotPanelParent, ItemType slotType)
    {
        slots.Add(Instantiate(InventorySlot));
        InISlot(SlotPanelParent, (slots.Count() - 1), slotType);
        SlotsToCheck.Add(slots.Count() - 1);
    }

    public void InISlot(GameObject parentObject, int slotID, ItemType slotType)
    {
        slots[slotID].transform.SetParent(parentObject.transform);
        slots[slotID].transform.localScale = new Vector3(1, 1, 1);
        slots[slotID].transform.position = parentObject.transform.position;
        slots[slotID].GetComponent<Slot>().ItemType = slotType;
        slots[slotID].GetComponent<Slot>().ID = slotID;
        slots[slotID].name = slotID.ToString();
        Items.Add(new Item());
    }

    public void Update()
    {
        for (int i = 0; i < SlotsToCheck.Count; i++)
        {
            int slotId = SlotsToCheck[i];
            slots[slotId].GetComponent<Slot>().CheckDrillDataItemSlot();
            //slots[slotId].GetComponent<Slot>().CheckScavangerDataItemSlot();
        }

        TotalGold.text = CurrencyConverter.Instance.GetCurrencyIntoString(
            Game.Instance.TotalGold);
    }

    public int GetEmptySlot()
    {
        //find general slot
        for (int x = 0; x < slots.Count(); x++)
        {
            Slot slot = slots[x].GetComponent<Slot>();

            if (slot.transform.childCount <= 0)
            {
                switch (slot.ItemType)
                {
                    case ItemType.WorkerItem:
                        return x;
                    case ItemType.ScavangerItem:
                        return x;
                    case ItemType.Ore:
                        return x;
                }
            }
        }

        return 0;
    }

    public int GetSlotWithItem(ItemType itemType)
    {
        //find general slot
        for (int x = 0; x < slots.Count(); x++)
        {
            Slot slot = slots[x].GetComponent<Slot>();

            if (slot.transform.childCount > 0 && slot.ItemType == itemType)
            {
                return x;
            }
        }

        return -1;
    }

    public int GetEmptySlot(ItemType itemType)
    {
        //find general slot
        for (int x = 0; x < slots.Count(); x++)
        {
            Slot slot = slots[x].GetComponent<Slot>();

            if (slot.transform.childCount <= 0 && slot.ItemType == itemType)
            {
                return x;
            }
        }

        return -1;
    }

    public void AddAmoutToItem(int amount, int slot)
    {
        ItemData data = slots[slot].transform.GetChild(0).GetComponent<ItemData>();

        data.count = amount;

        data.transform.Find("Count").GetComponent<Text>().text = CurrencyConverter.Instance.GetCurrencyIntoStringNoSign(data.count);
    }

    public void AddItemToSlot(int slot, int id, int amount)
    {
        Item itemToAdd = ItemDatabase.Instance.FetchItemByID(id);

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
        data.count = amount;
        data.transform.Find("Count").GetComponent<Text>().text = CurrencyConverter.Instance.GetCurrencyIntoStringNoSign(data.count);

        Items[slot] = itemToAdd;
    }

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
                        if (slots[x].GetComponent<Slot>().ItemType == ItemType.General && slots[x].GetComponent<Slot>().transform.childCount <= 0)
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

                    itemObject.GetComponent<ItemData>().count = 1;
                    break;
                }
            }
        }
    }

    public bool InventoryFull()
    {
        int MaxCount = 0;
        int itemCount = 0;

        for (int i = 0; i < SaveLoad.Instance.Item_Database.Count(); i++)
        {
            for (int x = 0; x < slots.Count; x++)
            {
                if (slots[x].GetComponent<Slot>().ItemType == ItemType.General)
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

                //dont forgot to check the item holder if the player has pick up the item
                //if (ItemHolder.transform.childCount > 0)
                //{
                //    if (ItemDatabase.Instance.Database[i].ID == id && slots[x].transform.GetChild(0).GetComponent<ItemData>().Item.ID == id)
                //    {
                //        //we have an item in the item holder
                //        ItemData data = ItemHolder.transform.GetChild(0).GetComponent<ItemData>();
                //        return data.amount;
                //    }
                //}
            }
        }

        return 0;
    }

    public void RemoveItem(int id, int amount)
    {
        if(id == 000)
            return;

        Item itemToRemove = ItemDatabase.Instance.FetchItemByID(id);
        if (itemToRemove.Stackable && CheckInventory(itemToRemove))
        {

            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].ID == id)
                {
                    ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
                    data.count -= amount;
                    data.transform.Find("Count").GetComponent<Text>().text = CurrencyConverter.Instance.GetCurrencyIntoStringNoSign(data.count);
                    if (data.count == 0)
                    {
                        Destroy(slots[i].transform.GetChild(0).gameObject);
                        Items[i] = new Item();
                        break;
                    }
                    else if (data.count == 1)
                    {
                        slots[i].transform.GetChild(0).transform.Find("Count").GetComponent<Text>().text = "";
                        break;
                    }
                    break;
                }
            }
        }
        else
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].ID != 000 && Items[i].ID == id)
                {
                    Destroy(slots[i].transform.GetChild(0).gameObject);
                    Items[i] = new Item();
                    break;
                }
            }
        }
    }

    public int GetSlotByID(int id)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].ID == id)
            {
                if (slots[i].transform.childCount > 0)
                {
                    return i;
                }
            }
        }

        return -1;
    }

    bool CheckInventory(Item item)
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

    //selling items is broken it removes it from the master list instead of the ingame object
    #region 

    List<int> slotWithItem = new List<int>();
    public int SellItemCount()
    {
        return slotWithItem.Count();
    }
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

                if (itemdata.Item.ItemType == ItemType.Ore)
                {
                    for (int i = 0; i < MineralSellValue.Count(); i++)
                    {
                        if (MineralSellValue[i].ResourceType == itemdata.Item.ResourceType)
                        {
                            float value = MineralSellValue[i].Value * itemcount;

                            Game.Instance.AddGold(value);
                            Debug.Log("Sold " + itemdata.Item.ResourceType + " Gold added: " + value);
                            RemoveItem(itemToRemove.ID, itemcount);
                            slotWithItem.Clear();
                            UnSelectAll();
                        }
                    }
                }
            }
        }
    }

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

    public bool boolToogleButton = false;

    public void UseComsumable(int ID)
    {
        if (SlotPanel.transform.childCount > 0)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                GameObject slot = slots[i];

                if (slot.GetComponent<Slot>().ItemType == ItemType.General)
                {
                    GameObject item = slot.transform.GetChild(0).gameObject;
                    ItemData itemdata = item.GetComponent<ItemData>();

                    if (itemdata.Item.ItemType == ItemType.Consumable)
                    {
                        if (ID == itemdata.Item.ID)
                        {
                            Debug.Log("Comsumed Potion");
                            RemoveItem(itemdata.Item.ID, 1);
                            break;
                        }
                    }
                }
            }
        }
    }

    public void SellItem()
    {
        slotWithItem.Clear();

        //get all items in main weapon panel
        if (SlotPanel.transform.childCount > 0)
        {
            //make sure the item type are the same weapons to weaponslot
            for (int i = 0; i < slots.Count; i++)
            {
                //make sure only inventory items are selected
                if (slots[i].GetComponent<Slot>().ItemType == ItemType.General)
                {
                    //remove slots without weapons in it
                    if (slots[i].transform.childCount > 0)
                    {
                        if (slots[i].transform.GetChild(0).GetComponent<ItemData>().Item.ItemType == ItemType.WorkerItem ||
                            slots[i].transform.GetChild(0).GetComponent<ItemData>().Item.ItemType == ItemType.ScavangerItem ||
                            slots[i].transform.GetChild(0).GetComponent<ItemData>().Item.ItemType == ItemType.Ore)
                        {
                            slotWithItem.Add(i);
                        }
                    }
                }
            }

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

    public void SellItem(int SlotID, int amount)
    {
        if (slots[SlotID].transform.childCount > 0)
        {
            ItemData itemData = slots[SlotID].transform.GetChild(0).GetComponent<ItemData>();

            int count = 0;

            int value = itemData.count - amount;

            if(value >= 0)
            {
                count = amount;
            }
            else
            {
                count = itemData.count;
            }

            ItemType itemType = itemData.Item.ItemType;
            ResourceType resourceType = itemData.Item.ResourceType;

            for (int i = 0; i < MineralSellValue.Count(); i++)
            {
                if (MineralSellValue[i].ResourceType == resourceType)
                {
                    float sellValue = MineralSellValue[i].Value * count;

                    Game.Instance.AddGold(sellValue);
                    Debug.Log("Sold " + count + "@" + MineralSellValue[i].Value + " " + resourceType + " Gold added: " + value + " from slot " + SlotID);
                    RemoveItem(itemData.Item.ID, count);
                    break;
                }
            }
        }
        else
        {
            //no items in slot
        }
    }

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

            ItemType itemType = itemData.Item.ItemType;
            ResourceType resourceType = itemData.Item.ResourceType;

            float v = sellValue * count;

            Game.Instance.AddGold(v);
            RemoveItem(itemData.Item.ID, count);
        }
        else
        {
            //no items in slot
        }
    }
}