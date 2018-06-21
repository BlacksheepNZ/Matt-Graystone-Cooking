using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class Game : MonoBehaviour
{
    /// <summary>
    /// Instantiate class object
    /// </summary>
    public static Game Instance
    {
        get
        {
            if (instance == null)
            {
                DontDestroyOnLoad(instance);
                instance = GameObject.FindObjectOfType<Game>();
            }

            return Game.instance;
        }
    }
    private static Game instance;

    /// <summary>
    /// GUI
    /// </summary>
    public Text GUI_Text_TotalGold;

    /// <summary>
    /// GUI
    /// </summary>
    [HideInInspector]
    public float TotalGold
    {
        get { return totalGold; }
    }
    private float totalGold;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    private void Update()
    {
        if (GUI_Text_TotalGold != null)
            GUI_Text_TotalGold.text =
                CurrencyConverter.Instance.GetCurrencyIntoStringNoSign(totalGold);

        //unlock all
        if(Input.GetKeyDown(KeyCode.F12))
        {
            AddGold(float.MaxValue);
            PlayerManager.Instance.TotalXp = float.MaxValue;
            PlayerManager.Instance.CurrentLevel = int.MaxValue;
            PlayerManager.Instance.CurrentExperience = float.MaxValue;
            PlayerManager.Instance.ResearchPoints = int.MaxValue;

            //for (int i = 0; i < ; i++)
            //{
                //SaveLoad.Instance.Recipe_Prefab
            //}

            //for (int i = 0; i < SaveLoad.Instance.Item_Database.Count; i++)
            //{
            //    Inventory.Instance.AddItem(
            //        SaveLoad.Instance.Item_Database[i].ID, 
            //        int.MaxValue);
            //}

            for (int i = 0; i < ChefResearchTree.Instance.GUI_Chef_Button.Count; i++)
            {
                GameObject chefObject = ChefResearchTree.Instance.GUI_Chef_Button[i];
                Chef chef = chefObject.GetComponent<Chef>();
                chef.CurrentLevel = int.MaxValue;
            }
        }
    }

    /// <summary>
    /// Add gold to total.
    /// </summary>
    public void AddGold(float amount)
    {
        PlayerManager.Instance.AddExperience(amount);
        totalGold += amount;
    }

    /// <summary>
    /// Remove gold from total.
    /// </summary>
    public void RemoveGold(float amount)
    {
        totalGold -= amount;
    }

    /// <summary>
    /// Have enough gold to purchase.
    /// </summary>
    public bool CanPurchase(float cost)
    {
        if (totalGold >= cost)
            return true;
        else
            return false;
    }
}
 