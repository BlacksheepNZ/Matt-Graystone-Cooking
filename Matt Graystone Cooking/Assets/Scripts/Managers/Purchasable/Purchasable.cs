﻿using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// 
/// </summary>
[Serializable]
public class Purchasable
{
    /// <summary>
    /// GUI
    /// </summary>
    public Text GUI_Text_Item_Name;

    /// <summary>
    /// GUI
    /// </summary>
    public Text GUI_Text_Count;

    /// <summary>
    /// GUI
    /// </summary>
    public Text GUI_Text_Value;

    /// <summary>
    /// GUI
    /// </summary>
    public Button GUI_Button_Sell_All;

    /// <summary>
    /// GUI
    /// </summary>
    public Text GUI_Text_Total_Count;

    /// <summary>
    /// GUI
    /// </summary>
    public Text GUI_Text_Sell_Value;

    /// <summary>
    /// GUI
    /// </summary>
    public Image GUI_Image_Potrait;

    /// <summary>
    /// GUI
    /// </summary>
    public Button GUI_Button_Upgrade;

    /// <summary>
    /// GUI
    /// </summary>
    public Text GUI_Text_Reward;

    /// <summary>
    /// GUI
    /// </summary>
    public Text GUI_Text_Cost_To_Purchase;

    /// <summary>
    /// GUI
    /// </summary>
    public Button GUI_Button_FirstTime_Purchase;

    /// <summary>
    /// GUI
    /// </summary>
    public ProgressionBar GUI_Progression_Bar;

    /// <summary>
    /// GUI
    /// </summary>
    public GameObject GUI_Slot;

    /// <summary>
    /// Buy Amount Mode (1, 50 , 100);
    /// </summary>
    [HideInInspector]
    public int Cost_To_Purchase_Amount;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public Sprite Image;

    /// <summary>
    /// Current Level
    /// </summary>
    [HideInInspector]
    public int ID
    {
        get { return this._id; }
        set { this._id = value; }
    }
    private int _id;

    /// <summary>
    /// Name
    /// </summary>
    [HideInInspector]
    public string Item_Name;

    /// <summary>
    /// Base cost to purchase
    /// </summary>
    [HideInInspector]
    public float Base_Cost;

    /// <summary>
    /// current cost
    /// </summary>
    [HideInInspector]
    public float Cost;

    /// <summary>
    /// cost muiltplyer
    /// </summary>
    [HideInInspector]
    public float Coefficent;

    /// <summary>
    /// current count
    /// </summary>
    [HideInInspector]
    public int Count;

    /// <summary>
    /// amount of resources per tick
    /// </summary>
    [HideInInspector]
    public float Resource_Rate;

    /// <summary>
    /// associated item gained at the end of the timer
    /// </summary>
    [HideInInspector]
    public string Item_ID;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public float Time_To_Complete_Task = 1.0f;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public bool On_Complete;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public bool Unlocked = false;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public bool Is_Purchased;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public bool Started_Timer;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public float Current_Time = 0;

    /// <summary>
    /// Bonus
    /// </summary>
    public BonusStats BonusStats;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    public Purchasable( int id, 
                        string itemName,
                        float baseCost,
                        Sprite image,
                        float coefficent,
                        int count,
                        float resourceRate,
                        string itemID,
                        float timeToCompleteTask)
    {
        this._id = id;
        Item_Name = itemName;
        Base_Cost = baseCost;
        Image = image;
        Coefficent = coefficent;
        Count = count;
        Resource_Rate = resourceRate;
        Item_ID = itemID;
        Time_To_Complete_Task = timeToCompleteTask;

        Cost = Base_Cost;
        Is_Purchased = false;

        BonusStats = new BonusStats();
    }

