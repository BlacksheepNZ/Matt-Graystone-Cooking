using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Character : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    private bool unlocked;
    /// <summary>
    /// 
    /// </summary>
    private Button button;
    /// <summary>
    /// 
    /// </summary>
    private int CostToPurchase;

	void Start ()
    {
        unlocked = false;
        
        button = GetComponent<Button>();
        button.interactable = false;
        button.onClick.AddListener(() => OnClick());
    }

    private void OnClick()
    {
        throw new NotImplementedException();
    }
}
