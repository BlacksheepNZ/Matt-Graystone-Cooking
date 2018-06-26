using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 5 rows x 15 length
/// (max char 75)
/// </summary>
public class ToolTip : MonoBehaviour
{
    /// <summary>
    /// GUI
    /// </summary>
    public Text GUI_Text_Name;

    /// <summary>
    /// GUI
    /// </summary>
    public Text GUI_Text_Decription;

    /// <summary>
    /// GUI
    /// </summary>
    public Text GUI_Text_Count;

    /// <summary>
    /// 
    /// </summary>
    private ItemData item;

    /// <summary>
    /// 
    /// </summary>
    private string data;

    /// <summary>
    /// 
    /// </summary>
    public void Activate(ItemData item)
    {
        this.item = item;
        ConstructDataString();
    }

    /// <summary>
    /// 
    /// </summary>
    public void DeActivate()
    {
        GUI_Text_Name.text = "";
        GUI_Text_Decription.text = "";
        GUI_Text_Count.text = "";
    }

    /// <summary>
    /// 
    /// </summary>
    public void ConstructDataString()
    {
        string itemColor = "FFF";

        ItemRarity itemRarity = item.Item.ItemRarity;

        if (itemRarity == ItemRarity.Common)
            itemColor = "FFF";
        if (itemRarity == ItemRarity.Uncommon)
            itemColor = "00FF00";
        if (itemRarity == ItemRarity.Rare)
            itemColor = "0473f0";
        if (itemRarity == ItemRarity.Epic)
            itemColor = "800080";
        if (itemRarity == ItemRarity.Legendary)
            itemColor = "FFA500";

        GUI_Text_Name.text = "<color=#" + itemColor + "><b>" + item.Item.Name + "</b></color>";
        GUI_Text_Decription.text = item.Item.BonusStats.ToString(); //item.Item.ToString();
    }

    /// <summary>
    /// 
    /// </summary>
    public IEnumerator UpdateCount()
    {
        while (true)
        {
            GUI_Text_Count.text = CurrencyConverter.Instance.GetCurrencyIntoStringNoSign(item.count);

            yield return null;
        }
    }
}
