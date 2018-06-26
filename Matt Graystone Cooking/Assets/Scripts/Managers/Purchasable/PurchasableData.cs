using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class PurchasableData : MonoBehaviour
{
    /// <summary>
    /// Instantiate class object
    /// </summary>
    public static PurchasableData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<PurchasableData>();
            }

            return PurchasableData.instance;
        }
    }
    private static PurchasableData instance;

    /// <summary>
    /// 
    /// </summary>
    public Purchasable Purchasable;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    public void Start()
    {
        Purchasable.GUI_Button_FirstTime_Purchase.onClick.AddListener(FirstTimePurchase);
        Purchasable.GUI_Button_Upgrade.onClick.AddListener(Upgrade);
        Purchasable.Cost_To_Purchase_Amount = 1;

        Purchasable.GUI_Button_Sell_All.onClick.AddListener(SellALL);

        Text Costtext = Purchasable.GUI_Button_FirstTime_Purchase.GetComponentInChildren<Text>();
        Costtext.text = CurrencyConverter.Instance.GetCurrencyIntoString(Purchasable.Cost);

        Purchasable.On_Complete = true;

        if (Purchasable.Started_Timer == true)
        {
            Purchasable.GUI_Progression_Bar.Value = Purchasable.Current_Time;

            StartCoroutine(Purchasable.Update_Timer());
        }

        Purchasable.Cost_To_Purchase_Amount = 1;

        Purchasable.GUI_Image_Potrait.sprite = Purchasable.Image;

        Inventory.Instance.AddSlot(Purchasable.GUI_Slot, ItemType.Item);

        Purchasable.BonusStats = new BonusStats();
    }

    private void SellALL()
    {
        Item item = ItemDatabase.Instance.FetchItemByID(int.Parse(Purchasable.Item_ID));
        ItemData itemData = Inventory.Instance.GetItemData(item);
        if (itemData == null)
            return;

        Inventory.Instance.SellItem(item, itemData.count);

        Purchasable.UpdateGUI();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    public void Update()
    {
        Purchasable.Update();

        //check if this has run before, and start it if it has.
        if (Purchasable.Is_Purchased == true)
        {
            Purchasable.GUI_Button_FirstTime_Purchase.gameObject.SetActive(false);
        }
        else
        {
            Purchasable.GUI_Button_FirstTime_Purchase.gameObject.SetActive(true);
        }

        //check if this has run before, and start it if it has.
        if(Purchasable.Unlocked == true && Purchasable.On_Complete == true)
        {
            StartCoroutine(Purchasable.Update_Timer());
        }

        //update items that are droped into our slot
        if (Purchasable.GUI_Slot.transform.childCount > 0)
        {
            GameObject gObject = Purchasable.GUI_Slot.transform.GetChild(0).gameObject;
            if (gObject != null)
            {
                gObject.GetComponent<Slot>().CheckSlotForItem();
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void FirstTimePurchase()
    {
        StartCoroutine(Purchasable.Update_Timer());

        Purchasable.First_Time_Purchase();
    }

    /// <summary>
    /// 
    /// </summary>
    public void Upgrade()
    {
        Purchasable.Upgrade();
    }
}
