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
    public int ItemIDToRemove;
    [SerializeField]
    public int SellValue;
    [SerializeField]
    public string Key;

    public Recipe(string name, string key, List<RecipeItem> item, int sell_value)
    {
        Name = name;
        Key = key;
        Items = item;
        SellValue = sell_value;
    }

    public Recipe()
    {
    }
}
