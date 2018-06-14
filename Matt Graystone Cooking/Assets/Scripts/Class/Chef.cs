using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class Chef : MonoBehaviour
{
    /// <summary>
    /// GUI
    /// </summary>
    public Text Text_Name;
    /// <summary>
    /// GUI
    /// </summary>
    public Text Text_Cost;
    /// <summary>
    /// GUI
    /// </summary>
    public Text Text_Level;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public string Name;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public int RequiredLevel = 100;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public bool Unlocked = false;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public int CostToLevel;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public int MaxLevel;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public int CurrentLevel;

    /// <summary>
    /// 
    /// </summary>
    private Button button;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            Purchasable();
        });

        if(Unlocked == true)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    public void Update()
    {
        UpdateGUI();
        Unlock();
    }

    /// <summary>
    /// 
    /// </summary>
    public void Unlock()
    {
        if(PlayerManager.Instance.CurrentLevel >= RequiredLevel)
        {
            Unlocked = true;
            button.interactable = true;
        }
        else
        {
            Unlocked = false;
            button.interactable = false;
        }
    }

    /// <summary>
    /// Update GUI is called once per frame
    /// </summary>
    public void UpdateGUI()
    {
        Text_Name.text = Name;
        Text_Cost.text = "Cost. " + CostToLevel.ToString();
        Text_Level.text = CurrentLevel.ToString() + "/" + MaxLevel.ToString();
    }

    /// <summary>
    /// 
    /// </summary>
    public void Purchasable()
    {
        if (Unlocked == true)
        {
            if (CurrentLevel < MaxLevel &&
                PlayerManager.Instance.GetResearchPoints() >= CostToLevel)
            {

                CurrentLevel++;
                PlayerManager.Instance.RemovePrestigePoints(CostToLevel);
            }
        }

        UpdateGUI();
    }
}