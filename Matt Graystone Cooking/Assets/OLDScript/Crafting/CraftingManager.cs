using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class CraftingManager : MonoBehaviour
{
    public GameObject TabHolder_Crafting;
    public GameObject TabHolder_Inventory;
    public GameObject Player;

    private bool triggerEntered = false;

    private void Update()
    {
        // We check if user pressed PickUp key and character is inside trigger
        if (Input.GetButtonDown("PickUp") && triggerEntered == true)
        {
            TabHolder_Crafting.GetComponent<Gui_Anim>().Move_In();
            TabHolder_Inventory.GetComponent<Gui_Anim>().Move_In();
            PickupDialog.Instance.DeActivate();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject == Player)
        {
            PickupDialog.Instance.Activate("E", " Crafting Station");
            triggerEntered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PickupDialog.Instance.DeActivate();
        triggerEntered = false;
        TabHolder_Crafting.GetComponent<Gui_Anim>().Move_Out_Instant();
        TabHolder_Inventory.GetComponent<Gui_Anim>().Move_Out_Instant();
    }
}

//[System.Serializable]
//public class CraftingManager : MonoBehaviour
//{
//    private static CraftingManager instance;
//    public static CraftingManager Instance
//    {
//        get
//        {
//            if (instance == null)
//            {
//                instance = GameObject.FindObjectOfType<CraftingManager>();
//            }

//            return CraftingManager.instance;
//        }
//    }

//    public GameObject panel;
//    public GameObject panel_preview;
//    private int slotCount = 9;

//    private string LastKey = "000000000000000000000000000";
//    public string CurrentKey = "";
//    public Button Button;

//    private void Start()
//    {
//        for (int i = 0; i < slotCount; i++)
//        {
//            Inventory.Instance.AddSlot(panel, ItemType.Consumable);
//        }

//        LastKey = CurrentKey;
//        Button.interactable = false;

//        //direct add
//        //UnlockRecipe(SaveLoad.Instance.Recipe_Data[0], false);
//    }

//    private string KeyBuilder()
//    {
//        string key_builder = "";

//        List<int> slotId = Inventory.Instance.SlotsToCheck;

//        for (int i = 0; i < slotId.Count; i++)
//        {
//            if (Inventory.Instance.slots[slotId[i]].transform.childCount > 0)
//            {
//                int id = Inventory.Instance.slots[slotId[i]].transform.GetChild(0).GetComponent<ItemData>().Item.ID;
//                key_builder += id;
//            }
//            else
//            {
//                key_builder += "000";
//            }
//        }

//        return key_builder;
//    }

//    public void Update()
//    {
//        CurrentKey = KeyBuilder();

//        if (LastKey != CurrentKey)
//        {
//            Recipe repice = CheckForKey(CurrentKey);
//            if (repice != null && repice.Unlocked == false)
//            {
//                panel_preview.GetComponent<Image>().sprite = repice.Preview_Image;

//                //display repice found
//                //await user input to craft
//                Button.interactable = true;
//                //add repice to prefab tab
//                Button.onClick.AddListener(() => UnlockRecipe(repice, true));
//            }
//            else
//            {
//                panel_preview.GetComponent<Image>().sprite = SaveLoad.Instance.Item_Images[0];// repice.Preview_Image;
//                Button.interactable = false;

//                //no repice found
//                LastKey = CurrentKey;
//            }
//        }
//    }

//    public void UnlockRecipe(Recipe recipe, bool showUnlock)
//    {
//        for (int i = 0; i < SaveLoad.Instance.Recipe_Item.Count; i++)
//        {
//            RecipeData r = SaveLoad.Instance.Recipe_Item[i].GetComponent<RecipeData>();
//            if (r.recipe.Name == recipe.Name)
//            {
//                r.recipe.Unlocked = true;
//                SaveLoad.Instance.Recipe_Item[i].SetActive(true);
//                Button.interactable = false;

//                if(showUnlock == true)
//                    gameObject.GetComponent<RecipeUnlock>().Set(
//                        r.recipe.Name, 
//                        r.StringBuilder(),
//                        r.recipe.SellValue.ToString());

//                MoveAllItemToInventory();
//            }
//        }
//    }

//    public void MoveAllItemToInventory()
//    {
//        List<GameObject> item_in_slot = new List<GameObject>();
//        for (int i = 0; i < Inventory.Instance.slots.Count; i++)
//        {
//            if (Inventory.Instance.slots[i].GetComponent<Slot>().ItemType == ItemType.Consumable)
//            {
//                if (Inventory.Instance.slots[i].transform.childCount > 0)
//                {
//                    item_in_slot.Add(Inventory.Instance.slots[i]);
//                }
//            }
//        }

//        for (int i = 0; i < item_in_slot.Count; i++)
//        {
//            GameObject to_slot = Inventory.Instance.slots[Inventory.Instance.GetEmptySlot()];

//            Inventory.Instance.MoveItemToSlot(item_in_slot[i], to_slot);

//            //Debug.Log("from " + item_in_slot[i] + " " + "to" + to_slot.GetComponent<Slot>().ID);
//        }
//    }

//    public Recipe CheckForKey(string key)
//    {
//        for (int i = 0; i < SaveLoad.Instance.Recipe_Item.Count; i++)
//        {
//            Recipe RecipeData = SaveLoad.Instance.Recipe_Item[i].GetComponent<RecipeData>().recipe;

//            if (RecipeData.Key == key)
//            {
//                return RecipeData;
//            }
//        }

//        return null;
//    }
//}
