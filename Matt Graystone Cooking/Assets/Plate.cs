using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class Plate : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    private List<ItemData> ItemData = new List<ItemData>();
    private List<RecipeData> RecipeData_Master = new List<RecipeData>();

    public Text TextBox;

    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        ItemData dropedItem = eventData.pointerDrag.GetComponent<ItemData>();
        if (dropedItem == null) return;

        ItemData.Add(dropedItem);

        if(ItemData.Count == 1)
        {
            //get recipe[] from first ingredient dopped
            List<RecipeData> data = Get_Attached_Recipe(dropedItem.Item.ID);
            if (data.Count > 0)
            {
                RecipeData_Master = data;
                TextBox.text = RecipeData_Master.Count + " Recipes";
            }
        }
        else
        {
            RecipeData_Master = GetPass(ItemData[ItemData.Count - 1].Item.ID, RecipeData_Master);

            if (RecipeData_Master.Count == 1)
            {
                TextBox.text = "Recipe found " + RecipeData_Master[0].recipe.Name.ToString();
            }
        }

        if(RecipeData_Master.Count == 0)
        {
            Clear();
        }

        //ID[ID.Count];

        //second ingredient dropped

        //check match with recipe[] filterlist recipe2[]

        //third ingredient dropped

        //check match with recipe2[] filterlist recipe3[]


        //List<RecipeData> data = Get_Attached_Recipe(dropedItem.Item.ID);
        //if (data.Count > 0)
        //{
        //    //recipeData.Add(data);
        //}

        //Debug.Log(data.Count + " Recipes");


    }

    private List<RecipeData> GetPass(int itemID, List<RecipeData> data)
    {
        List<RecipeData> recipeDataLocal = Find_Match(data, itemID);

        if (recipeDataLocal.Count > 0)
        {
            //Panel_Preview.GetComponent<Image>().sprite = Match_Second_Pass.First().recipe.Preview_Image;
            Debug.Log(recipeDataLocal.Count + " Recipes");
        }

        return recipeDataLocal;
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

    private List<RecipeData> Find_Match(List<RecipeData> recipe_master, int itemID)
    {
        List<RecipeData> recipeData_Local = new List<RecipeData>();

        for (int i = 0; i < recipe_master.Count; i++)
        {
            for (int x = 0; x < recipe_master[i].recipe.Items.Count; x++)
            {
                if (recipe_master[i].recipe.Items[x].ItemID == itemID)
                {
                    recipeData_Local.Add(recipe_master[i]);
                }
            }
        }

        return recipeData_Local;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (RecipeData_Master.Count == 1)
        {
            RecipeData recipeData = RecipeData_Master[0];

            recipeData.Purchase();

            CreatePopUpText(recipeData.ToString());
        }
    }

    public void Clear()
    {
        TextBox.text = "";
        ItemData.Clear();
    }

    public GameObject FloatingTextPrefab;

    public void CreatePopUpText(string text)
    {
        GameObject instance = Instantiate(FloatingTextPrefab);
        instance.transform.SetParent(gameObject.transform, false);
        instance.transform.position = Vector3.zero;
        instance.GetComponent<FloatingText>().SetText(text);
    }
}
