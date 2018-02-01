﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using LitJson;
using System.IO;
using System;
using System.Linq;

public class PastryManager : MonoBehaviour
{
    private static PastryManager instance;
    public static PastryManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<PastryManager>();
            }

            return PastryManager.instance;
        }
    }

    public Chef Button_Chef_Pastry;
    public Text Text_Level;

    public Button Switch_Buy_Amount;
    public Text Switch_Button_Text;
    public BuyAmountMode Buy_Amount_Mode = BuyAmountMode.Single;

    public int Research_Points;

    private void Start()
    {
        Switch_Button_Text.text = Buy_Amount_Mode.ToString();
        Switch_Buy_Amount.onClick.AddListener(() =>
        {
            BuyAmountMode current = Buy_Amount_Mode;
            if (current == Enum.GetValues(typeof(BuyAmountMode)).Cast<BuyAmountMode>().Max())
            {
                Buy_Amount_Mode = BuyAmountMode.Single;
            }
            else
            {
                Buy_Amount_Mode = current + 1;
            }

            int amount = 0;

            switch (Buy_Amount_Mode)
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

            for (int i = 0; i < SaveLoad.Instance.Pastry_Item.Count; i++)
            {
                Purchasable purchasable = SaveLoad.Instance.Pastry_Item[i].GetComponent<PurchasableData>().Purchasable;
                purchasable.CostToPurchaseAmount = amount;
            }

            Switch_Button_Text.text = Buy_Amount_Mode.ToString();
        });
    }

    public float GetRewardRate()
    {
        float value = 0;

        for (int i = 0; i < SaveLoad.Instance.Pastry_Item.Count; i++)
        {
            Purchasable purchasable = SaveLoad.Instance.Pastry_Item[i].GetComponent<PurchasableData>().Purchasable;
            if (purchasable.Unlocked == true)
            {
                value += (purchasable.Resource_Rate * purchasable.Count) / purchasable.TimeToCompleteTask;
            }
        }

        return value;
    }

    private void Update()
    {
        Text_Level.text = "lvl. " + Button_Chef_Pastry.CurrentLevel.ToString();

        for (int i = 0; i < SaveLoad.Instance.Pastry_Item.Count; i++)
        {
            if (SaveLoad.Instance.Pastry_Item[i].GetComponent<PurchasableData>().Purchasable.ID < Button_Chef_Pastry.CurrentLevel)
            {
                SaveLoad.Instance.Pastry_Item[i].GetComponent<PurchasableData>().gameObject.SetActive(true);
            }
            else
            {
                SaveLoad.Instance.Pastry_Item[i].GetComponent<PurchasableData>().gameObject.SetActive(false);
            }

            SaveLoad.Instance.Pastry_Item[i].GetComponent<PurchasableData>().Update();
        }
    }
}
