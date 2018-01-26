//using System;
//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;
//using UnityEngine.UI;

//public class ScavangerData : MonoBehaviour
//{
//    public Scavanger Purchasable;
//    private static ScavangerData instance;
//    public static ScavangerData Instance
//    {
//        get
//        {
//            if (instance == null)
//            {
//                instance = GameObject.FindObjectOfType<ScavangerData>();
//            }

//            return ScavangerData.instance;
//        }
//    }

//    public void Start()
//    {
//        Purchasable.ButtonFirstTimePurchase.onClick.AddListener(FirstTimePurchase);
//        Purchasable.ButtonUpgrade.onClick.AddListener(Upgrade);

//        Text Costtext = Purchasable.ButtonFirstTimePurchase.GetComponentInChildren<Text>();
//        Costtext.text = "Buy: " + CurrencyConverter.Instance.GetCurrencyIntoString(Purchasable.Cost);

//        //this need to be set so that the timers can start
//        Purchasable.OnComplete = true;

//        Purchasable.IDText.text = Purchasable.ID.ToString("00");

//        if (Purchasable.StartedTimer == true)
//        {
//            Purchasable.ProgressionBar.Value = Purchasable.CurrentTime;
//            StartCoroutine(Purchasable.UpdateTimer());
//        }

//        Purchasable.CostToPurchaseAmount = 1;
//    }

//    public void Update()
//    {
//        Purchasable.Update();

//        if (Purchasable.IsPurchased == true)
//        {
//            Purchasable.ButtonFirstTimePurchase.gameObject.SetActive(false);
//        }
//        else
//        {
//            Purchasable.ButtonFirstTimePurchase.gameObject.SetActive(true);
//        }

//        if (Purchasable.CanPurchase() == true || Purchasable.ShowUnlockNearGoal())
//        {
//            gameObject.SetActive(true);
//        }

//        if (Purchasable.Unlocked == true && Purchasable.OnComplete == true)
//        {
//            if(Purchasable.buildingUnlock == false)
//            {
//                Purchasable.SpawnVessel();
//                Purchasable.buildingUnlock = true;
//            }

//            StartCoroutine(Purchasable.UpdateTimer());
//        }
//    }

//    public void FirstTimePurchase()
//    {
//        //SoundManager.Instance.Play_CashRegister();

//        Purchasable.FirstTimePurchase();
//    }

//    public void Upgrade()
//    {
//        //SoundManager.Instance.Play_CashRegister();

//        switch(ScavangerManager.Instance.BuyAmountMode)
//        {
//            case BuyAmountMode.Single:
//                Purchasable.Upgrade();
//                break;
//            case BuyAmountMode.Fifty:
//                Purchasable.Upgrade();
//                break;
//            case BuyAmountMode.OneHundred:
//                Purchasable.Upgrade();
//                break;
//        }
//    }
//}
