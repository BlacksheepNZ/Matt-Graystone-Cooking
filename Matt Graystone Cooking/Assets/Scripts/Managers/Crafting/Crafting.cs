using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;

public class CraftingCost
{
    public ResourceType ResourceType;
    public int Count;
}

[System.Serializable]
public class Crafting
{
    #region fields

    public Text NameText;

    public string Name;
    public int ID;
    public Sprite BorderImage;

    public ItemRarity ItemRarity;

    public ItemType ItemType;

    public ResourceType ResourceType = ResourceType.Empty;

    public Button ButtonClick;

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

    protected int[] _lootProbabilites = new int[] { 50, 70, 85, 95, 100 };
    protected const int MaxProbability = 100;

    public int NumberOfstats = 0;

    public Dropdown DropDownBox1;
    public Dropdown DropDownBox2;

    private BonusType BonusType = BonusType.Empty;

    #endregion

    public float InitialCost = 0;
    public int CraftingCount = -1;
    public float Coefficient = 0;
    public float InitialTime = 0;


    public Text CostText;
    public List<Bonus> Bonus = new List<global::Bonus>();

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

    public void GetChance()
    {
        for (int n = 0; n < 1000; n++)
            Debug.Log(ChooseItemRarity());
    }

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

    private float AddBonus(BonusType BonusType, ResourceType ResourceType)
    {
        for (int i = 0; i < Bonus.Count; i++)
        {
            if (Bonus[i].BonusType == BonusType.DecreasePurchaseCost && BonusType == BonusType.DecreasePurchaseCost)
            {
                if (Bonus[i].ResourceType == ResourceType)
                {
                    float B_DecreseCost = ItemPercent(Bonus[i].MinValue, Bonus[i].MaxValue);
                    return B_DecreseCost;
                }
            }
            if (Bonus[i].BonusType == BonusType.DecreseSpeed && BonusType == BonusType.DecreseSpeed)
            {
                if (Bonus[i].ResourceType == ResourceType)
                {
                    float B_DecreaseTimeToCompleteTask = ItemPercent(Bonus[i].MinValue, Bonus[i].MaxValue);
                    return B_DecreaseTimeToCompleteTask;
                }
            }
            if (Bonus[i].BonusType == BonusType.IncreaseCurrency && BonusType == BonusType.IncreaseCurrency)
            {
                if (Bonus[i].ResourceType == ResourceType)
                {
                    float B_IncreaseCurrencyRewardRate = ItemPercent(Bonus[i].MinValue, Bonus[i].MaxValue);
                    return B_IncreaseCurrencyRewardRate;
                }
            }
            if (Bonus[i].BonusType == BonusType.IncreaseResource && BonusType == BonusType.IncreaseResource)
            {
                if (Bonus[i].ResourceType == ResourceType)
                {
                    float B_IncreaseResourceReward = ItemPercent(Bonus[i].MinValue, Bonus[i].MaxValue);
                    return B_IncreaseResourceReward;
                }
            }

            if (Bonus[i].BonusType == BonusType.CritChance && BonusType == BonusType.CritChance)
            {
                if (Bonus[i].ResourceType == ResourceType)
                {
                    float B_IncreaseCritChance = ItemPercent(Bonus[i].MinValue, Bonus[i].MaxValue);
                    return B_IncreaseCritChance;
                }
            }

            if (Bonus[i].BonusType == BonusType.MineNextTeir && BonusType == BonusType.MineNextTeir)
            {
                if (Bonus[i].ResourceType == ResourceType)
                {
                    float B_MineNextTeir = ItemPercent(Bonus[i].MinValue, Bonus[i].MaxValue);
                    return B_MineNextTeir;
                }
            }

            //if (Bonus[i].BonusType == BonusType.ReduceUpgradeCost && BonusType == BonusType.ReduceUpgradeCost)
            //{
            //    if (Bonus[i].ResourceType == ResourceType)
            //    {
            //        // = ItemPercent(Bonus[i].MinValue, Bonus[i].MaxValue);
            //        return;
            //    }
            //}
        }

        return 0;
    }

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

        for (int i = 0; i < NumberOfstats; i++)
        {
            BonusType value = (BonusType)array[i];
            float amount = AddBonus(value, (ResourceType)DropDownBox2.value);
            //item.AddBonus(value, amount);
        }
    }

    public ItemRarity ChooseItemRarity()
    {
        ItemRarity lootType = 0;
        int randomValue = UnityEngine.Random.Range(0, MaxProbability);
        while (_lootProbabilites[(int)lootType] <= randomValue)
        {
            lootType++;
        }
        return lootType;
    }

    private void CostToCraft(int ItemID, int cost)
    {
        Item itemToRemove = ItemDatabase.Instance.FetchItemByID(ItemID);
        int itemcount = Inventory.Instance.CheckItemCount(itemToRemove.ID);
        if (itemcount >= cost)
        {
            Inventory.Instance.RemoveItem(itemToRemove.ID, cost);
            Debug.Log("Removed Item Cost");
        }
        else
        {
            Debug.Log("You need more resources.");
        }
    }

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

    private float ItemPercent(float min, float max)
    {
        switch (ItemRarity)
        {
            default:
                return 0;
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
    }

    private float MinMax(float min, float max)
    {
        return UnityEngine.Random.Range(min, max);
    }
}
