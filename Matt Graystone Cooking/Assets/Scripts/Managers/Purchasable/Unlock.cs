using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class Unlock : MonoBehaviour
{
    /// <summary>
    /// GUI
    /// </summary>
    public Text NameText;

    /// <summary>
    /// GUI
    /// </summary>
    public Text DecriptionText;

    /// <summary>
    /// GUI
    /// </summary>
    public Sprite Image;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public string Name;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public string Decription;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public bool isUnlocked;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public float Cost;

    /// <summary>
    /// 
    /// </summary>
    public Unlock(string name, string decription, Sprite image)
    {
        Name = name;
        Decription = decription;
        Image = image;
        isUnlocked = false;
    }

    /// <summary>
    /// 
    /// </summary>
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
    
