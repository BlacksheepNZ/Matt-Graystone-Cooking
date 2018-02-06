using System;
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

    //Bottom Panel
    public GameObject Panel_Pastry;
    public GameObject Panel_Larder;
    public GameObject Panel_Sauce;
    public GameObject Panel_Fish;
    public GameObject Panel_Vegetable;
    public GameObject Panel_Meat;

    //Middle Panel
    public GameObject Panel_Inventory;
    public GameObject Panel_Crafting;
    public GameObject Panel_Recipes;
    public GameObject Panel_Chef_Research_Tree;

    //Top Panel
    public GameObject Panel_Option;
    public GameObject Panel_Recipe_Preview;

    private void Start()
    {
        Close_Top_Panel();
    }

    public void Close_Top_Panel()
    {
        Close_Chef_Research_Tree();
        Close_Inventory();
        Close_Panel_Option();
        Close_Recipe_Preview();
    }

    public void Open_Recipe_Preview()
    {
        Close_Top_Panel();

        Open_Panel(Panel_Recipe_Preview);
    }

    private void Close_Recipe_Preview()
    {
        Close_Panel(Panel_Recipe_Preview);
    }

    public void Open_Panel_Option()
    {
        Close_Top_Panel();

        Open_Panel(Panel_Option);
    }

    public void Close_Panel_Option()
    {
        Close_Panel(Panel_Option);
    }

    public void Open_Panel_Pastry()
    {
        Open_Panel(Panel_Pastry);
    }

    public void Open_Panel_Larder()
    {
        Open_Panel(Panel_Larder);
    }

    public void Open_Panel_Sauce()
    {
        Open_Panel(Panel_Sauce);
    }

    public void Open_Panel_Fish()
    {
        Open_Panel(Panel_Fish);
    }

    public void Open_Panel_Vegetable()
    {
        Open_Panel(Panel_Vegetable);
    }

    public void Open_Panel_Meat()
    {
        Open_Panel(Panel_Meat);
    }

    public void Open_Recipes()
    {
        Close_Top_Panel();

        Open_Panel(Panel_Recipes);
    }

    public void Open_Inventory()
    {
        Close_Top_Panel();

        Open_Panel(Panel_Inventory);
    }
    public void Close_Inventory()
    {
        Close_Panel(Panel_Inventory);
    }

    public void Open_Crafting()
    {
        Open_Panel(Panel_Crafting);
    }
    public void Close_Crafting()
    {
        Close_Panel(Panel_Crafting);
    }

    public void Open_Chef_Research_Tree()
    {
        Close_Top_Panel();

        Open_Panel(Panel_Chef_Research_Tree);
    }
    public void Close_Chef_Research_Tree()
    {
        Close_Panel(Panel_Chef_Research_Tree);
    }

    private void Open_Panel(GameObject game_object)
    {
        game_object.gameObject.transform.localPosition = Vector3.zero;
        game_object.transform.SetAsLastSibling();
    }

    private void Close_Panel(GameObject game_object)
    {
        game_object.gameObject.transform.localPosition = new Vector3(HidenPosition, 0, 0);
    }
}
