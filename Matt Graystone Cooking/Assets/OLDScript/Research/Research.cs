using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
[Serializable]
public class Research
{
    /// <summary>
    /// GUI
    /// </summary>
    public Text NameText;

    /// <summary>
    /// GUI
    /// </summary>
    public Text DecriptionText;

    /// <summary>
    /// GUI
    /// </summary>
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
