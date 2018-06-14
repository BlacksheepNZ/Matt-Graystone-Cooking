using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
[Serializable]
public class RecipeItem
{
    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    public int ItemID;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    public int Count;

    /// <summary>
    /// 
    /// </summary>
    public RecipeItem(int itemID, 
                      int count)
    {
        ItemID = itemID;
        Count = count;
    }

    /// <summary>
    /// 
    /// </summary>
    public RecipeItem()
    {
    }
}

/// <summary>
/// 
/// </summary>
[Serializable]
public class Recipe
{
    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    public string Name;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    public bool Unlocked;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    public List<RecipeItem> Items = new List<RecipeItem>();

    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    public int SellValue;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    public string Key;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    public Sprite Preview_Image;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    public Recipe(string name, 
                  string key, 
                  Sprite preview_image, 
                  List<RecipeItem> item, 
                  int sell_value)
    {
        Name = name;
        Key = key;
        Preview_Image = preview_image;
        Items = item;
        SellValue = sell_value;
    }

    /// <summary>
    /// Use this for initialization
    /// </summary>
    public Recipe() { }

    /// <summary>
    /// 
    /// </summary>
    public override string ToString()
    {
        return Name;
    }
}
