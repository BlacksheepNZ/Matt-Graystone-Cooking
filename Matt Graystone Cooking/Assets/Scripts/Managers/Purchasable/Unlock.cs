using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Unlock : MonoBehaviour
{
    public Text NameText;
    public Text DecriptionText;
    public Sprite Image;

    public string Name;
    public string Decription;

    public bool isUnlocked;

    public float Cost;

    public Unlock(string name, string decription, Sprite image)
    {
        Name = name;
        Decription = decription;
        Image = image;
        isUnlocked = false;
    }

    public bool CanPurchase()
    {
        if(Cost >= 0)//change later to currency
        {
            //remove gold todo
            return true;
        }
        return false;
    }
}
    
