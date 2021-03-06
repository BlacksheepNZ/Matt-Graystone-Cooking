﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;

/// <summary>
/// 
/// </summary>
public class CraftingCost
{
    /// <summary>
    /// 
    /// </summary>
    public ResourceType ResourceType;

    /// <summary>
    /// 
    /// </summary>
    public int Count;
}

/// <summary>
/// 
/// </summary>
[System.Serializable]
public class Crafting
{
    /// <summary>
    /// GUI
    /// </summary>
    public Text NameText;

    /// <summary>
    /// 
    /// </summary>
    public string Name;

    /// <summary>
    /// 
    /// </summary>
    public int ID;

    /// <summary>
    /// 
    /// </summary>
    public Sprite BorderImage;

    /// <summary>
    /// 
    /// </summary>
    public ItemRarity ItemRarity;

    /// <summary>
    /// 
    /// </summary>
    public SlotType SlotType;

    /// <summary>
    /// 
    /// </summary>
    public ResourceType ResourceType = ResourceType.Empty;

    /// <summary>
    /// 
    /// </summary>
    public Button ButtonClick;

    /// <summary>
    /// 
    /// </summary>
    public ProgressionBar ProgressionBar;

    /// <summary>
    /// Bonus
    /// </summary>
    /// 
    //Common, 50 - 0 = 50%
    //Uncommon, 70 - 50 = 20%
    //Rare, 85 - 70 = 15%
    //Epic, 95 - 85 = 10%
    //Legendary, 100-95 = 5%

    /// <summary>
    ///Common ( 50 to 0 = 50% )
    ///Uncommon ( 70 to 50 = 20% )
    ///Rare ( 85 to 70 = 15% )
    ///Epic ( 95 to 85 = 10% )
    ///Legendary ( 100 to 95 = 5% )
    /// </summary>
    protected int[] LootProbabilites = new int[] { 50, 70, 85, 95, 100 };

    /// <summary>
    /// MaxProbability
    /// </summary>
    protected const int MAX_PROBABILITY = 100;

    /// <summary>
    /// 
    /// </summary>
    public int NumberOfstats = 0;

    /// <summary>
    /// 
    /// </summary>
    public Dropdown DropDownBox1;

    /// <summary>
    /// 
    /// </summary>
    public Dropdown DropDownBox2;

    /// <summary>
    /// 
    /// </summary>
    public float InitialCost = 0;

    /// <summary>
    /// 
    /// </summary>
    public int CraftingCount = -1;

    /// <summary>
    /// 
    /// </summary>
    public float Coefficient = 0;

    /// <summary>
    /// 
    /// </summary>
    public float InitialTime = 0;

    /// <summary>
    /// 
    /// </summary>
    public Text CostText;

    /// <summary>
    /// 
    /// </summary>
    public BonusStats BonusStats = new BonusStats();

    /// <summary>
    /// Interface updates on call
    /// </summary>
    public IEnumerator Update()
    {
        while (true)
        {
            NameText.text = ResourceType.ToString();

            float itemCount = Inventory.Instance.CheckItemCount((int)ResourceType);
            CostText.text = CurrencyConverter.Instance.GetCurrencyIntoStringNoSign(itemCount) + "/" +
                CurrencyConverter.Instance.GetCurrencyIntoStringNoSign(CostToCraft());

            yield return null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void GetChance()
    {
        for (int n = 0; n < 1000; n++)
            Debug.Log(ChooseItemRarity());
    }

    /// <summary>
    /// 
    /// </summary>
    public bool CheckCost(float cost)
    {
        float itemCount = Inventory.Instance.CheckItemCount((int)ResourceType);

        if (itemCount >= cost)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public float CostToCraft()
    {
        if (CraftingCount <= 0)
        {
            return InitialCost;
        }
        else
        {
            return (InitialCost * CraftingCount) * Coefficient;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void GenerateModifers(Item item)
    {
        List<int> array = new List<int>();

        int count = Enum.GetNames(typeof(BonusType)).Length;
        for (int i = 0; i < count; i++)
        {
            array.Add(i);
        }

        System.Random rnd = new System.Random();
        Extensions.Shuffle(rnd, array);

        //if we have more stats than bonus
        if(NumberOfstats > count)
        {
            array.RemoveRange(NumberOfstats, count);
        }

        for (int i = 0; i < NumberOfstats; i++)
        {
            item.AddBonus((BonusType)array[i], 50);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public ItemRarity ChooseItemRarity()
    {
        ItemRarity lootType = 0;
        int randomValue = UnityEngine.Random.Range(0, MAX_PROBABILITY);
        while (LootProbabilites[(int)lootType] <= randomValue)
        {
            lootType++;
        }
        return lootType;
    }

    /// <summary>
    /// 
    /// </summary>
    private void CostToCraft(int ItemID, int cost)
    {
        Item itemToRemove = ItemDatabase.Instance.FetchItemByID(ItemID);
        int itemcount = Inventory.Instance.CheckItemCount(itemToRemove.ID);
        if (itemcount >= cost)
        {
            //Inventory.Instance.RemoveItem(itemToRemove.ID, cost);
            Debug.Log("Removed Item Cost");
        }
        else
        {
            Debug.Log("You need more resources.");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public Sprite GetBorderImageFromRarity(List<Sprite> sprite)
    {
        try
        {
            switch (ItemRarity)
            {
                default:
                    return sprite[0];
                case ItemRarity.Common:
                    return sprite[0];
                case ItemRarity.Uncommon:
                    return sprite[1];
                case ItemRarity.Rare:
                    return sprite[2];
                case ItemRarity.Epic:
                    return sprite[3];
                case ItemRarity.Legendary:
                    return sprite[4];
            }
        }
        catch
        {
            return sprite[0];
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private float ItemPercent(float min, float max)
    {
        switch (ItemRarity)
        {
            case ItemRarity.Common:
                return MinMax(min, max);
            case ItemRarity.Uncommon:
                return MinMax(min, max);
            case ItemRarity.Rare:
                return MinMax(min, max);
            case ItemRarity.Epic:
                return MinMax(min, max);
            case ItemRarity.Legendary:
                return MinMax(min, max);
        }

        return 0;
    }

    /// <summary>
    /// 
    /// </summary>
    private float MinMax(float min, float max)
    {
        return UnityEngine.Random.Range(min, max);
    }
}
