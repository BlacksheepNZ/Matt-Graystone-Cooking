﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class RecipePreviewManager : MonoBehaviour
{
    public GameObject Recipe_Prefab_Preview;
    public Transform Recipe_Parent;

    public List<GameObject> Recipe_Prefab_GameObject;

    public Button Button_Left;
    public Button Button_Right;
    List<RecipeData> data = new List<RecipeData>();

    public void Start()
    {
        data = SaveLoad.Instance.Recipe_Data;
        for (int i = 0; i < data.Count; i++)
        {
            CreatePrefab(data[i]);
        }

        Button_Left.onClick.AddListener(() =>
        {
            ScrollLeft();
        });

        Button_Right.onClick.AddListener(() =>
        {
            ScrollRight();
        });
    }

    public int currentIndex = 0;

    private void ScrollToIndex(int i)
    {
        GameObject recipe_preview_gameobject = Recipe_Prefab_GameObject[i];
        recipe_preview_gameobject.transform.SetAsLastSibling();
    }

    private void ScrollRight()
    {
        currentIndex++;

        if (currentIndex <= Recipe_Prefab_GameObject.Count - 1)
        {
            ScrollToIndex(currentIndex);
        }
        else
        {
            currentIndex = 0;
            ScrollToIndex(currentIndex);
        }
    }

    private void ScrollLeft()
    {
        currentIndex--;

        if (currentIndex >= 0)
        {
            ScrollToIndex(currentIndex);
        }
        else
        {
            currentIndex = Recipe_Prefab_GameObject.Count - 1;
            ScrollToIndex(currentIndex);
        }
    }

    public void Update()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        GameObject recipe_preview_gameobject = Recipe_Prefab_GameObject[currentIndex];
        RecipePreview RecipePreview = recipe_preview_gameobject.GetComponent<RecipePreview>();

        RecipePreview.Text_Sell_Value.text = "$" 
            + data[currentIndex].recipe.SellValue 
            * data[currentIndex].AmountSellMuiltplyer;

        RecipePreview.Text_Item.text = StringBuilder(data[currentIndex]);
    }

    public string StringBuilder(RecipeData data)
    {
        string x = "";

        for (int i = 0; i < data.recipe.Items.Count; i++)
        {
            Item item = data.GetItemByID(data.recipe.Items[i].ItemID);
            if (item != null)
            {
                int item_count = Inventory.Instance.CheckItemCount(data.recipe.Items[i].ItemID);
                if (item_count >= data.recipe.Items[i].Count * data.AmountSellMuiltplyer)
                {
                    string value = item.Name + " " + "x" + data.recipe.Items[i].Count * data.AmountSellMuiltplyer + " \n";
                    x += data.ColorString("FFA500", value);
                }
                else
                {
                    string value = item.Name + " " + "x" + data.recipe.Items[i].Count * data.AmountSellMuiltplyer + " \n";
                    x += data.ColorString("FFFFFF", value);
                }
            }
        }

        return x;
    }

    public void CreatePrefab(RecipeData recipe)
    {
        GameObject recipe_preview_prefab = Instantiate(Recipe_Prefab_Preview);
        recipe_preview_prefab.transform.SetParent(Recipe_Parent);
        recipe_preview_prefab.transform.localScale = new Vector3(1, 1, 1);
        recipe_preview_prefab.transform.position = Recipe_Parent.position;
        Recipe_Prefab_GameObject.Add(recipe_preview_prefab);

        RecipePreview recipe_preview_data = recipe_preview_prefab.GetComponent<RecipePreview>();

        recipe_preview_prefab.name = recipe.recipe.Name + "_Prefab";
        recipe_preview_data.Name = recipe.recipe.Name;
        recipe_preview_data.Item = StringBuilder(recipe);
        recipe_preview_data.Sell_Value = recipe.recipe.SellValue.ToString();
        recipe_preview_data.SetText();
        UpdateText();
    }
}