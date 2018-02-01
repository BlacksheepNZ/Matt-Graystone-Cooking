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
    public int CostToPurchaseAmount;

    public Sprite Image;

    public int ID;
    public string ItemName;
    public float BaseCost;
    public float Cost;
    public float Coefficent;
    public int Count;
    public float Resource_Rate;

    public string ItemID;

    public float TimeToCompleteTask = 1.0f;

    public ProgressionBar ProgressionBar;

    public Button ButtonFirstTimePurchase;
    public Button ButtonUpgrade;
    //public Button ButtonImage;
    //public Button ButtonUpgradePurchase;

    public bool OnComplete;
    public bool Unlocked = false;
    /// <summary>
    /// Bonus
    /// </summary>

    public bool IsPurchased;

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
        ItemName = itemName;
        BaseCost = baseCost;
        Image = image;
        Coefficent = coefficent;
        Count = count;
        Resource_Rate = resourceRate;
        ItemID = itemID;
        TimeToCompleteTask = timeToCompleteTask;

        Cost = BaseCost;
        IsPurchased = false;
    }

    public bool CanPurchase()
    {
        if (Game.Instance.CanPurchase(Cost))
        {
            return true;
        }

        return false;
    }

    public void Update()
    {
        CostToPurchase.text = CurrencyConverter.Instance.GetCurrencyIntoString(CostToBuy());
        ItemNameText.text = ItemName;
        CountText.text = "LVL. " + Count.ToString("0");

        Text_Reward.text = "+ " + CurrencyConverter.Instance.GetCurrencyIntoString(Resource_Rate);
        ValueText.text = CurrencyConverter.Instance.GetCurrencyIntoStringNoSign(Resource_Rate * Count);

        //ProgressionBar.progressText.text = CurrencyConverter.Instance.GetCurrencyIntoStringNoSign(ProgressionBar.Current) + "/" + CurrencyConverter.Instance.GetCurrencyIntoStringNoSign(ProgressionBar.Max) + DisplayText(B_DecreaseTimeToCompleteTask);
    }

    private string DisplayText(float bonus)
    {
        if (bonus != 0)
            return "<color=#0473f0><b> (" + CurrencyConverter.Instance.GetCurrencyIntoString(bonus) + "%) </b></color>";
        else
            return "";
    }

    public float DecreaseValue(float cost, float decrease)
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
    public float IncreaseValue(float cost, float increase)
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

    public void FirstTimePurchase()
    {
        if (CanPurchase() == true)
        {
            Game.Instance.RemoveGold(Cost);
            IsPurchased = true;
            Unlocked = true;
        }
    }

    public float CostToBuy()
    {
        float value = 0;
        for (int i = 0; i < CostToPurchaseAmount; i++)
        {
            value += Cost * Coefficent;
        }

        return value;
    }

    public void Upgrade()
    {
        float value = CostToBuy();

        if (Game.Instance.CanPurchase(value))
        {
            Game.Instance.RemoveGold(value);

            for (int i = 0; i < CostToPurchaseAmount; i++)
            {
                Cost += Cost * Coefficent;
            }

            Count += CostToPurchaseAmount;
        }
    }

    #region Timer

    public bool Started_Timer;
    public float Current_Time = 0;

    public IEnumerator UpdateTimer()
    {
        Started_Timer = true;
        OnComplete = false;

        float speed = (Time.fixedDeltaTime / TimeToCompleteTask);

        while (ProgressionBar.Value < 1)
        {
            ProgressionBar.Value += speed;
            Current_Time = ProgressionBar.Value;

            yield return null;
        }

        ProgressionBar.Value = 0;
        ResetTimer();
    }

    private void ResetTimer()
    {
        if (!Inventory.Instance.InventoryFull())
        {
            PlayerManager.Instance.AddExperience(Count * Resource_Rate);
            Inventory.Instance.AddItem(int.Parse(ItemID), (int)(Count * Resource_Rate));
            OnComplete = true;
        }
    }

    #endregion
}
