using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class FeedMeMenu : MonoBehaviour
{
    /// <summary>
    /// Instantiate class object
    /// </summary>
    public static FeedMeMenu Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<FeedMeMenu>();
            }

            return FeedMeMenu.instance;
        }
    }
    private static FeedMeMenu instance;

    /// <summary>
    /// 
    /// </summary>
    public GameObject GUI_Feed_Me_Button;
    public GameObject Panel;

    /// <summary>
    /// 
    /// </summary>
    private List<GameObject> Buttons = new List<GameObject>();

    /// <summary>
    /// 
    /// </summary>
    public void AddButton(RecipeData recipe)
    {
        if(Buttons.Count > 5)
        {
            Buttons.RemoveAt(0);
        }

        GameObject prefab = Instantiate(GUI_Feed_Me_Button);
        prefab.transform.SetParent(Panel.transform);
        prefab.transform.localScale = new Vector3(1, 1, 1);
        prefab.transform.position = Panel.transform.position;
        prefab.GetComponent<Button>().onClick.AddListener(() => OnClick(recipe));

        Buttons.Add(prefab);
    }

    /// <summary>
    /// 
    /// </summary>
    private void OnClick(RecipeData recipe)
    {
        recipe.Purchase();
    }
}
