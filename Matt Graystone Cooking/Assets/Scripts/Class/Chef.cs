using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class Chef : MonoBehaviour
{
    public Text Text_Name;
    public Text Text_Cost;
    public Text Text_Level;

    public int CostToLevel = 1;
    public int MaxLevel = 10;
    public int CurrentLevel = 0;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            Purchasable();
        });
    }

    public void UpdateText()
    {
        try
        {
            Text_Cost.text = "Cost. " + CostToLevel.ToString();
            Text_Level.text = CurrentLevel.ToString() + "/" + MaxLevel.ToString();
        }
        catch (Exception ex) { }
    }

    public void Purchasable()
    {
        if (CurrentLevel < MaxLevel &&
            PlayerManager.Instance.GetPrestigePoints() >= CostToLevel)
        {
            CurrentLevel++;
            PlayerManager.Instance.RemovePrestigePoints(CostToLevel);
        }

        UpdateText();
    }
}