using System;
using UnityEngine;
using UnityEngine.EventSystems;
 
public class TabManager : MonoBehaviour
{
    public GameObject Parent;

    public void SetActive()
    {
        Parent.gameObject.SetActive(true);
    }

    public void SetDeActive()
    {
        Parent.gameObject.SetActive(false);
    }
}

