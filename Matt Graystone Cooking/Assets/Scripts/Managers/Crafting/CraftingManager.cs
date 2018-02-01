using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

[System.Serializable]
public class CraftingManager : MonoBehaviour
{
    private static CraftingManager instance;
    public static CraftingManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<CraftingManager>();
            }

            return CraftingManager.instance;
        }
    }

    public GameObject panel;
    private int slotCount = 9;

    private string LastKey = "000000000000000000000000000";
    public string CurrentKey = "";
    public Button Button;

    private void Start()
    {
        for (int i = 0; i < slotCount; i++)
        {
            Inventory.Instance.AddSlot(panel, ItemType.Consumable);
        }

        LastKey = CurrentKey;
        Button.interactable = false;

        //direct add
        //UnlockRecipe(SaveLoad.Instance.Recipe_Data[0], false);
    }

    private string KeyBuilder()
    {
        string key_builder = "";

        List<int> slotId = Inventory.Instance.SlotsToCheck;

        for (int i = 0; i < slotId.Count; i++)
        {
            if (Inventory.Instance.slots[slotId[i]].transform.childCount > 0)
            {
                int id = Inventory.Instance.slots[slotId[i]].transform.GetChild(0).GetComponent<ItemData>().Item.ID;
                key_builder += id;
            }
            else
            {
                key_builder += "000";
            }
        }

        return key_builder;
    }

    public void Update()
    {
        CurrentKey = KeyBuilder();

        if (LastKey != CurrentKey)
        {
            Recipe repice = CheckForKey(CurrentKey);
            if (repice != null && repice.Unlocked == false)
            {
                //display repice found
                //await user input to craft
                Button.interactable = true;
                //add repice to prefab tab
                Button.onClick.AddListener(() => UnlockRecipe(repice, true));
            }

            //no repice found
            LastKey = CurrentKey;
        }
    }

    public void UnlockRecipe(Recipe recipe, bool showUnlock)
    {
        for (int i = 0; i < SaveLoad.Instance.Recipe_Item.Count; i++)
        {
            RecipeData r = SaveLoad.Instance.Recipe_Item[i].GetComponent<RecipeData>();
            if (r.recipe.Name == recipe.Name)
            {
                r.recipe.Unlocked = true;
                SaveLoad.Instance.Recipe_Item[i].SetActive(true);
                Button.interactable = false;

                if(showUnlock == true)
                    gameObject.GetComponent<RecipeUnlock>().Set(
                        r.recipe.Name, 
                        r.StringBuilder(),
                        r.recipe.SellValue.ToString());

                MoveAllItemToInventory();
            }
        }
    }

    public void MoveAllItemToInventory()
    {
        int s = Inventory.Instance.slots.Count(x => x.GetComponent<Slot>().ItemType == ItemType.Consumable);
        Item z = Inventory.Instance.GetItemFromSlot(s);

        Inventory.Instance.MoveItemToSlot(z, Inventory.Instance.slots[Inventory.Instance.GetEmptySlot()].GetComponent<Slot>());
    }

    public Recipe CheckForKey(string key)
    {
        for (int i = 0; i < SaveLoad.Instance.Recipe_Data.Count; i++)
        {
            if (SaveLoad.Instance.Recipe_Data[i].Key == key)
            {
                return SaveLoad.Instance.Recipe_Data[i];
            }
        }

        return null;
    }
}
