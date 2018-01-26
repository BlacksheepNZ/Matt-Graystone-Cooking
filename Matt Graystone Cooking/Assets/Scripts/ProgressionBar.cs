using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressionBar : MonoBehaviour
{
    public Image foregroundImage;

    [Range(0, 1)]
    [Tooltip("Value between 0 and 1")]
    public float Value = 0;

    void Start()
    {
        foregroundImage = gameObject.GetComponent<Image>();
        Value = 0;
    }

    public void Update()
    {
        if (foregroundImage != null)
            foregroundImage.fillAmount = Value;

        //Bar.transform.localScale = new Vector3(
        //    Current,
        //    Bar.transform.localScale.y,
        //    Bar.transform.localScale.z);
    }
}
