using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class MineralSellValue
{
    public ResourceType ResourceType;
    public int Value;

    public MineralSellValue(
        ResourceType resourceType,
        int value)
    {
        ResourceType = resourceType;
        Value = value;
    }
}
