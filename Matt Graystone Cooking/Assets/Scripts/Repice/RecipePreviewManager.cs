using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class RecipePreviewManager : MonoBehaviour
{
    public GameObject Recipe_Prefab_Preview;
    public Transform Recipe_Parent;

    public List<GameObject> Recipe_Prefab_GameObject;

    public Button Button_Left;
    public Button Button_Right;

    public void Start()
    {
        List<Recipe> data = SaveLoad.Instance.Recipe_Data;
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

    public void CreatePrefab(Recipe recipe)
    {
        GameObject recipe_preview_prefab = Instantiate(Recipe_Prefab_Preview);
        recipe_preview_prefab.transform.SetParent(Recipe_Parent);
        recipe_preview_prefab.transform.localScale = new Vector3(1, 1, 1);
        recipe_preview_prefab.transform.position = Recipe_Parent.position;
        Recipe_Prefab_GameObject.Add(recipe_preview_prefab);

        RecipePreview recipe_preview_data = recipe_preview_prefab.GetComponent<RecipePreview>();

        recipe_preview_prefab.name = recipe.Name + "_Prefab";
        recipe_preview_data.Name = recipe.Name;
        recipe_preview_data.Item = ConstructItem(recipe.Items);
        recipe_preview_data.Sell_Value = "Sell Value :" + recipe.SellValue.ToString();
        recipe_preview_data.SetText();
    }

    private string ConstructItem(List<RecipeItem> item_data)
    {
        string data = "";

        for (int i = 0; i < item_data.Count; i++)
        {
            data += Inventory.Instance.GetItemByID(item_data[i].ItemID).Name + " x" + item_data[i].Count + "\n";
        }

        return data;
    }
}
