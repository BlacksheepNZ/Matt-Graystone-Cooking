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
        Purchasable.ButtonFirstTimePurchase.onClick.AddListener(FirstTimePurchase);
        Purchasable.ButtonUpgrade.onClick.AddListener(Upgrade);
        Purchasable.CostToPurchaseAmount = 1;

        Text Costtext = Purchasable.ButtonFirstTimePurchase.GetComponentInChildren<Text>();
        Costtext.text = CurrencyConverter.Instance.GetCurrencyIntoString(Purchasable.Cost);

        Purchasable.OnComplete = true;

        Purchasable.IDText.text = Purchasable.ID.ToString("00");

        if (Purchasable.Started_Timer == true)
        {
            Purchasable.ProgressionBar.Value = Purchasable.Current_Time;

            StartCoroutine(Purchasable.UpdateTimer());
        }

        Purchasable.CostToPurchaseAmount = 1;
    }

    public void Update()
    {
        Purchasable.Update();

        if (Purchasable.IsPurchased == true)
        {
            Purchasable.ButtonFirstTimePurchase.gameObject.SetActive(false);
        }
        else
        {
            Purchasable.ButtonFirstTimePurchase.gameObject.SetActive(true);
        }

        if(Purchasable.Unlocked == true && Purchasable.OnComplete == true)
        {
            StartCoroutine(Purchasable.UpdateTimer());
        }
    }

    public void FirstTimePurchase()
    {
        Purchasable.FirstTimePurchase();
    }

    public void Upgrade()
    {
        Purchasable.Upgrade();
    }
}
