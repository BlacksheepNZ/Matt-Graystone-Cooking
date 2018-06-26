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
        DeActivateButton();
        Game.Instance.AddGold(100 + 100 * PlayerManager.Instance.CurrentLevel);
        ScreenManager.Instance.CloseScreen(this.gameObject);
    }

    private void DeActivateButton()
    {
        GUI_Button_First.interactable = false;
        GUI_Button_Second.interactable = false;
        GUI_Button_Third.interactable = false;
    }

    private void ActivateButton()
    {
        GUI_Button_First.interactable = true;
        GUI_Button_Second.interactable = true;
        GUI_Button_Third.interactable = true;
    }
}
