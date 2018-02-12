using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Crafting_DragDrop : MonoBehaviour
{
    private static Crafting_DragDrop instance;
    public static Crafting_DragDrop Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Crafting_DragDrop>();
            }

            return global::Crafting_DragDrop.instance;
        }
    }

    public GameObject Panel;
    public GameObject Panel_Preview;
    public Text Text;
    public int Slot_Count = 5;
    public List<int> Slot_ID = new List<int>();
    public Button Button;

    private List<RecipeData> Match_First_Pass = new List<RecipeData>();
    private List<RecipeData> Match_Second_Pass = new List<RecipeData>();
    private List<RecipeData> Match_Third_Pass = new List<RecipeData>();
    private List<RecipeData> Match_Fourth_Pass = new List<RecipeData>();
    private List<RecipeData> Match_Fifth_Pass = new List<RecipeData>();

    private void Start()
    {
        for (int i = 0; i < Slot_Count; i++)
        {
            Inventory.Instance.AddSlot(Panel, ItemType.Consumable);
            Slot_ID.Add(Inventory.Instance.slots.Count() - 1);
        }

        Button.interactable = false;
    }

    public void CheckSlot()
    {
        Get_First_Pass();
        Get_Second_Pass();
        Get_Third_Pass();
        Get_Fourth_Pass();
        Get_Fifth_Pass();

        Clear();

        if(Match_First_Pass.Count == 0)
        {
            Panel_Preview.GetComponent<Image>().sprite = SaveLoad.Instance.Item_Images[0];
        }
    }

    private void Clear()
    {
        Match_First_Pass.Clear();
        Match_Second_Pass.Clear();
        Match_Third_Pass.Clear();
        Match_Fourth_Pass.Clear();
        Match_Fifth_Pass.Clear();
    }

    private int Get_Slot_Item(int slot_item_ID)
    {
        Item item = Inventory.Instance.GetItemFromSlot(Slot_ID[slot_item_ID]);
        if(item == null)
        {
            return -1;
        }

        return item.ID;
    }

    private void Get_First_Pass()
    {
        int id = Get_Slot_Item(0);
        if (id != -1)
        {
            Match_First_Pass = Get_Attached_Recipe(id);
            Panel_Preview.GetComponent<Image>().sprite = Match_First_Pass.First().recipe.Preview_Image;
        }

        Text.text = Match_First_Pass.Count + " Recipes";
    }

    private void Get_Second_Pass()
    {
        int id = Get_Slot_Item(1);
        if (id != -1)
        {
            Match_Second_Pass = Find_Match(Match_First_Pass, id);
        }

        if(Match_Second_Pass.Count > 0)
        {
            Panel_Preview.GetComponent<Image>().sprite = Match_Second_Pass.First().recipe.Preview_Image;
            Text.text = Match_Second_Pass.Count + " Recipes";
        }
    }

    private void Get_Third_Pass()
    {
        int id = Get_Slot_Item(2);
        if (id != -1)
        {
            Match_Third_Pass = Find_Match(Match_Second_Pass, id);
        }

        if (Match_Third_Pass.Count > 0)
        {
            Panel_Preview.GetComponent<Image>().sprite = Match_Third_Pass.First().recipe.Preview_Image;
            Text.text = Match_Third_Pass.Count + " Recipes";
        }
    }

    private void Get_Fourth_Pass()
    {
        int id = Get_Slot_Item(3);
        if (id != -1)
        {
            Match_Fourth_Pass = Find_Match(Match_Third_Pass, id);
        }

        if (Match_Fourth_Pass.Count > 0)
        {
            Panel_Preview.GetComponent<Image>().sprite = Match_Fourth_Pass.First().recipe.Preview_Image;
            Text.text = Match_Fourth_Pass.Count + " Recipes";
        }
    }

    private void Get_Fifth_Pass()
    {
        int id = Get_Slot_Item(4);
        if (id != -1)
        {
            Match_Fifth_Pass = Find_Match(Match_Fourth_Pass, id);
        }


        if (Match_Fifth_Pass.Count > 0)
        {
            Panel_Preview.GetComponent<Image>().sprite = Match_Fifth_Pass.First().recipe.Preview_Image;
            Text.text = Match_Fifth_Pass.Count + " Recipes";
        }
    }

    private List<RecipeData> Find_Match(List<RecipeData> recipe_master, int itemID)
    {
        List<RecipeData> recipeData_Local = new List<RecipeData>();

        for (int i = 0; i < recipe_master.Count; i++)
        {
            for (int x = 0; x < recipe_master[i].recipe.Items.Count; x++)
            {
                if(recipe_master[i].recipe.Items[x].ItemID == itemID)
                {
                    recipeData_Local.Add(recipe_master[i]);
                }
            }
        }

        return recipeData_Local;
    }

    private List<RecipeData> Get_Attached_Recipe(int itemID)
    {
        List<RecipeData> recipeData_Local = new List<RecipeData>();
        List<RecipeData> recipeData_Global = SaveLoad.Instance.Recipe_Data;

        for (int i = 0; i < recipeData_Global.Count; i++)
        {
            for (int x = 0; x < recipeData_Global[i].recipe.Items.Count; x++)
            {
                if (recipeData_Global[i].recipe.Items[x].ItemID == itemID)
                {
                    recipeData_Local.Add(recipeData_Global[i]);
                }
            }
        }

        return recipeData_Local;
    }
}
