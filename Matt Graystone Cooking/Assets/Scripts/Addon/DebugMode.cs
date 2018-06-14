using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMode : MonoBehaviour
{

    //public Reward Reward;

    //public GameObject WarpGameObject;
    //public GameObject PlexGameObject;

    bool GUIEnabledControl = false;
    bool GUIEnabledStats = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F11))
        {
            GUIEnabledStats = false;
            GUIEnabledControl = !GUIEnabledControl;
        }

        if (Input.GetKeyDown(KeyCode.F12))
        {
            GUIEnabledControl = false;
            GUIEnabledStats = !GUIEnabledStats;
        }
    }

    void OnGUI()
    {
        if (GUIEnabledStats)
        {
            GUI.Box(new Rect(0, 350, 250, 50), "Prestige Points: " + Prestige.Instance.TotalPoints);
            GUI.Box(new Rect(0, 50, 250, 50), "Total LifeTime Gold: " + CurrencyConverter.Instance.GetCurrencyIntoString(Prestige.Instance.LifetimCurrency));
            //GUI.Box(new Rect(0, 100, 250, 50), "Total Gold Per Click: " + Game.Instance.TotalGoldPerClick);
            GUI.Box(new Rect(0, 150, 250, 50), "Current Level" + PlayerManager.Instance.CurrentLevel);
            GUI.Box(new Rect(0, 200, 250, 50), "XP: " + PlayerManager.Instance.CurrentExperience + "/" + PlayerManager.Instance.XpTillNextLevel);

            //GUI.Box(new Rect(0, 250, 250, 50), "Scavanger" + CurrencyConverter.Instance.GetCurrencyIntoString(ScavangerManager.Instance.GetRewardRate()));
            GUI.Box(new Rect(0, 300, 250, 50), "Pastry" + CurrencyConverter.Instance.GetCurrencyIntoString(PastryManager.Instance.GetRewardRate()));
        }

        if (GUIEnabledControl)
        {
            if (GUI.Button(new Rect(0, 0, 200, 50), "Save"))
            {
                //SaveLoad.Instance.SaveFile();
            };

            if (GUI.Button(new Rect(200, 0, 200, 50), "Load"))
            {
                //SaveLoad.Instance.Load();
            };

            if (GUI.Button(new Rect(0, 50, 200, 50), "Add 1M Gold"))
            {
                Game.Instance.AddGold(1000000);
            };

            if (GUI.Button(new Rect(0, 100, 200, 50), "Add Potion"))
            {
                //Inventory.Instance.AddItem(100, 1);
            };

            if (GUI.Button(new Rect(0, 150, 200, 50), "Use Potion"))
            {
                //Inventory.Instance.UseComsumable(100);
            };

            if (GUI.Button(new Rect(0, 200, 200, 50), "Add Buff"))
            {
                //BuffManager.Instance.AddBuff(BuffEffect.Buff1, 5.0f);
            };

            if (GUI.Button(new Rect(0, 250, 200, 50), "Add Building"))
            {
                //Building.Instance.CreateBuilding();
            };

            if (GUI.Button(new Rect(0, 300, 200, 50), "Show WarpEffect"))
            {
                //WarpGameObject.SetActive(true);
            };

            if (GUI.Button(new Rect(200, 300, 200, 50), "Hide WarpEffect"))
            {
                //WarpGameObject.SetActive(false);
            };

            if (GUI.Button(new Rect(0, 350, 200, 50), "Show Plex"))
            {
                //PlexGameObject.SetActive(true);
            };

            if (GUI.Button(new Rect(200, 350, 200, 50), "Hide Plex"))
            {
                //PlexGameObject.SetActive(false);
            };

            if (GUI.Button(new Rect(0, 400, 200, 50), "Add Comet"))
            {
                //CometManager.Instance.CreateComet();
            };
        }
    }
}
