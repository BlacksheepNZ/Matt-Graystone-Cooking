using UnityEngine;
using System.Collections.Generic;
using LitJson;
using System.IO;
using System;

/// <summary>
/// 
/// </summary>
public class ItemDatabase : MonoBehaviour
{
    /// <summary>
    /// Instantiate class object
    /// </summary>
    public static ItemDatabase Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<ItemDatabase>();
            }

            return ItemDatabase.instance;
        }
    }
    private static ItemDatabase instance;

    /// <summary>
    /// 
    /// </summary>
    public Item FetchItemByID(int id)
    {
        for (int i = 0; i < SaveLoad.Instance.Item_Database.Count; i++)
        {
            if (SaveLoad.Instance.Item_Database[i].ID == id)
            {
                return SaveLoad.Instance.Item_Database[i];
            }
        }

        return null;
    }
}
