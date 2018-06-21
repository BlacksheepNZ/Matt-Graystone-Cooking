using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
[Serializable]
public class RecipeData : MonoBehaviour
{
    /// <summary>
    /// GUI
    /// </summary>
    public Text GUI_Text_Name;

    /// <summary>
    /// GUI
    /// </summary>
    public Text GUI_Text_Requirement;

    /// <summary>
    /// GUI
    /// </summary>
    public Text GUI_Text_Sell_Value;

    /// <summary>
    /// GUI
    /// </summary>
    public Button GUI_Button_Purchase;

    /// <summary>
    /// GUI
    /// </summary>
    public Image GUI_Preview_Sprite;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public Recipe recipe;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public int AmountSellMuiltplyer = 1;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    public void Start()
    {
        GUI_Text_Name.text = recipe.Name;
        GUI_Preview_Sprite.sprite = recipe.Preview_Image;

        GUI_Button_Purchase.onClick.AddListener(() => Purchase());
    }

    /// <summary>
    /// 
    /// </summary>
    public void Unlock()
    {
        if (recipe.Unlocked == true)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    public void Update()
    {
        UpdateText();
    }

    /// <summary>
    /// 
    /// </summary>
    public override string ToString()
    {
        return (recipe.SellValue * AmountSellMuiltplyer).ToString();
    }

    /// <summary>
    /// 
    /// </summary>
    public void UpdateText()
    {
        GUI_Text_Sell_Value.text = "$" + recipe.SellValue * AmountSellMuiltplyer;
        GUI_Text_Requirement.text = StringBuilder();
    }

    /// <summary>
    /// 
    /// </summary>
    public string StringBuilder()
    {
        string x = "";

        for(int i = 0; i < recipe.Items.Count; i++)
        {
            Item item = GetItemByID(recipe.Items[i].ItemID);
            if (item != null)
            {
                int item_count = Inventory.Instance.CheckItemCount(recipe.Items[i].ItemID);
                if (item_count >= recipe.Items[i].Count * AmountSellMuiltplyer)
                {
                    string value = item.Name + " " + "x" + recipe.Items[i].Count * AmountSellMuiltplyer + " \n";
                    x += ColorString("FFA500", value);
                }
                else
                {
                    string value = item.Name + " " + "x" + recipe.Items[i].Count * AmountSellMuiltplyer + " \n";
                    x += ColorString("FFFFFF", value);
                }
            }
        }

        return x;
    }

    /// <summary>
    /// 
    /// </summary>
    public string ColorString(string color, string input)
    {
       return "<color=#" + color + ">" + input + "</color>";
    }

    /// <summary>
    /// 
    /// </summary>
    public Item GetItemByID(int id)
    {
        for (int y = 0; y < SaveLoad.Instance.Item_Database.Count; y++)
        {
            Item i = SaveLoad.Instance.Item_Database[y];

            if (i.ID == id)
                return i;
        }

        return null;
    }

    /// <summary>
    /// 
    /// </summary>
    public void Purchase()
    {
        int have_item_count = 0;

        if (recipe == null)
        {
            Debug.Log("No recipe found.");
            return;
        }

        for (int i = 0; i < recipe.Items.Count; i++)
        {
            Item item = GetItemByID(recipe.Items[i].ItemID);
            if (item != null)
            {
                int count = Inventory.Instance.CheckItemCount(item.ID);

                if (count > AmountSellMuiltplyer) { have_item_count += 1; }
                else { Debug.Log("need more resources"); break; }//ok
            }
        }

        //have desired items to craft
        if (have_item_count == recipe.Items.Count)
        {
            for (int i = 0; i < recipe.Items.Count; i++)
            {
                for (int x = 0; x < Inventory.Instance.slots.Count; x++)
                {
                    GameObject slot = Inventory.Instance.slots[x];
                    if (slot.transform.childCount > 0)
                    {
                        ItemData item = slot.transform.GetChild(0).GetComponent<ItemData>();

                        if (item.Item.ID == recipe.Items[i].ItemID)
                        {
                            Inventory.Instance.RemoveItem(recipe.Items[i].ItemID, recipe.Items[i].Count * AmountSellMuiltplyer, Inventory.Instance.slots[item.slot].GetComponent<Slot>());
                        }
                    }
                }
            }

            Game.Instance.AddGold(recipe.SellValue * AmountSellMuiltplyer);
            Debug.Log("AddGold :" + recipe.SellValue * AmountSellMuiltplyer);
        }
        else
        {
            Debug.Log("no item");
        }
    }
}
