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
    }

    private List<RecipeData> recipeData = new List<RecipeData>();

    public Item()
    {
        //ID = 000;
    }

    string nl = "\n";

    public void GetAttachedRecipes()
    {
        recipeData.Clear();

        List<RecipeData> totalRecipeData = SaveLoad.Instance.Recipe_Data;
        for (int i = 0; i < totalRecipeData.Count; i++)
        {
            RecipeData recipe = totalRecipeData[i];
            for (int x = 0; x < recipe.recipe.Items.Count; x++)
            {
                RecipeItem recipeItem = recipe.recipe.Items[x];
                if (recipeItem.ItemID == ID)
                {
                    recipeData.Add(recipe);
                }
            }
        }
    }

    public string GetDecription()
    {
        string decription = "Used to craft in \n";

        for (int i = 0; i < recipeData.Count; i++)
        {
            decription += recipeData[i].recipe.Name + "\n";
        }

        return decription;
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
