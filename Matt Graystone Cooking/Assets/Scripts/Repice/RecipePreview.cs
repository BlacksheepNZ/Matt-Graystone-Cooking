using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RecipePreview : MonoBehaviour
{
    public Text Text_Name;
    public Text Text_Item;
    public Text Text_Sell_Value;

    public string Name;
    public string Item;
    public string Sell_Value;

    public void SetText()
    {
        Text_Name.text = Name;
        Text_Item.text = Item;
        Text_Sell_Value.text = Sell_Value;
    }
}
