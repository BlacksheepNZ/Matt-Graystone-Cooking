using System;
using UnityEngine;

/// <summary>
/// 
/// </summary>
[Serializable]
public class PlayerManager : MonoBehaviour
{
    /// <summary>
    /// Instantiate class object
    /// </summary>
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
    private static PlayerManager instance;

    /// <summary>
    /// GUI
    /// </summary>
    public ProgressionBar GUI_Progression_Bar;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public float TotalXp = 0;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public int CurrentLevel = 0;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public int XpTillNextLevel = 0;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public float CurrentExperience = 0;

    /// <summary>
    /// 
    /// </summary>
    public int ResearchPoints = 1;

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    public void Update()
    {
        if (CurrentExperience > 0)
        {
            GUI_Progression_Bar.Value = (float)CurrentExperience / XpTillNextLevel;
        }
    }

    /// <summary>
    /// 
    /// </summary>
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

    /// <summary>
    /// 
    /// </summary>
    public void OnLevelUp()
    {
        if (CurrentLevel != 0)
        {
            AddPrestigePoints(CurrentLevel);
            //AchievementManager.Instance.CreateAchievement(true, "Level " + CurrentLevel, "Congratulations of reaching level " + CurrentLevel, CurrentLevel, 0);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public int GetResearchPoints()
    {
        return ResearchPoints;
    }

    /// <summary>
    /// 
    /// </summary>
    public void AddPrestigePoints(int amount)
    {
        ResearchPoints += amount;
    }

    /// <summary>
    /// 
    /// </summary>
    public void RemovePrestigePoints(int amount)
    {
        ResearchPoints -= amount;
    }
}