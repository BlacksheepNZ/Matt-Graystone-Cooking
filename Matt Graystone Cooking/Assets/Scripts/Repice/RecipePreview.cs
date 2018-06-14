using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class RecipePreview : MonoBehaviour
{
    /// <summary>
    /// GUI
    /// </summary>
    public Text GUI_Text_Name;

    /// <summary>
    /// GUI
    /// </summary>
    public Text GUI_Text_Item;

    /// <summary>
    /// GUI
    /// </summary>
    public Text GUI_Text_Sell_Value;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public string Name;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public string Item;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public string Sell_Value;

    /// <summary>
    /// 
    /// </summary>
    public void SetText()
    {
        GUI_Text_Name.text = Name;
        GUI_Text_Item.text = Item;
        GUI_Text_Sell_Value.text = "$" + Sell_Value;
    }
}
