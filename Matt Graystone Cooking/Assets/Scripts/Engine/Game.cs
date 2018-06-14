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
 