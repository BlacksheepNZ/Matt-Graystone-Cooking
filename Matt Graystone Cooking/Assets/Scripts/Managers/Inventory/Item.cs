using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

[System.Serializable]
public class Item
{
    public string Name
    {
        get;
        set;
    }
    public int ID;
    public Sprite BorderImage;
    public int ImageID;

    public bool Stackable;

    public ItemRarity ItemRarity;
    public ItemType ItemType;
    public ResourceType ResourceType;

    /// <summary>
    /// Bonus
    /// </summary>

    public float B_DecreseCost;
    public float B_IncreaseResourceRate;

    public float B_IncreaseCurrencyRewardRate;
    public float B_DecreaseTimeToCompleteTask;

    public float B_IncreaseCritChance;
    public float B_MineNextTeir;

    public float B_Speed;

    public List<Tuple<BonusType, float>> BonusAttached;
    public BonusType BonusType;
    public float B_Amount;

    public Item(string name,
        int id, 
        Sprite borderImage, 
        int imageID, 
        bool stackable, 
        ItemRarity itemRarity, 
        ItemType itemType,
        ResourceType resourceType)
    {
        Name = name;
        ID = id;
        BorderImage = borderImage;
        ImageID = imageID;
        Stackable = stackable;
        ItemRarity = itemRarity;
        ItemType = itemType;
        ResourceType = resourceType;

        BonusAttached = new List<Tuple<BonusType, float>>();
        AddBonus(BonusType, B_Amount);
    }

    public Item(string name,
    int id,
    int imageID,
    bool stackable,
    ItemRarity itemRarity,
    ItemType itemType,
    ResourceType resourceType)
    {
        Name = name;
        ID = id;
        ImageID = imageID;
        Stackable = stackable;
        ItemRarity = itemRarity;
        ItemType = itemType;
        ResourceType = resourceType;

        BonusAttached = new List<Tuple<BonusType, float>>();
        AddBonus(BonusType, B_Amount);
    }

    public void AddBonus(BonusType BonusType, float b_amount)
    {
        switch(BonusType)
        {
            case BonusType.Empty:
                break;
            case BonusType.DecreseSpeed:
                B_DecreseCost = b_amount;
                break;
            case BonusType.DecreasePurchaseCost:
                B_DecreaseTimeToCompleteTask = b_amount;
                break;
            case BonusType.IncreaseCurrency:
                B_IncreaseCurrencyRewardRate = b_amount;
                break;
            case BonusType.CritChance:
                B_IncreaseCritChance = b_amount;
                break;
            case BonusType.MineNextTeir:
                B_MineNextTeir = b_amount;
                break;
            case BonusType.PowerNode:
                B_Speed = b_amount;
                break;
        }

        BonusAttached.Add(new Tuple<BonusType, float>(BonusType, b_amount));
    }

    public Item()
    {
        ID = 000;
    }

    string nl = "\n";

    public string GetDecription()
    {
        string value = "";

        if (B_DecreseCost != 0)
            value += "Decrese Cost By " + B_DecreseCost.ToString("0.00") + "%" + nl;

        if (B_IncreaseResourceRate != 0)
            value += "Increase Resource Reward By " + B_IncreaseResourceRate.ToString("0.00") + "%" + nl;

        if (B_IncreaseCurrencyRewardRate != 0)
            value += "Increase Currency Reward By " + B_IncreaseCurrencyRewardRate.ToString("0.00") + "%" + nl;

        if (B_DecreaseTimeToCompleteTask != 0)
            value += "Decrease Time To Complete Task By " + B_DecreaseTimeToCompleteTask.ToString("0.00") + "%" + nl;

        if (B_IncreaseCritChance != 0)
            value += "Increase Crit Chance By " + B_IncreaseCritChance.ToString("0.00") + "%" + nl;

        if (B_MineNextTeir != 0)
            value += "Mine Next Teir Chance By " + B_MineNextTeir.ToString("0.00") + "%" + nl;

        return value;
    }

    public void Upgrade(ItemRarity itemRarity)
    {
        switch(itemRarity)
        {
            case ItemRarity.Uncommon:
                LevelUpItem(ItemRarity.Common);
                break;
            case ItemRarity.Common:
                LevelUpItem(ItemRarity.Rare);
                break;
            case ItemRarity.Rare:
                LevelUpItem(ItemRarity.Epic);
                break;
            case ItemRarity.Epic:
                LevelUpItem(ItemRarity.Legendary);
                break;
            case ItemRarity.Legendary:
                break;
        }
    }

    void LevelUpItem(ItemRarity outputRarity)
    {
        ItemRarity = outputRarity;
    }
}
