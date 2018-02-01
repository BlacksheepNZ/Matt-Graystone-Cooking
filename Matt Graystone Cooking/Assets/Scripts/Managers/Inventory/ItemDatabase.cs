using UnityEngine;
using System.Collections.Generic;
using LitJson;
using System.IO;
using System;

public class ItemDatabase : MonoBehaviour
{
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

    private static ItemDatabase instance;
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
}
