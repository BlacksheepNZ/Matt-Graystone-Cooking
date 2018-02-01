using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class ChefResearchTree : MonoBehaviour
{
    private static ChefResearchTree instance;
    public static ChefResearchTree Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<ChefResearchTree>();
            }

            return ChefResearchTree.instance;
        }
    }

    public Text Text_ChefLevel;
    public Text Text_ResearchPoints;

    public void Update()
    {
        Text_ResearchPoints.text = "Prestige Points :"+ PlayerManager.Instance.ResearchPoints.ToString();
        Text_ChefLevel.text = "Chef Lvl. " + PlayerManager.Instance.CurrentLevel.ToString();
    }
}
