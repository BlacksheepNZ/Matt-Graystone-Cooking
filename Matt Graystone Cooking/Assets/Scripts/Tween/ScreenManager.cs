using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    /// <summary>
    /// Instantiate class object
    /// </summary>
    public static ScreenManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<ScreenManager>();
            }

            return global::ScreenManager.instance;
        }
    }
    private static ScreenManager instance;

    /// <summary>
    /// used to hold all screen data
    /// </summary>
    public List<GameObject> Screens;

    public List<GameObject> Buttons;

    public ScreenManager()
    {
        //for (int i = 0; i < 5; i++)
        //{
        //    Buttons[i].SetActive(false);
        //}
    }

    public void OpenScreen()
    {
        //Add MainScreen
        Screens[0].SetActive(true);
        Screens[0].GetComponent<Gui_Anim>().Fade_In();
    }

    public void CloseScreen(int id)
    {
        if (Screens[id] == null)
            return;

        Screens[id].SetActive(true);
        Screens[id].GetComponent<Gui_Anim>().Fade_Out();
    }

    public void CloseScreen(GameObject gameObject)
    {
        if (gameObject == null)
            return;

        gameObject.SetActive(true);
        gameObject.GetComponent<Gui_Anim>().Fade_Out();
    }

    public void CloseAllScreen()
    {
        if (Screens.Count == 0)
            return;

        for (int i = 0; i < Screens.Count; i++)
        {
            GameObject screen = Screens[i];

            if (screen == null)
                return;

            if (screen.activeInHierarchy == false)
                return;

            screen.GetComponent<Gui_Anim>().Fade_Out();
        }
    }
}