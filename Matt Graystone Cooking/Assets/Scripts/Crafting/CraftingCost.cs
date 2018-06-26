using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class RecipeCost
{
    public ResourceType ResourceType;
    public float InitialCost;
    public float Coefficient;
    public float InitialTime;

    public RecipeCost(
        ResourceType resourceType,
        float initialCost,
        float coefficient,
        float initialTime)
    {
        ResourceType = resourceType;
        InitialCost = initialCost;
        Coefficient = coefficient;
        InitialTime = initialTime;
    }
}
