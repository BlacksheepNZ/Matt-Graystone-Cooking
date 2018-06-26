using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class CraftingData : MonoBehaviour
{
    public Crafting Crafting;

    public void Start()
    {
        if (StartedTimer == true)
        {
            Crafting.ProgressionBar.Value = CurrentTime;
            StartCoroutine(UpdateTimer());
        }
    }

    public void ResourceType()
    {
        for (int i = 0; i < SaveLoad.Instance.Recipe_Cost.Count; i++)
        {
            RecipeCost CraftingCost = SaveLoad.Instance.Recipe_Cost[i];

            if (Crafting.ResourceType == CraftingCost.ResourceType)
            {
                Crafting.InitialCost = CraftingCost.InitialCost;

                Crafting.Coefficient = CraftingCost.Coefficient;
                Crafting.InitialTime = CraftingCost.InitialTime;
            }
        }
    }

    public bool ItemCrafted;

    #region Timer

    public bool OnComplete;
    public bool StartedTimer;
    public float CurrentTime = 0;

    public IEnumerator UpdateTimer()
    {
        Crafting.ButtonClick.interactable = false;
        Crafting.DropDownBox1.interactable = false;
        Crafting.DropDownBox2.interactable = false;

        StartedTimer = true;
        OnComplete = false;

        float distance = 1;

        float speed = (Time.fixedDeltaTime / Crafting.InitialTime);

        while (Crafting.ProgressionBar.Value < distance)
        {
            Crafting.ProgressionBar.Value += speed;
            CurrentTime = Crafting.ProgressionBar.Value;

            yield return null;
        }

        Crafting.ButtonClick.interactable = true;
        Crafting.DropDownBox1.interactable = true;
        Crafting.DropDownBox2.interactable = true;
        Crafting.ProgressionBar.Value = 0;
        ResetTimer();
    }

    void ResetTimer()
    {
        if (!Inventory.Instance.InventoryFull())
        {
            AddItem();
            OnComplete = true;
            StartedTimer = false;
        }
    }

    #endregion

    public void AddItem()
    {
        //todo these will change on user input
        Crafting.ItemRarity = ItemRarity.Legendary;
        Crafting.ItemType = ItemType.Item;
        Crafting.ResourceType = global::ResourceType.Food_1_Pastry;
        ////

        Crafting.ID = SaveLoad.Instance.Item_Database.Count;

        Crafting.ItemRarity = Crafting.ChooseItemRarity();
        Crafting.NumberOfstats = Resource.ItemNumberOFStats(Crafting.ItemRarity);

        Crafting.Name = Crafting.ResourceType.ToString() + " " + Crafting.ItemType.ToString() + " of " + Crafting.ItemRarity.ToString();

        Crafting.BorderImage = Crafting.GetBorderImageFromRarity(SaveLoad.Instance.RarityImages);

        Item item = new Item(
            Crafting.Name,
            Crafting.ID,
            Crafting.BorderImage,
            Crafting.DropDownBox1.value,
            false,
            Crafting.ItemRarity,
            Crafting.ItemType,
            Crafting.ResourceType);

        Crafting.GenerateModifers(item);

        SaveLoad.Instance.Item_Database.Add(item);

        Inventory.Instance.AddItemToSlot(Inventory.Instance.GetEmptySlot(), Crafting.ID, 1);

        Crafting.CraftingCount++;
    }

    public void OnClick()
    {
        if (!StartedTimer)
        {
            if (!Inventory.Instance.InventoryFull())
            {
                for (int i = 0; i < SaveLoad.Instance.Pastry_Purchasable.Count; i++)
                {
                    float cost = Crafting.CostToCraft();

                    if (Crafting.CheckCost(cost))
                    {
                        //Inventory.Instance.RemoveItem((int)Crafting.ResourceType, (int)cost);

                        StartCoroutine(UpdateTimer());

                        break;
                    }
                    else
                    {
                        Debug.Log("need more resources");
                    }
                }
            }
            else
            {
                Debug.Log("Inventory Full");
            }
        }
    }
}
