using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[System.Serializable]
public class RecipeItem
{
    [SerializeField]
    public int ItemID;
    [SerializeField]
    public int Count;

    public RecipeItem(int itemID, int count)
    {
        ItemID = itemID;
        Count = count;
    }

    public RecipeItem()
    {
    }
}

[System.Serializable]
public class Recipe
{
    [SerializeField]
    public string Name;
    [SerializeField]
    public bool Unlocked;
    [SerializeField]
    public List<RecipeItem> Items = new List<RecipeItem>();
    [SerializeField]
    public int SellValue;
    [SerializeField]
    public string Key;
    [SerializeField]
    public Sprite Preview_Image;

    public Recipe(string name, string key, Sprite preview_image, List<RecipeItem> item, int sell_value)
    {
        Name = name;
        Key = key;
        Preview_Image = preview_image;
        Items = item;
        SellValue = sell_value;
    }

    public Recipe()
    {
    }
}
