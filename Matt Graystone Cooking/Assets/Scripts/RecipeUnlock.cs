using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RecipeUnlock : MonoBehaviour {

    public Button Button_OK;
    public Text Text_Name;
    public Text Text_Recipe;
    public Text Text_SellValue;

    public GameObject Prefab_Recipe_Unlock;

    public void Set(string name, string recipe, string sell_value)
    {
        Prefab_Recipe_Unlock.gameObject.SetActive(true);

        Text_Name.text = name;
        Text_Recipe.text = recipe;
        Text_SellValue.text = sell_value;

        Button_OK.onClick.AddListener(() => Prefab_Recipe_Unlock.gameObject.SetActive(false));
    }
}
