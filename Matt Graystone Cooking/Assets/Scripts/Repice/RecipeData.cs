using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class RecipeData : MonoBehaviour
{
    public Text Text_Name;
    public Text Text_Requirement;
    public Text Text_Sell_Value;

    public Button Button_Purchase;

    public Recipe recipe;

    public void Start()
    {
        Text_Name.text = recipe.Name;
        Text_Sell_Value.text = recipe.SellValue.ToString();

        Button_Purchase.onClick.AddListener(() => Purchase());

        UpdateText();
    }

    public string Text_Requirements()
    {
        return "Requirements :" + StringBuilder() + " = $" + recipe.SellValue * AmountSellMuiltplyer;
    }

    public void UpdateText()
    {
        Text_Requirement.text = Text_Requirements();
    }

    private string StringBuilder()
    {
        string x = "";

        for(int i = 0; i < recipe.Items.Count; i++)
        {
            Item item = GetItemByID(recipe.Items[i].ItemID);
            if (item != null)
            {
                x += item.Name + " " +
                   "x" + recipe.Items[i].Count * AmountSellMuiltplyer + " ";
            }
        }

        return x;
    }

    private Item GetItemByID(int id)
    {
        for (int y = 0; y < SaveLoad.Instance.Item_Database.Count; y++)
        {
            Item i = SaveLoad.Instance.Item_Database[y];

            if (i.ID == id)
                return i;
        }

        return null;
    }

    private void Purchase()
    {
        Sell();
    }

    public int AmountSellMuiltplyer = 1;

    void Sell()
    {
        int have_item_count = 0;

        for (int i = 0; i < recipe.Items.Count; i++)
        {
            Item item = GetItemByID(recipe.Items[i].ItemID);
            if (item != null)
            {
                int count = Inventory.Instance.CheckItemCount(item.ID);

                if (count >= AmountSellMuiltplyer) { have_item_count += 1; }
                else { Debug.Log("need more resources"); break; }//ok
            }
        }

        //have desired items to craft
        if (have_item_count == recipe.Items.Count)
        {
            for (int i = 0; i < recipe.Items.Count; i++)
            {
                Inventory.Instance.RemoveItem(recipe.Items[i].ItemID, recipe.Items[i].Count * AmountSellMuiltplyer);
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