    /// <summary>
    /// 
    /// </summary>
    public bool Can_Purchase()
    {
        float value = Decrease_Value(Cost, BonusStats.Decrese_Cost);

        if (Game.Instance.CanPurchase(value))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    public void Update()
    {
        UpdateGUI();
    }

    /// <summary>
    /// 
    /// </summary>
    public void UpdateGUI()
    {
        Item item = ItemDatabase.Instance.FetchItemByID(int.Parse(Item_ID));

        if (item == null)
            return;

        //GUI_Text_Cost_To_Purchase.text = CurrencyConverter.Instance.GetCurrencyIntoString(Cost_To_Buy());
        GUI_Text_Item_Name.text = item.Name;
        GUI_Text_Count.text = "LVL. " + Count.ToString("0");

        ItemData itemData = Inventory.Instance.GetItemData(item);

        if (itemData == null)
            return;

        GUI_Text_Total_Count.text = CurrencyConverter.Instance.GetCurrencyIntoStringNoSign
                                    (itemData.count) + "\n" + "Sell All";

        GUI_Text_Sell_Value.text = CurrencyConverter.Instance.GetCurrencyIntoString(
                                   (float)Inventory.Instance.ItemValue(item));

        float resourceRate = Resource_Rate * Count;

        //cost to level
        //resource rate
        //

        //test reward
        GUI_Text_Reward.text = BonusStats.ToString();
            //"Resource Rate: " + CurrencyConverter.Instance.GetCurrencyIntoString(
            //    Increase_Value(Resource_Rate, BonusStats.Increase_Resource_Rate)) + Display_Text(0) + "\n" +

            //"Resource Rate: " + 
            //CurrencyConverter.Instance.GetCurrencyIntoStringNoSign((1 + (BonusStats.Increase_Resource_Rate / 100)) * resourceRate) + 
            //Display_Text(BonusStats.Increase_Resource_Rate) + "\n" +

            //"Speed: " + Display_Text(BonusStats.Decrease_Time_To_Complete_Task);

        //GUI_Text_Reward.text = Display_Text();// "+ " + CurrencyConverter.Instance.GetCurrencyIntoString(Resource_Rate);
        GUI_Text_Value.text = "x"+CurrencyConverter.Instance.GetCurrencyIntoStringNoSign(Resource_Rate * Count);
    }

    /// <summary>
    /// 
    /// </summary>
    public float Decrease_Value(float cost, float decrease)
    {
        if(decrease == 0)
        {
            return cost;
        }

        float percent = decrease / 100;

        float value = cost - (percent * cost);
        Mathf.Clamp(value, 0.001f, value);
        return value;
    }

    /// <summary>
    /// 
    /// </summary>
    public float Increase_Value(float cost, float increase)
    {
        if (increase == 0)
        {
            return cost;
        }

        float percent = increase / 100;
        float value = cost + (percent * cost);
        Mathf.Clamp(value, 0.001f, value);
        return value;
    }

    /// <summary>
    /// 
    /// </summary>
    public void First_Time_Purchase()
    {
        if (Can_Purchase() == true)
        {
            Game.Instance.RemoveGold(Cost);
            Is_Purchased = true;
            Unlocked = true;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public float Cost_To_Buy()
    {
        float cost = Decrease_Value(Cost, BonusStats.Decrese_Cost);

        float value = 0;
        for (int i = 0; i < Cost_To_Purchase_Amount; i++)
        {
            value += cost * Coefficent;
        }

        return value;
    }

    /// <summary>
    /// 
    /// </summary>
    public void Upgrade()
    {
        Item item = ItemDatabase.Instance.FetchItemByID(int.Parse(Item_ID));

        if (item == null)
            return;

        ItemData itemData = Inventory.Instance.GetItemData(item);

        if (itemData == null)
            return;

        float cost = Decrease_Value(Cost, BonusStats.Decrese_Cost);
        float value = Cost_To_Buy();

        if (Game.Instance.CanPurchase(value))
        {
            Game.Instance.RemoveGold(value);

            for (int i = 0; i < Cost_To_Purchase_Amount; i++)
            {
                Cost += cost * Coefficent;
            }

            Count += Cost_To_Purchase_Amount;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public IEnumerator Update_Timer()
    {
        Started_Timer = true;
        On_Complete = false;

        float speed = (Time.fixedDeltaTime / Time_To_Complete_Task);

        while (GUI_Progression_Bar.Value < 1)
        {
            GUI_Progression_Bar.Value += speed;
            Current_Time = GUI_Progression_Bar.Value;

            yield return null;
        }

        GUI_Progression_Bar.Value = 0;
        Reset_Timer();
    }

    /// <summary>
    /// 
    /// </summary>
    private void Reset_Timer()
    {
        if (!Inventory.Instance.InventoryFull())
        {
            PlayerManager.Instance.AddExperience(Count * Resource_Rate);

            float cost = Increase_Value((Count * Resource_Rate), BonusStats.Increase_Resource_Rate);
            Inventory.Instance.AddItem(int.Parse(Item_ID), (int)cost);
            On_Complete = true;
        }
    }

    /// <summary>
    /// Add Bonus to purchasable
    /// </summary>
    public void AddBonus(Item itemToAdd)
    {
        for (int i = 0; i < itemToAdd.BonusAttached.Count; i++)
        {
            var x = itemToAdd.BonusAttached[i];
            BonusStats.Add(x.Item1, x.Item2);
        }
    }

    public void RemoveBonus()
    {
        BonusStats = new BonusStats();
    }
}
