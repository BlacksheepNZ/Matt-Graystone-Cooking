using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class RecipeManager : MonoBehaviour
{
    private static RecipeManager instance;
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

    public Button Button_SwitchBuyAmount;
    public Text Text_SwitchButtonText;
    public BuyAmountMode BuyAmountMode = BuyAmountMode.Single;

    private int amount = 1;

    private void Start()
    {
        SwitchBuyMode();
    }

    public void SwitchBuyMode()
    {
        Text_SwitchButtonText.text = BuyAmountMode.ToString();
        Button_SwitchBuyAmount.onClick.AddListener(() =>
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

            Text_SwitchButtonText.text = BuyAmountMode.ToString();
        });
    }
}
