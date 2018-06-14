using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class MessageBox : MonoBehaviour
{
    /// <summary>
    /// Instantiate class object
    /// </summary>
    public static MessageBox Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<MessageBox>();
            }

            return MessageBox.instance;
        }
    }
    private static MessageBox instance;

    /// <summary>
    /// GUI
    /// </summary>
    public Text GUI_Text_Title;

    /// <summary>
    /// GUI
    /// </summary>
    public Text GUI_Text_Decription;

    /// <summary>
    /// 
    /// </summary>
    public void SetText(string title, string decription)
    {
        Tabs.Instance.Open_Message_Box();

        GUI_Text_Title.text = title;
        GUI_Text_Decription.text = decription;
    }
}
