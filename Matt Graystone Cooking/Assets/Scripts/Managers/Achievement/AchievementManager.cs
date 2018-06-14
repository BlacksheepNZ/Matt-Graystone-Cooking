using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class AchievementManager : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    private static AchievementManager instance;
    public static AchievementManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<AchievementManager>();
            }

            return AchievementManager.instance;
        }
    }

    public Transform Parent;

    public GameObject AchievementPrefab;

    public Sprite[] Sprites;

    public GameObject VisualAchievement;

    //public Text PointsText;

    public Dictionary<string, Achievement> Achievements = new Dictionary<string, Achievement>();

    public int TotalPoints;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    private void Awake()
    {
        //PointsText.text = TotalPoints.ToString();
    }

    /// <summary>
    /// 
    /// </summary>
    public void CreateAchievement(bool activate, string title, string decription, int points, int spriteIndex)
    {
        GameObject achievement = (GameObject)Instantiate(AchievementPrefab);

        Achievement newAchievement = new Achievement(title, decription, points, spriteIndex, achievement);

        Achievements.Add(title, newAchievement);

        //SetAchievementInfo("AchievementVerticalLayout", achievement, title);

        if (activate) EarnAchievement(title);
    }

    /// <summary>
    /// 
    /// </summary>
    public void EarnAchievement(string title)
    {
        if(Achievements[title].EarnAchievement())
        {
            //display achievement
            GameObject achievement = (GameObject)Instantiate(VisualAchievement);

            SetAchievementInfo(achievement, title);

            TotalPoints += Achievements[title].Points;
            //PointsText.text = TotalPoints.ToString();

            StartCoroutine(HideAchievement(achievement));
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public IEnumerator HideAchievement(GameObject achievement)
    {
        RewardFireworks.Instance.Play();
        yield return new WaitForSeconds(3);
        Destroy(achievement);
    }

    /// <summary>
    /// 
    /// </summary>
    public void SetAchievementInfo(GameObject achievement, string title)
    {
        achievement.transform.SetParent(Parent);
        achievement.transform.localPosition = Vector3.zero;
        achievement.transform.localScale = new Vector3(1, 1, 1);
        achievement.transform.GetChild(0).GetComponent<Text>().text = title;
        achievement.transform.GetChild(1).GetComponent<Text>().text = Achievements[title].Decription;
        achievement.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = Achievements[title].Points.ToString();
        achievement.transform.GetChild(3).GetComponent<Image>().sprite = Sprites[Achievements[title].SpriteIndex];
    }
}

