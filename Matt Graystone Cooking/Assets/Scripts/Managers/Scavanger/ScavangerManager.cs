//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections.Generic;
//using LitJson;
//using System.IO;
//using System;
//using System.Linq;

//public class ScavangerManager : MonoBehaviour
//{
//    private static ScavangerManager instance;
//    public static ScavangerManager Instance
//    {
//        get
//        {
//            if (instance == null)
//            {
//                instance = GameObject.FindObjectOfType<ScavangerManager>();
//            }

//            return ScavangerManager.instance;
//        }
//    }

//    public Text TotalGoldPerSecondText;

//    public Button SwitchBuyAmount;
//    public Text SwitchButtonText;
//    public BuyAmountMode BuyAmountMode = BuyAmountMode.Single;

//    private void Start()
//    {
//        //set first item to visible
//        SaveLoad.Instance.Scavanger_Item[0].SetActive(true);

//        SwitchButtonText.text = BuyAmountMode.ToString();
//        SwitchBuyAmount.onClick.AddListener(() =>
//        {
//            BuyAmountMode current = BuyAmountMode;
//            if (current == Enum.GetValues(typeof(BuyAmountMode)).Cast<BuyAmountMode>().Max())
//            {
//                BuyAmountMode = BuyAmountMode.Single;
//            }
//            else
//            {
//                BuyAmountMode = current + 1;
//            }

//            int amount = 0;

//            switch (BuyAmountMode)
//            {
//                case BuyAmountMode.Single:
//                    amount = 1;
//                    break;
//                case BuyAmountMode.Fifty:
//                    amount = 50;
//                    break;
//                case BuyAmountMode.OneHundred:
//                    amount = 100;
//                    break;
//            }

//            for (int i = 0; i < SaveLoad.Instance.Scavanger_Item.Count; i++)
//            {
//                Scavanger purchasable = SaveLoad.Instance.Scavanger_Item[i].GetComponent<ScavangerData>().Purchasable;
//                purchasable.CostToPurchaseAmount = amount;
//            }

//            SwitchButtonText.text = BuyAmountMode.ToString();
//        });
//    }

//    public float GetRewardRate()
//    {
//        float value = 0;

//        for (int i = 0; i < SaveLoad.Instance.Scavanger_Item.Count; i++)
//        {
//            Scavanger purchasable = SaveLoad.Instance.Scavanger_Item[i].GetComponent<ScavangerData>().Purchasable;
//            if (purchasable.Unlocked == true)
//            {
//                float valueCurrency = purchasable.IncreaseValue(purchasable.CurrencyReward, purchasable.B_IncreaseCurrencyRewardRate);

//                value += (valueCurrency * purchasable.Count) / purchasable.TimeToCompleteTask;
//            }
//        }

//        return value;
//    }

//    private void Update()
//    {
//        TotalGoldPerSecondText.text = CurrencyConverter.Instance.GetCurrencyIntoString(GetRewardRate());

//        for (int i = 0; i < SaveLoad.Instance.Scavanger_Item.Count; i++)
//        {
//            SaveLoad.Instance.Scavanger_Item[i].GetComponent<ScavangerData>().Update();
//        }
//    }
//}
