using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class ChefSelection : MonoBehaviour
{
    /// <summary>
    /// GUI
    /// </summary>
    public Button GUI_Button_First;

    /// <summary>
    /// GUI
    /// </summary>
    public Button GUI_Button_Second;

    /// <summary>
    /// GUI
    /// </summary>
    public Button GUI_Button_Third;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start()
    {
        GUI_Button_First.onClick.AddListener(() => Action());
        GUI_Button_Second.onClick.AddListener(() => Action());
        GUI_Button_Third.onClick.AddListener(() => Action());
    }

    /// <summary>
    /// 
    /// </summary>
    private void Action()
    {
        Game.Instance.AddGold(100 + 100 * PlayerManager.Instance.CurrentLevel);
        ScreenManager.Instance.CloseScreen(this.gameObject);
    }
}
