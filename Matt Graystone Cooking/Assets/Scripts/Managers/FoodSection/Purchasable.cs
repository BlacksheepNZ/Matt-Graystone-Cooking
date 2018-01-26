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
    public float ResourceRate;

    public ResourceType ResourceType;

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

    public float B_DecreseCost = 1;
    public float B_IncreaseResourceRate;

    public float B_IncreaseCurrencyRewardRate;
    public float B_DecreaseTimeToCompleteTask;

    public bool IsPurchased;

    public Purchasable(
        int id, 
        string itemName,
     float baseCost,
     Sprite image,
     float coefficent,
     int count,
     float resourceRate,
     ResourceType resourceTypeReward,
     float timeToCompleteTask)
    {
        ID = id;
        ItemName = itemName;
        BaseCost = baseCost;
        Image = image;
        Coefficent = coefficent;
        Count = count;
        ResourceRate = resourceRate;
        ResourceType = resourceTypeReward;
        TimeToCompleteTask = timeToCompleteTask;

        Cost = BaseCost;
        IsPurchased = false;
    }

    public bool CanPurchase()
    {
        float value = DecreaseValue(Cost, B_DecreseCost);
        if (Game.Instance.CanPurchase(value))
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

        float valueResource = IncreaseValue(ResourceRate, B_IncreaseResourceRate);

        Text_Reward.text = "+ " + CurrencyConverter.Instance.GetCurrencyIntoString(valueResource);
        ValueText.text = CurrencyConverter.Instance.GetCurrencyIntoStringNoSign(ResourceRate * Count) + DisplayText(B_IncreaseResourceRate);

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
        float cost = DecreaseValue(Cost, B_DecreseCost);

        float value = 0;
        for (int i = 0; i < CostToPurchaseAmount; i++)
        {
            value += cost * Coefficent;
        }

        return value;
    }

    public void Upgrade()
    {
        float cost = DecreaseValue(Cost, B_DecreseCost);
        float value = CostToBuy();

        if (Game.Instance.CanPurchase(value))
        {
            Game.Instance.RemoveGold(value);

            for (int i = 0; i < CostToPurchaseAmount; i++)
            {
                Cost += cost * Coefficent;
            }

            Count += CostToPurchaseAmount;
        }
    }

    public void AddRewards(Item itemToAdd)
    {
        B_DecreseCost = itemToAdd.B_DecreseCost;
        B_IncreaseResourceRate = itemToAdd.B_IncreaseResourceRate;
        B_IncreaseCurrencyRewardRate = itemToAdd.B_IncreaseCurrencyRewardRate;
        B_DecreaseTimeToCompleteTask = itemToAdd.B_DecreaseTimeToCompleteTask;
    }

    public void RemoveRewards()
    {
        B_DecreseCost = 0;
        B_IncreaseResourceRate = 0;
        B_IncreaseCurrencyRewardRate = 0;
        B_DecreaseTimeToCompleteTask = 0;
    }

    #region Timer

    public bool StartedTimer;
    public float CurrentTime = 0;

    public IEnumerator UpdateTimer()
    {
        StartedTimer = true;
        OnComplete = false;

        float value = DecreaseValue(TimeToCompleteTask, B_DecreaseTimeToCompleteTask);

        float speed = (Time.fixedDeltaTime / value);

        while (ProgressionBar.Value < 1)
        {
            ProgressionBar.Value += speed;
            CurrentTime = ProgressionBar.Value;

            yield return null;
        }

        ProgressionBar.Value = 0;
        ResetTimer();
    }

    private void ResetTimer()
    {
        float valueResource = IncreaseValue(ResourceRate, B_IncreaseResourceRate);

        if (!Inventory.Instance.InventoryFull())
        {
            Inventory.Instance.AddItem((int)ResourceType, (int)(Count * valueResource));
            OnComplete = true;
        }
    }

    #endregion
}
