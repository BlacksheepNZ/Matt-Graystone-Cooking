using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

/// <summary>
/// 
/// </summary>
public class LarderManager : MonoBehaviour
{
    /// <summary>
    /// Instantiate class object
    /// </summary>
    public static LarderManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<LarderManager>();
            }

            return LarderManager.instance;
        }
    }
    private static LarderManager instance;

    /// <summary>
    /// GUI button chef
    /// </summary>
    public Chef Button_Chef_Larder;

    /// <summary>
    /// GUI
    /// </summary>
    public Text Text_Level;

    /// <summary>
    /// GUI
    /// </summary>
    public Button Switch_Buy_Amount;

    /// <summary>
    /// GUI
    /// </summary>
    public Text Switch_Button_Text;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public BuyAmountMode Buy_Amount_Mode = BuyAmountMode.Single;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public int Research_Points;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    private void Start()
    {
        Switch_Button_Text.text = Buy_Amount_Mode.ToString();
        Switch_Buy_Amount.onClick.AddListener(() =>
        {
            BuyAmountMode current = Buy_Amount_Mode;
            if (current == Enum.GetValues(typeof(BuyAmountMode)).Cast<BuyAmountMode>().Max())
            {
                Buy_Amount_Mode = BuyAmountMode.Single;
            }
            else
            {
                Buy_Amount_Mode = current + 1;
            }

            int amount = 0;

            switch (Buy_Amount_Mode)
            {
                case BuyAmountMode.Single:
                    amount = 1;
                    break;
                case BuyAmountMode.Fifty:
                    amount = 50;
                    break;
                case BuyAmountMode.OneHundred:
                    amount = 100;
                    break;
            }

            for (int i = 0; i < SaveLoad.Instance.Larder_Item.Count; i++)
            {
                Purchasable purchasable = SaveLoad.Instance.Larder_Item[i].GetComponent<PurchasableData>().Purchasable;
                purchasable.Cost_To_Purchase_Amount = amount;
            }

            Switch_Button_Text.text = Buy_Amount_Mode.ToString();
        });
    }

    /// <summary>
    /// 
    /// </summary>
    public float GetRewardRate()
    {
        float value = 0;

        for (int i = 0; i < SaveLoad.Instance.Larder_Item.Count; i++)
        {
            Purchasable purchasable = SaveLoad.Instance.Larder_Item[i].GetComponent<PurchasableData>().Purchasable;
            if (purchasable.Unlocked == true)
            {
                value += (purchasable.Resource_Rate * purchasable.Count) / purchasable.Time_To_Complete_Task;
            }
        }

        return value;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    private void Update()
    {
        //Text_Level.text = "lvl. " + Button_Chef_Larder.CurrentLevel.ToString();

        for (int i = 0; i < SaveLoad.Instance.Larder_Item.Count; i++)
        {
            //if (SaveLoad.Instance.Larder_Item[i].GetComponent<PurchasableData>().Purchasable.ID < Button_Chef_Larder.CurrentLevel)
            //{
            //    SaveLoad.Instance.Larder_Item[i].GetComponent<PurchasableData>().gameObject.SetActive(true);
            //}

            if (SaveLoad.Instance.Larder_Item[i].GetComponent<PurchasableData>().Purchasable.ID < Button_Chef_Larder.CurrentLevel)
            {
                SaveLoad.Instance.Larder_Item[i].GetComponent<PurchasableData>().gameObject.SetActive(true);
            }
            else
            {
                SaveLoad.Instance.Larder_Item[i].GetComponent<PurchasableData>().gameObject.SetActive(false);
            }

            SaveLoad.Instance.Larder_Item[i].GetComponent<PurchasableData>().Update();
        }
    }
}
