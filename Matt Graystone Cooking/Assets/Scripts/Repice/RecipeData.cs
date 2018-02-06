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

        Button_Purchase.onClick.AddListener(() => Purchase());
    }

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

    public void Update()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        Text_Sell_Value.text = "$" + recipe.SellValue * AmountSellMuiltplyer;
        Text_Requirement.text = StringBuilder();
    }

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

    public string ColorString(string color, string input)
    {
       return "<color=#" + color + ">" + input + "</color>";
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

                if (count > AmountSellMuiltplyer) { have_item_count += 1; }
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
