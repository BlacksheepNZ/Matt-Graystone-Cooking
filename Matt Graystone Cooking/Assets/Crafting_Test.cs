using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Crafting_Test : MonoBehaviour
{
    public int ItemAdded;
    public List<int> ItemList = new List<int>();

    public void AddItem(int itemID)
    {
        ItemList.Add(itemID);
    }

    //public void FindRecipe(int itemID)
    //{
    //    for (int i = 0; i < SaveLoad; i++)
    //    {

    //    }
    //}
}
