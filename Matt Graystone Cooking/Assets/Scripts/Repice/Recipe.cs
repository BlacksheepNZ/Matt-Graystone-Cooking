using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[System.Serializable]
public class RecipeItem
{
    public int ItemID;
    public int Count;

    public RecipeItem(int itemID, int count)
    {
        ItemID = itemID;
        Count = count;
    }
}

[System.Serializable]
public class Recipe
{
    public string Name;
    public bool Unlocked;

    public List<RecipeItem> Items = new List<RecipeItem>();

    public int ItemIDToRemove;

    public int SellValue;

    public string Key;

    public Recipe(string name, string key, List<RecipeItem> item, int sell_value)
    {
        Name = name;
        Key = key;
        Items = item;
        SellValue = sell_value;
    }
}
