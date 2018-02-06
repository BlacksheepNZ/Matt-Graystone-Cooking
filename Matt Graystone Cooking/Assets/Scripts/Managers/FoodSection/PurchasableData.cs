using UnityEngine;
using UnityEngine.UI;

public class PurchasableData : MonoBehaviour
{
    public Purchasable Purchasable;
    private static PurchasableData instance;
    public static PurchasableData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<PurchasableData>();
            }

            return PurchasableData.instance;
        }
    }

    public void Start()
    {
        Purchasable.Button_FirstTime_Purchase.onClick.AddListener(FirstTimePurchase);
        Purchasable.Button_Upgrade.onClick.AddListener(Upgrade);
        Purchasable.Cost_To_Purchase_Amount = 1;

        Text Costtext = Purchasable.Button_FirstTime_Purchase.GetComponentInChildren<Text>();
        Costtext.text = CurrencyConverter.Instance.GetCurrencyIntoString(Purchasable.Cost);

        Purchasable.On_Complete = true;

        Purchasable.IDText.text = Purchasable.ID.ToString("00");

        if (Purchasable.Started_Timer == true)
        {
            Purchasable.Progression_Bar.Value = Purchasable.Current_Time;

            StartCoroutine(Purchasable.Update_Timer());
        }

        Purchasable.Cost_To_Purchase_Amount = 1;
    }

    public void Update()
    {
        Purchasable.Update();

        if (Purchasable.Is_Purchased == true)
        {
            Purchasable.Button_FirstTime_Purchase.gameObject.SetActive(false);
        }
        else
        {
            Purchasable.Button_FirstTime_Purchase.gameObject.SetActive(true);
        }

        if(Purchasable.Unlocked == true && Purchasable.On_Complete == true)
        {
            StartCoroutine(Purchasable.Update_Timer());
        }
    }

    public void FirstTimePurchase()
    {
        Purchasable.First_Time_Purchase();
    }

    public void Upgrade()
    {
        Purchasable.Upgrade();
    }
}
