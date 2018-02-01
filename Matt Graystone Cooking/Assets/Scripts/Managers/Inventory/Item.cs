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

    public Item()
    {
        ID = 000;
    }

    string nl = "\n";

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
