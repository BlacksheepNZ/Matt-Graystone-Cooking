using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RecipePreview_QuickView : MonoBehaviour
{
    public Text Text_Name;
    public Button Button;

    public string Name;

    public void SetText()
    {
        Text_Name.text = Name;
    }
}
