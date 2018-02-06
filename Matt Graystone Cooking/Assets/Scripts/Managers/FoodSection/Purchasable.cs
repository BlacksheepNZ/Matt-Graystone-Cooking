using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Diagnostics;
using System.Collections.Generic;
using LitJson;
using System.IO;
using System.Collections;

[System.Serializable]
public class Purchasable
{
    public Text IDText;
    public Text ItemNameText;
    public Text CountText;
    public Text ValueText;
    public Text CostToPurchase;
    public Text Text_Reward;
    public int Cost_To_Purchase_Amount;

    public Sprite Image;

    public int ID;
    public string Item_Name;
    public float Base_Cost;
    public float Cost;
    public float Coefficent;
    public int Count;
    public float Resource_Rate;
    public string Item_ID;
    public float Time_To_Complete_Task = 1.0f;

    public ProgressionBar Progression_Bar;

    public Button Button_FirstTime_Purchase;
    public Button Button_Upgrade;

    public bool On_Complete;
    public bool Unlocked = false;
    public bool Is_Purchased;

    public Purchasable(
        int id, 
        string itemName,
     float baseCost,
     Sprite image,
     float coefficent,
     int count,
     float resourceRate,
     string itemID,
     float timeToCompleteTask)
    {
        ID = id;
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
    }

    public bool Can_Purchase()
    {
        if (Game.Instance.CanPurchase(Cost))
        {
            return true;
        }

        return false;
    }

    public void Update()
    {
        CostToPurchase.text = CurrencyConverter.Instance.GetCurrencyIntoString(Cost_To_Buy());
        ItemNameText.text = Inventory.Instance.GetItemByID(int.Parse(Item_ID)).Name;
        CountText.text = "LVL. " + Count.ToString("0");

        Text_Reward.text = "+ " + CurrencyConverter.Instance.GetCurrencyIntoString(Resource_Rate);
        ValueText.text = CurrencyConverter.Instance.GetCurrencyIntoStringNoSign(Resource_Rate * Count);

        //ProgressionBar.progressText.text = CurrencyConverter.Instance.GetCurrencyIntoStringNoSign(ProgressionBar.Current) + "/" + CurrencyConverter.Instance.GetCurrencyIntoStringNoSign(ProgressionBar.Max) + DisplayText(B_DecreaseTimeToCompleteTask);
    }

    private string Display_Text(float bonus)
    {
        if (bonus != 0)
            return "<color=#0473f0><b> (" + CurrencyConverter.Instance.GetCurrencyIntoString(bonus) + "%) </b></color>";
        else
            return "";
    }

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

    public void First_Time_Purchase()
    {
        if (Can_Purchase() == true)
        {
            Game.Instance.RemoveGold(Cost);
            Is_Purchased = true;
            Unlocked = true;
        }
    }

    public float Cost_To_Buy()
    {
        float value = 0;
        for (int i = 0; i < Cost_To_Purchase_Amount; i++)
        {
            value += Cost * Coefficent;
        }

        return value;
    }

    public void Upgrade()
    {
        float value = Cost_To_Buy();

        if (Game.Instance.CanPurchase(value))
        {
            Game.Instance.RemoveGold(value);

            for (int i = 0; i < Cost_To_Purchase_Amount; i++)
            {
                Cost += Cost * Coefficent;
            }

            Count += Cost_To_Purchase_Amount;
        }
    }

    #region Timer

    public bool Started_Timer;
    public float Current_Time = 0;

    public IEnumerator Update_Timer()
    {
        Started_Timer = true;
        On_Complete = false;

        float speed = (Time.fixedDeltaTime / Time_To_Complete_Task);

        while (Progression_Bar.Value < 1)
        {
            Progression_Bar.Value += speed;
            Current_Time = Progression_Bar.Value;

            yield return null;
        }

        Progression_Bar.Value = 0;
        Reset_Timer();
    }

    private void Reset_Timer()
    {
        if (!Inventory.Instance.InventoryFull())
        {
            PlayerManager.Instance.AddExperience(Count * Resource_Rate);
            Inventory.Instance.AddItem(int.Parse(Item_ID), (int)(Count * Resource_Rate));
            On_Complete = true;
        }
    }

    #endregion
}
