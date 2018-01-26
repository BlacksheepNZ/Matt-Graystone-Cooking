using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{
    /// <summary>
    /// max char 75
    /// 5 rows x 15 length
    /// </summary>

    private Item item;
    private string data;
    public Text Name;
    public Text Decription;

    public void Activate(Item item)
    {
        this.item = item;
        ConstructDataString();

        //if(Input.mousePosition.x > Screen.width / 2)
        //{
        //    ChangePivot(1);
        //}
        //if (Input.mousePosition.x < Screen.width / 2)
        //{
        //    ChangePivot(0);
        //}
    }

    //0 left, 1 right
    //public void ChangePivot(int pivotValue)
    //{
    //    RectTransform rectTransform = ToolTip_GameObject.GetComponent<RectTransform>();

    //    rectTransform.offsetMin = new Vector2(pivotValue, 1);
    //    rectTransform.offsetMax = new Vector2(pivotValue, 1);

    //    rectTransform.pivot = new Vector2(pivotValue, 1);
    //    rectTransform.position = Vector2.zero;

    //}

    //also could change border
    public void ConstructDataString()
    {
        string itemColor = "FFF";

        if (item.ItemRarity == ItemRarity.Common)
            itemColor = "FFF";
        if (item.ItemRarity == ItemRarity.Uncommon)
            itemColor = "00FF00";
        if (item.ItemRarity == ItemRarity.Rare)
            itemColor = "0473f0";
        if (item.ItemRarity == ItemRarity.Epic)
            itemColor = "800080";
        if (item.ItemRarity == ItemRarity.Legendary)
            itemColor = "FFA500";

        Name.text = "<color=#" + itemColor + "><b>" + item.Name + "</b></color>";
        Decription.text = item.GetDecription();
    }

    public void DeActivate()
    {
        Name.text = "";
        Decription.text = "";
    }
}
