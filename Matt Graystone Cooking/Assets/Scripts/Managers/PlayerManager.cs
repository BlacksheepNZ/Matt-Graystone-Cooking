using System;
using UnityEngine;

[System.Serializable]
public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance;
    public static PlayerManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<PlayerManager>();
            }

            return PlayerManager.instance;
        }
    }

    public float TotalXp;
    public int CurrentLevel;
    public int XpTillNextLevel;
    public float CurrentExperience;

    public int ResearchPoints;

    public ProgressionBar progressionBar;

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.F10))
        {
            AddExperience(100000);
        }

        if (CurrentExperience > 0)
        {
            progressionBar.Value = (float)CurrentExperience / XpTillNextLevel;
        }
    }

    public void AddExperience(float experience)
    {
        CurrentExperience += experience;
        TotalXp += experience;

        int currentXp = (int)(0.1f * Math.Sqrt(CurrentExperience));

        if (CurrentExperience >= XpTillNextLevel)
        {
            CurrentExperience = 0;
            CurrentLevel = currentXp;

            OnLevelUp();
        }

        XpTillNextLevel = 100 * (CurrentLevel + 1) * (CurrentLevel + 1);
    }

    public void OnLevelUp()
    {
        if (CurrentLevel != 0)
        {
            AddPrestigePoints(CurrentLevel);
            //AchievementManager.Instance.CreateAchievement(true, "Level " + CurrentLevel, "Congratulations of reaching level " + CurrentLevel, CurrentLevel, 0);
        }
    }

    public int GetPrestigePoints()
    {
        return ResearchPoints;
    }

    public void AddPrestigePoints(int amount)
    {
        ResearchPoints += amount;
    }
    public void RemovePrestigePoints(int amount)
    {
        ResearchPoints -= amount;
    }
}