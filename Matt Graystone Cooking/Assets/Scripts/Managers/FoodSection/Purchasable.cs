using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// 
/// </summary>
[Serializable]
public class Purchasable
{
    /// <summary>
    /// GUI
    /// </summary>
    public Text GUI_Text_ID;

    /// <summary>
    /// GUI
    /// </summary>
    public Text GUI_Text_Item_Name;

    /// <summary>
    /// GUI
    /// </summary>
    public Text GUI_Text_Count;

    /// <summary>
    /// GUI
    /// </summary>
    public Text GUI_Text_Value;

    /// <summary>
    /// 
    /// </summary>
    public Image GUI_Image_Potrait;

    /// <summary>
    /// GUI
    /// </summary>
    public Button GUI_Button_Upgrade;

    /// <summary>
    /// GUI
    /// </summary>
    public Text GUI_Text_Reward;

    /// <summary>
    /// GUI
    /// </summary>
    public Text GUI_Text_Cost_To_Purchase;

    /// <summary>
    /// GUI
    /// </summary>
    public Button GUI_Button_FirstTime_Purchase;

    /// <summary>
    /// 
    /// </summary>
    public ProgressionBar GUI_Progression_Bar;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public int Cost_To_Purchase_Amount;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public Sprite Image;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public int ID;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public string Item_Name;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public float Base_Cost;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public float Cost;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public float Coefficent;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public int Count;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public float Resource_Rate;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public string Item_ID;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public float Time_To_Complete_Task = 1.0f;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public bool On_Complete;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public bool Unlocked = false;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public bool Is_Purchased;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public bool Started_Timer;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public float Current_Time = 0;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    public Purchasable( int id, 
                        string itemName,
                        float baseCost,
                        Sprite image,
                        float coefficent,
                        int count,
                        float resourceRate,
                        string itemID,
                        float timeToCompleteTask)
    {
        ID = id;
        Item_Name = itemName;
        Base_Cost = baseCost;
        Image = image;
        Coefficent = coefficent;
        Count = count;
        Resource_Rate = resourceRate;
        Item_ID = itemID;
        Time_To_Complete_Task = timeToCompleteTask;

        Cost = Base_Cost;
        Is_Purchased = false;
    }

    /// <summary>
    /// 
    /// </summary>
    public bool Can_Purchase()
    {
        if (Game.Instance.CanPurchase(Cost))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    public void Update()
    {
        UpdateGUI();
    }

    /// <summary>
    /// 
    /// </summary>
    private void UpdateGUI()
    {
        GUI_Text_Cost_To_Purchase.text = CurrencyConverter.Instance.GetCurrencyIntoString(Cost_To_Buy());
        GUI_Text_Item_Name.text = Inventory.Instance.GetItemByID(int.Parse(Item_ID)).Name;
        GUI_Text_Count.text = "LVL. " + Count.ToString("0");

        GUI_Text_Reward.text = "+ " + CurrencyConverter.Instance.GetCurrencyIntoString(Resource_Rate);
        GUI_Text_Value.text = CurrencyConverter.Instance.GetCurrencyIntoStringNoSign(Resource_Rate * Count);
    }

    /// <summary>
    /// 
    /// </summary>
    private string Display_Text(float bonus)
    {
        if (bonus != 0)
            return "<color=#0473f0><b> (" + CurrencyConverter.Instance.GetCurrencyIntoString(bonus) + "%) </b></color>";
        else
            return "";
    }

    /// <summary>
    /// 
    /// </summary>
    public float Decrease_Value(float cost, float decrease)
    {
        if(decrease == 0)
        {
            return cost;
        }

        float percent = decrease / 100;

        float value = cost - (percent * cost);
        Mathf.Clamp(value, 0.001f, value);
        return value;
    }

    /// <summary>
    /// 
    /// </summary>
    public float Increase_Value(float cost, float increase)
    {
        if (increase == 0)
        {
            return cost;
        }

        float percent = increase / 100;
        float value = cost + (percent * cost);
        Mathf.Clamp(value, 0.001f, value);
        return value;
    }

    /// <summary>
    /// 
    /// </summary>
    public void First_Time_Purchase()
    {
        if (Can_Purchase() == true)
        {
            Game.Instance.RemoveGold(Cost);
            Is_Purchased = true;
            Unlocked = true;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public float Cost_To_Buy()
    {
        float value = 0;
        for (int i = 0; i < Cost_To_Purchase_Amount; i++)
        {
            value += Cost * Coefficent;
        }

        return value;
    }

    /// <summary>
    /// 
    /// </summary>
    public void Upgrade()
    {
        float value = Cost_To_Buy();

        if (Game.Instance.CanPurchase(value))
        {
            Game.Instance.RemoveGold(value);

            for (int i = 0; i < Cost_To_Purchase_Amount; i++)
            {
                Cost += Cost * Coefficent;
            }

            Count += Cost_To_Purchase_Amount;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public IEnumerator Update_Timer()
    {
        Started_Timer = true;
        On_Complete = false;

        float speed = (Time.fixedDeltaTime / Time_To_Complete_Task);

        while (GUI_Progression_Bar.Value < 1)
        {
            GUI_Progression_Bar.Value += speed;
            Current_Time = GUI_Progression_Bar.Value;

            yield return null;
        }

        GUI_Progression_Bar.Value = 0;
        Reset_Timer();
    }

    /// <summary>
    /// 
    /// </summary>
    private void Reset_Timer()
    {
        if (!Inventory.Instance.InventoryFull())
        {
            PlayerManager.Instance.AddExperience(Count * Resource_Rate);
            Inventory.Instance.AddItem(int.Parse(Item_ID), (int)(Count * Resource_Rate));
            On_Complete = true;
        }
    }
}
