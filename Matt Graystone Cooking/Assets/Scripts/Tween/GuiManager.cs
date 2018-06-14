using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GuiManager : MonoBehaviour
{
    public List<Gui_Anim> Gui_Anim_Tabs_To_Close;

    public void Close_Tab()
    {
        for (int i = 0; i < Gui_Anim_Tabs_To_Close.Count; i++)
        {
           //Gui_Anim_Tabs_To_Close[i].Move_Out_Instant();
        }
    }
}
