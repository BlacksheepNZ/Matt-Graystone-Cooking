using System;

/// <summary>
/// 
/// </summary>
[Serializable]
public class ItemSellValue
{
    /// <summary>
    /// 
    /// </summary>
    public ResourceType ResourceType;

    /// <summary>
    /// 
    /// </summary>
    public ItemRarity ItemRarity;

    /// <summary>
    /// 
    /// </summary>
    public int Value;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    public ItemSellValue(ResourceType resourceType,
                         ItemRarity itemRarity,
                         int value)
    {
        ResourceType = resourceType;
        ItemRarity = itemRarity;
        Value = value;
    }
}
