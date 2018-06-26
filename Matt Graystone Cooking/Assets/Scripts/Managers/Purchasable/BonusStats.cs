using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum BonusType
{
    Decrese_Cost,
    Increase_Resource_Rate,
    Increase_Currency_Reward_Rate,
    Decrease_Time_To_Complete_Task,
    Increase_Speed,
}

/// <summary>
/// 
/// </summary>
public class BonusStats
{
    public float Decrese_Cost
    {
        get { return decreaseCost; }
    }
    private float decreaseCost;

    public float Increase_Resource_Rate
        {
        get { return increaseResourceRate; }
    }
    private float increaseResourceRate;

    public float Increase_Currency_Reward_Rate
    {
        get { return increaseCurrencyRewardRate; }
    }
    private float increaseCurrencyRewardRate;

    public float Decrease_Time_To_Complete_Task
    {
        get { return decreaseTimeToCompleteTask; }
    }
    private float decreaseTimeToCompleteTask;

    public float Increase_Speed
    {
        get { return increaseSpeed; }
    }
    private float increaseSpeed;

    /// <summary>
    /// 
    /// </summary>
    public BonusStats()
    {
        Clear();
    }

    public void Add(BonusType typeb, float value)
    {
        switch(typeb)
        {
            case BonusType.Decrese_Cost:
                decreaseCost = value;
                break;
            case BonusType.Increase_Resource_Rate:
                increaseResourceRate = value;
                break;
            case BonusType.Increase_Currency_Reward_Rate:
                increaseCurrencyRewardRate = value;
                break;
            case BonusType.Decrease_Time_To_Complete_Task:
                decreaseTimeToCompleteTask = value;
                break;
            case BonusType.Increase_Speed:
                increaseSpeed = value;
                break;
        }

    }

    public void Remove(BonusType typeb, float value)
    {
        switch (typeb)
        {
            case BonusType.Decrese_Cost:
                decreaseCost = 0;
                break;
        }

    }

    public void Clear()
    {
        decreaseCost = 0;
        increaseResourceRate = 0;
        increaseCurrencyRewardRate = 0;
        decreaseTimeToCompleteTask = 0;
        increaseSpeed = 0;
    }

    /// <summary>
    /// 
    /// </summary>
    public override string ToString()
    {
        string nl = "\n";
        string value = "";

        if (Decrese_Cost != 0)
            value += "Decrese Cost By " + Display_Text(Decrese_Cost) + nl;

        if (Increase_Resource_Rate != 0)
            value += "Increase Resource Reward By " + Display_Text(Increase_Resource_Rate) + nl;

        if (Increase_Currency_Reward_Rate != 0)
            value += "Increase Currency Reward By " + Display_Text(Increase_Currency_Reward_Rate)  + nl;

        if (Decrease_Time_To_Complete_Task != 0)
            value += "Decrease Time To Complete Task By " + Display_Text(Decrease_Time_To_Complete_Task)  + nl;

        return value;
    }

    /// <summary>
    /// 
    /// </summary>
    private string Display_Text(float bonus)
    {
        if (bonus != 0)
            return "<color=#0473f0><b>" + CurrencyConverter.Instance.GetCurrencyIntoString(bonus) + "%</b></color>";
        else
            return "";
    }
}
