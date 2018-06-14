using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class ChefResearchTree : MonoBehaviour
{
    /// <summary>
    /// Instantiate class object
    /// </summary>
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
    private static ChefResearchTree instance;

    /// <summary>
    /// GUI
    /// </summary>
    public Text GUI_Text_Chef_Level;

    /// <summary>
    /// GUI
    /// </summary>
    public Text GUI_Text_Research_Points;

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    public void Update()
    {
        UpdateGUI();
    }

    /// <summary>
    /// Update GUI is called once per frame
    /// </summary>
    public void UpdateGUI()
    {
        GUI_Text_Research_Points.text = "Prestige Points :" + PlayerManager.Instance.ResearchPoints.ToString();
        GUI_Text_Chef_Level.text = "Chef Lvl. " + PlayerManager.Instance.CurrentLevel.ToString();
    }
}
