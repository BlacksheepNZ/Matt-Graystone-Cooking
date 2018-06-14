using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class RecipeManager : MonoBehaviour
{
    /// <summary>
    /// Instantiate class object
    /// </summary>
    public static RecipeManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<RecipeManager>();
            }

            return RecipeManager.instance;
        }
    }
    private static RecipeManager instance;

    /// <summary>
    /// GUI
    /// </summary>
    public Button GUI_Button_SwitchBuyAmount;

    /// <summary>
    /// GUI
    /// </summary>  
    public Text GUI_Text_SwitchButtonText;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public BuyAmountMode BuyAmountMode = BuyAmountMode.Single;

    /// <summary>
    /// 
    /// </summary>
    private int amount = 1;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    private void Start()
    {
        SwitchBuyMode();
    }

    /// <summary>
    /// 
    /// </summary>
    public void SwitchBuyMode()
    {
        GUI_Text_SwitchButtonText.text = BuyAmountMode.ToString();
        GUI_Button_SwitchBuyAmount.onClick.AddListener(() =>
        {
            BuyAmountMode current = BuyAmountMode;
            if (current == Enum.GetValues(typeof(BuyAmountMode)).Cast<BuyAmountMode>().Max())
            {
                BuyAmountMode = BuyAmountMode.Single;
            }
            else
            {
                BuyAmountMode = current + 1;
            }

            switch (BuyAmountMode)
            {
                case BuyAmountMode.Single:
                    amount = 1;
                    break;
                case BuyAmountMode.Fifty:
                    amount = 50;
                    break;
                case BuyAmountMode.OneHundred:
                    amount = 100;
                    break;
            }

            for (int i = 0; i < SaveLoad.Instance.Recipe_Item.Count; i++)
            {
                RecipeData r = SaveLoad.Instance.Recipe_Item[i].GetComponent<RecipeData>();
                r.AmountSellMuiltplyer = amount;
                r.UpdateText();
            }

            GUI_Text_SwitchButtonText.text = BuyAmountMode.ToString();
        });
    }
}
