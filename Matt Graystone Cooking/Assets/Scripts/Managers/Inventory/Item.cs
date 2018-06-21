using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
[Serializable]
public class Item
{
    /// <summary>
    /// 
    /// </summary>
    public string Name
    {
        get;
        set;
    }

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
    public int ImageID;

    /// <summary>
    /// 
    /// </summary>
    public bool Stackable;

    /// <summary>
    /// 
    /// </summary>
    public ItemRarity ItemRarity;

    /// <summary>
    /// 
    /// </summary>
    public ItemType ItemType;

    /// <summary>
    /// 
    /// </summary>
    public ResourceType ResourceType;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    public Item()
    {
        ID = 000;
    }

    /// <summary>
    /// Use this for initialization
    /// </summary>
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

    /// <summary>
    /// Use this for initialization
    /// </summary>
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

    /// <summary>
    /// 
    /// </summary>
    private List<RecipeData> recipeData = new List<RecipeData>();

    /// <summary>
    /// 
    /// </summary>
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

    /// <summary>
    /// Retuns the item decription.
    /// </summary>
    public override string ToString()
    {
        string decription = "Used to craft in \n";

        for (int i = 0; i < recipeData.Count; i++)
        {
            decription += recipeData[i].recipe.Name + "\n";
        }

        return decription;
    }

    /// <summary>
    /// 
    /// </summary>
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

    /// <summary>
    /// 
    /// </summary>
    void LevelUpItem(ItemRarity outputRarity)
    {
        ItemRarity = outputRarity;
    }
}
