using System;

/// <summary>
/// 
/// </summary>
[Serializable]
public class MineralSellValue
{
    /// <summary>
    /// 
    /// </summary>
    public ResourceType ResourceType;

    /// <summary>
    /// 
    /// </summary>
    public int Value;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    public MineralSellValue(ResourceType resourceType,
                            int value)
    {
        ResourceType = resourceType;
        Value = value;
    }
}
