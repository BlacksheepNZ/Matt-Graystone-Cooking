using System;

/// <summary>
/// 
/// </summary>
[Serializable]
public class Bonus
{
    /// <summary>
    /// 
    /// </summary>
    public BonusType BonusType;
    /// <summary>
    /// 
    /// </summary>
    public ResourceType ResourceType;
    /// <summary>
    /// 
    /// </summary>
    public float MinValue;
    /// <summary>
    /// 
    /// </summary>
    public float MaxValue;

    /// <summary>
    /// 
    /// </summary>
    public Bonus(BonusType bonusType, ResourceType resourceType, float minValue, float maxValue)
    {
        BonusType = bonusType;
        ResourceType = resourceType;
        MinValue = minValue;
        MaxValue = maxValue;
    }
}
