using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class SplitStack : MonoBehaviour
{
    /// <summary>
    /// GUI
    /// </summary>
    public Text GUI_Text_Count;

    /// <summary>
    /// GUI
    /// </summary>
    public Text GUI_Text_Count_Remaning;

    /// <summary>
    /// GUI
    /// </summary>
    public Slider GUI_Slider;

    /// <summary>
    /// GUI
    /// </summary>
    public Button GUI_Button;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    private float amount;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public GameObject ItemData;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    public void Start()
    {
        GUI_Button.onClick.AddListener(() =>
        {
            if (GUI_Slider.value > 0 )
            {
                if (ItemData != null)
                {
                    ItemData i = ItemData.transform.GetChild(0).GetComponent<ItemData>();

                    Inventory.Instance.SplitItem(i, (int)amount);
                    Inventory.Instance.HideSplitStack();

                    amount = 0;
                    GUI_Text_Count.text = amount.ToString();
                    GUI_Text_Count_Remaning.text = "0";
                    GUI_Slider.value = 0;
                    return;
                }
            }
            else
            {
                Inventory.Instance.HideSplitStack();
            }
        });
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnValueChanged(ItemData item)
    {
        if (ItemData != null)
        {
            GUI_Slider.onValueChanged.AddListener(delegate
            {
                ValueChangeCheck(item);
            });
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void ValueChangeCheck(ItemData item)
    {
        if (ItemData != null)
        {
            ItemData i = ItemData.transform.GetChild(0).GetComponent<ItemData>();

            amount = ((float)i.count * GUI_Slider.value) - 1;

            GUI_Text_Count_Remaning.text = CurrencyConverter.Instance.GetCurrencyIntoStringNoSign(i.count - amount);
            GUI_Text_Count.text = CurrencyConverter.Instance.GetCurrencyIntoStringNoSign(amount);

            GUI_Slider.minValue = 0;
            GUI_Slider.maxValue = 1;
        }
    }
}
