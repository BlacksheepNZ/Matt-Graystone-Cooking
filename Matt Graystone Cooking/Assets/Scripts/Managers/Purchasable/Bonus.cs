using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;

public class Bonus
{
    public BonusType BonusType;
    public ResourceType ResourceType;
    public float MinValue;
    public float MaxValue;

    public Bonus(BonusType bonusType, ResourceType resourceType, float minValue, float maxValue)
    {
        BonusType = bonusType;
        ResourceType = resourceType;
        MinValue = minValue;
        MaxValue = maxValue;
    }
}
