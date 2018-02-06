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

    public string Name;

    public int RequiredLevel = 100;
    public bool Unlocked = false;

    public int CostToLevel;
    public int MaxLevel;
    public int CurrentLevel;

    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            Purchasable();
        });

        if(Unlocked == true)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }

    public void Update()
    {
        UpdateText();
        Unlock();
    }

    public void Unlock()
    {
        if(PlayerManager.Instance.CurrentLevel >= RequiredLevel)
        {
            Unlocked = true;
            button.interactable = true;
        }
        else
        {
            Unlocked = false;
            button.interactable = false;
        }
    }

    public void UpdateText()
    {
        Text_Name.text = Name;
        Text_Cost.text = "Cost. " + CostToLevel.ToString();
        Text_Level.text = CurrentLevel.ToString() + "/" + MaxLevel.ToString();
    }

    public void Purchasable()
    {
        if (Unlocked == true)
        {
            if (CurrentLevel < MaxLevel &&
                PlayerManager.Instance.GetPrestigePoints() >= CostToLevel)
            {

                CurrentLevel++;
                PlayerManager.Instance.RemovePrestigePoints(CostToLevel);
            }
        }

        UpdateText();
    }
}