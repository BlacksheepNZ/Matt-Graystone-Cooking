using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Gui_Anim : MonoBehaviour
{
    public Gui_Anim_Component FadeOut;
    public Gui_Anim_Component FadeIn;

    private bool active;

    public void Start()
    {
        FadeOut.SetGameObject(this.gameObject);
        FadeIn.SetGameObject(this.gameObject);
    }
    public void Fade_Out()
    {
        StartCoroutine(FadeOut.FadeOut(true));
    }
    public void Fade_In()
    {
        gameObject.SetActive(true);

        StopCoroutine(FadeOut.FadeOut(true));
        StartCoroutine(FadeIn.FadeIn(true));
    }
}
