using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class Achievement
{
    /// <summary>
    /// 
    /// </summary>
    public string Title;

    /// <summary>
    /// 
    /// </summary>
    public string Decription;

    /// <summary>
    /// 
    /// </summary>
    public int Points;

    /// <summary>
    /// 
    /// </summary>
    public int SpriteIndex;

    /// <summary>
    /// 
    /// </summary>
    public bool Unlocked;

    /// <summary>
    /// 
    /// </summary>
    public GameObject AchievementReference;

    /// <summary>
    /// Use this for initialization
    /// </summary>
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

    /// <summary>
    /// 
    /// </summary>
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

    /// <summary>
    /// 
    /// </summary>
    public void SaveAchievements(bool value)
    {
        Unlocked = value;

        int tempPoints = PlayerPrefs.GetInt("Points");

        PlayerPrefs.SetInt("Points", tempPoints += Points);

        PlayerPrefs.SetInt(Title, value ? 1 : 0);

        PlayerPrefs.Save();
    }

    /// <summary>
    /// 
    /// </summary>
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



