using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Gui_Anim : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool MouseFadeOnRelease;

    public Gui_Anim_Component MoveOut;
    public Gui_Anim_Component MoveIn;

    public Gui_Anim_Component FadeOut;
    public Gui_Anim_Component FadeIn;

    private bool active;

    public void Start()
    {
        MoveOut.SetGameObject(this.gameObject);
        MoveIn.SetGameObject(this.gameObject);
        FadeOut.SetGameObject(this.gameObject);
        FadeIn.SetGameObject(this.gameObject);
    }

    public void Move_Out_Instant()
    {
        MoveOut.MoveInstant(false);
    }
    public void Move_Out()
    {
        StartCoroutine(MoveOut.MoveOverSeconds(false));
    }
    public void Move_In()
    {
        StopCoroutine(MoveOut.MoveOverSeconds(false));
        StartCoroutine(MoveIn.MoveOverSeconds(true));
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (MouseFadeOnRelease == true)
        {
            StopCoroutine(MoveOut.MoveOverSeconds(true));
            StartCoroutine(MoveIn.MoveOverSeconds(true));
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (MouseFadeOnRelease == true)
        {
            StartCoroutine(MoveOut.MoveOverSeconds(true));
        }
    }
}
