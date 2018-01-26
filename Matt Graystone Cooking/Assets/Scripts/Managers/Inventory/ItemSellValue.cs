using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ItemSellValue
{
    public ResourceType ResourceType;
    public ItemRarity ItemRarity;
    public int Value;

    public ItemSellValue(
        ResourceType resourceType,
        ItemRarity itemRarity,
        int value)
    {
        ResourceType = resourceType;
        ItemRarity = itemRarity;
        Value = value;
    }
}
