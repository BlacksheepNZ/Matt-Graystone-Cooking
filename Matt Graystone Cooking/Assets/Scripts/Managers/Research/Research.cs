using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[Serializable]
public class Research
{
    public Text NameText;
    public Text DecriptionText;
    public Button Button;

    public string Name;
    public string Decription;

    public ResearchEffect ResearchEffect;
    public float Value;
    public float Coefficent;

    public Research(string name, string decription, ResearchEffect researchEffect, float value, float coefficent)
    {
        Name = name;
        Decription = decription;
        ResearchEffect = researchEffect;
        Value = value;
        Coefficent = coefficent;
    }

    public IEnumerator GUI()
    {
        while (true)
        {
            NameText.text = Name;
            DecriptionText.text = Decription;

            yield return new WaitForSeconds(10);
        }
    }
}
