using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class RecipePreview_QuickView : MonoBehaviour
{
    /// <summary>
    /// GUI
    /// </summary>
    public Text GUI_Text_Name;

    /// <summary>
    /// GUI
    /// </summary>
    public Button GUI_Button;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public string Name;

    /// <summary>
    /// 
    /// </summary>
    public void SetText()
    {
        GUI_Text_Name.text = Name;
    }
}
