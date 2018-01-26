using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Collections;

public class Game : MonoBehaviour
{
    private static Game instance;
    public static Game Instance
    {
        get
        {
            if (instance == null)
            {
                DontDestroyOnLoad(instance);
                instance = GameObject.FindObjectOfType<Game>();
            }

            return Game.instance;
        }
    }

    //UI
    //public Text GoldPerSecondText;
    public Text GoldDisplayText;
    //public Text GoldPerClickText;

    //public Text UserLevelText;
    //public Text UserExperienceText;

    //Game Values
    public float TotalGold;
    //public float TotalGoldPerClick;

    //public ProgressionBar xpBar;

    //public Text CurrentPlanetName;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        //if (xpBar != null)
        //{
        //    xpBar.Max = PlayerManager.Instance.XpTillNextLevel;
        //    xpBar.Current = PlayerManager.Instance.CurrentExperience;
        //}

        //if (UserLevelText != null)
        //    UserLevelText.text = PlayerManager.Instance.CurrentLevel.ToString();
        //if (UserExperienceText != null)
        //    UserExperienceText.text = PlayerManager.Instance.CurrentExperience.ToString() + " / " + PlayerManager.Instance.XpTillNextLevel.ToString();

        //if(GoldPerClickText != null)
        //    GoldPerClickText.text = TotalGoldPerClick.ToString();
        if (GoldDisplayText != null)
            GoldDisplayText.text = CurrencyConverter.Instance.GetCurrencyIntoStringNoSign(TotalGold);

        //CurrentPlanetName.text = Map.Instance.CurrentPlanetName;
    }

    public void AddGold(float amount)
    {
        //Prestige.Instance.LifetimCurrency += amount;
        TotalGold += amount;
    }
    public void RemoveGold(float amount)
    {
        TotalGold -= amount;
    }
    public void AddGoldPerClick(float amount)
    {
        //Prestige.Instance.LifetimCurrency += amount;
        //TotalGoldPerClick += amount;
    }
    public void RemoveGoldPerClick(float amount)
    {
        //TotalGoldPerClick -= amount;
    }

    public bool CanPurchase(float cost)
    {
        if(TotalGold >= cost)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddClick()
    {
        //float PercentOfTotalClicks = TotalGold * 0.01f; //1 Percent
        ////AddGold(TotalGoldPerClick + PercentOfTotalClicks);

        //PlayerManager.Instance.AddExperience(25);
    }
}