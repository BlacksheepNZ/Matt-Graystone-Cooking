using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHoverSize : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private float Min = 1.0f;
    private float Max = 1.1f;

    public void OnPointerEnter(PointerEventData eventData)
    {
        this.gameObject.transform.localScale = new Vector3(Max, Max, Max);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.gameObject.transform.localScale = new Vector3(Min, Min, Min);
    }
}
