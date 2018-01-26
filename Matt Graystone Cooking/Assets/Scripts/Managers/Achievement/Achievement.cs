using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

class Achievement
{
    public string Title;
    public string Decription;
    public int Points;
    public int SpriteIndex;
    public bool Unlocked;

    public GameObject AchievementReference;

    public Achievement(string title, string decription, int points, int spriteIndex, GameObject achievementReference)
    {
        Title = title;
        Decription = decription;
        Points = points;
        SpriteIndex = spriteIndex;
        AchievementReference = achievementReference;

        Unlocked = false;
        //LoadAchievements(); 
    }

    public bool EarnAchievement()
    {
        if(!Unlocked)
        {
            AchievementReference.GetComponent<Image>().sprite = AchievementManager.Instance.Sprites[0];

            Unlocked = true;
            //SaveAchievements(true);
            return true;
        }
        return false;
    }

    public void SaveAchievements(bool value)
    {
        Unlocked = value;

        int tempPoints = PlayerPrefs.GetInt("Points");

        PlayerPrefs.SetInt("Points", tempPoints += Points);

        PlayerPrefs.SetInt(Title, value ? 1 : 0);

        PlayerPrefs.Save();
    }

    public void LoadAchievements()
    {
        Unlocked = PlayerPrefs.GetInt(Title) == 1 ? true : false;

        if(Unlocked)
        {
            AchievementManager.Instance.TotalPoints = PlayerPrefs.GetInt("Points");
            AchievementReference.GetComponent<Image>().sprite = AchievementManager.Instance.Sprites[0];
        }
    }
}



