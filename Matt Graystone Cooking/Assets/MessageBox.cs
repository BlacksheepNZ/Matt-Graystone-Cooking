using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MessageBox : MonoBehaviour
{
    private static MessageBox instance;
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

    public Text Text_Title;
    public Text Text_Decription;

    public void SetText(string title, string decription)
    {
        Tabs.Instance.Open_Message_Box();

        Text_Title.text = title;
        Text_Decription.text = decription;
    }
}
