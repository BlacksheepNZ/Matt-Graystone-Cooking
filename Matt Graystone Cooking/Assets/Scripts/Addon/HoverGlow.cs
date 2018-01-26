using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverGlow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private Outline Outline;
    private Color color;
    private float Alpha = 1;
    public float Speed;
    public float Max = 1;
    public float Min = 0;

    bool isFinished = false;

    // Use this for initialization
    void Start () {
        Outline = this.gameObject.transform.GetComponent<Outline>();
        color = Outline.effectColor;
        Alpha = color.a;
    }

    IEnumerator Show()
    {
        while (Alpha >= Min)
        {
            Alpha -= (Alpha * Time.deltaTime) * Speed;

            //Alpha = Mathf.Clamp(Alpha, Min, Max);

            if (Alpha <= Min)
            {
                isFinished = true;
            }

            color.a = Alpha;
            Outline.effectColor = color;

            yield return null;
        }
    }

    IEnumerator Hide()
    {
        while (Alpha <= Max)
        {
            Alpha += (Alpha * Time.deltaTime) * Speed;

            //Alpha = Mathf.Clamp(Alpha, Min, Max);

            if (Alpha >= Max)
            {
                isFinished = true;
            }

            color.a = Alpha;
            Outline.effectColor = color;

            yield return null;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StopCoroutine(Hide());
        Debug.Log("enter");
        StartCoroutine(Show());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopCoroutine(Show());
        Debug.Log("exit");
        StartCoroutine(Hide());
    }
}
