using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class ChefResearchTree : MonoBehaviour
{
    public int ResearchPoints;
    public List<Chef> Class = new List<Chef>();

    public Text Text_ChefLevel;
    public Text Text_ResearchPoints;

    public void Update()
    {
        Text_ResearchPoints.text = "Prestige Points :"+ PlayerManager.Instance.ResearchPoints.ToString();
        Text_ChefLevel.text = "Chef Lvl. " + PlayerManager.Instance.CurrentLevel.ToString();
    }
}
