using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// need to change later this can be set by the gui Panel_Tab
/// </summary>
public class Tabs : MonoBehaviour {

    private static Tabs instance;
    public static Tabs Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Tabs>();
            }

            return Tabs.instance;
        }
    }

    public int HidenPosition = 0;

    //Bottom
    public GameObject Panel_Pastry;
    public GameObject Panel_Larder;
    public GameObject Panel_Sauce;
    public GameObject Panel_Fish;
    public GameObject Panel_Vegetable;
    public GameObject Panel_Meat;

    //MiddlePanel
    public GameObject Panel_Inventory;
    public GameObject Panel_Crafting;
    public GameObject Panel_Recipes;
    public GameObject Panel_ChefResearchTree;

    private void Start()
    {
        Close_Inventory();
        Close_Crafting();
        Close_Chef_Research_Tree();
    }

    public void Open_Panel_Pastry()
    {
        Panel_Pastry.gameObject.transform.localPosition = Vector3.zero;
        Panel_Pastry.transform.SetAsLastSibling();
    }

    public void Open_Panel_Larder()
    {
        Panel_Larder.gameObject.transform.localPosition = Vector3.zero;
        Panel_Larder.transform.SetAsLastSibling();
    }

    public void Open_Panel_Sauce()
    {
        Panel_Sauce.gameObject.transform.localPosition = Vector3.zero;
        Panel_Sauce.transform.SetAsLastSibling();
    }

    public void Open_Panel_Fish()
    {
        Panel_Fish.gameObject.transform.localPosition = Vector3.zero;
        Panel_Fish.transform.SetAsLastSibling();
    }

    public void Open_Panel_Vegetable()
    {
        Panel_Vegetable.gameObject.transform.localPosition = Vector3.zero;
        Panel_Vegetable.transform.SetAsLastSibling();
    }

    public void Open_Panel_Meat()
    {
        Panel_Meat.gameObject.transform.localPosition = Vector3.zero;
        Panel_Meat.transform.SetAsLastSibling();
    }

    public void Open_Recipes()
    {
        Panel_Recipes.gameObject.transform.localPosition = Vector3.zero;
        Panel_Recipes.transform.SetAsLastSibling();
    }

    public void Open_Inventory()
    {
        Panel_Inventory.gameObject.transform.localPosition = Vector3.zero;
        Panel_Inventory.transform.SetAsLastSibling();

        Close_Chef_Research_Tree();
    }
    public void Close_Inventory()
    {
        Panel_Inventory.gameObject.transform.localPosition = new Vector3(HidenPosition, 0, 0);
    }

    public void Open_Crafting()
    {
        Panel_Crafting.gameObject.transform.localPosition = Vector3.zero;
        Panel_Crafting.transform.SetAsLastSibling();
    }
    public void Close_Crafting()
    {
        Panel_Crafting.gameObject.transform.localPosition = new Vector3(HidenPosition, 0, 0);
    }

    public void Open_Chef_Research_Tree()
    {
        Panel_ChefResearchTree.gameObject.transform.localPosition = Vector3.zero;
        Panel_ChefResearchTree.transform.SetAsLastSibling();

        Close_Inventory();
    }
    public void Close_Chef_Research_Tree()
    {
        Panel_ChefResearchTree.gameObject.transform.localPosition = new Vector3(HidenPosition, 0, 0);
    }
}
