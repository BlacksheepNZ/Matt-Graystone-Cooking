using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
[Serializable]
public class RecipeData : MonoBehaviour
{
    /// <summary>
    /// GUI
    /// </summary>
    public Text GUI_Text_Name;

    /// <summary>
    /// GUI
    /// </summary>
    public Text GUI_Text_Requirement;

    /// <summary>
    /// GUI
    /// </summary>
    public Text GUI_Text_Sell_Value;

    /// <summary>
    /// GUI
    /// </summary>
    public Button GUI_Button_Purchase;

    /// <summary>
    /// GUI
    /// </summary>
    public Image GUI_Preview_Sprite;

    /// <summary>
    /// GUI
    /// </summary>
    public ProgressionBar GUI_Progress_Bar;

    /// <summary>
    /// GUI
    /// </summary>
    public Button GUI_Button_Start;

    /// <summary>
    /// GUI
    /// </summary>
    public Button GUI_Button_Stop;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public Recipe recipe;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public int AmountSellMuiltplyer = 1;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public bool OnComplete;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public bool StartedTimer;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public float CurrentTime = 0;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    public void Start()
    {
        GUI_Text_Name.text = recipe.Name;
        GUI_Preview_Sprite.sprite = recipe.Preview_Image;

        //GUI_Button_Purchase.onClick.AddListener(() => Purchase());

        GUI_Button_Start.onClick.AddListener(() => OnStart());
        GUI_Button_Stop.onClick.AddListener(() => OnStop());
    }

    Coroutine co;

    private void OnStart()
    {
        GUI_Button_Start.gameObject.SetActive(false);
        GUI_Button_Stop.gameObject.SetActive(true);
        co = StartCoroutine(UpdateTimer());
    }

    private void OnStop()
    {
        GUI_Button_Start.gameObject.SetActive(true);
        GUI_Button_Stop.gameObject.SetActive(false);
        StopCoroutine(co);
    }

    public IEnumerator UpdateTimer()
    {
        StartedTimer = true;
        OnComplete = false;

        float speed = (Time.fixedDeltaTime / 1);

        while (GUI_Progress_Bar.Value < 1)
        {
            GUI_Progress_Bar.Value += speed;
            CurrentTime = GUI_Progress_Bar.Value;

            yield return null;
        }

        GUI_Progress_Bar.Value = 0;
        ResetTimer();
    }

    void ResetTimer()
    {
        if (!Inventory.Instance.InventoryFull())
        {
            Purchase();
            OnComplete = true;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void Unlock()
    {
        if (recipe.Unlocked == true)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    public void Update()
    {
        UpdateText();
    }

    /// <summary>
    /// 
    /// </summary>
    public override string ToString()
    {
        return (recipe.SellValue * AmountSellMuiltplyer).ToString();
    }

    /// <summary>
    /// 
    /// </summary>
    public void UpdateText()
    {
        GUI_Text_Sell_Value.text = "$" + recipe.SellValue * AmountSellMuiltplyer;
        GUI_Text_Requirement.text = StringBuilder();
    }

    /// <summary>
    /// 
    /// </summary>
    public string StringBuilder()
    {
        string x = "";

        for(int i = 0; i < recipe.Items.Count; i++)
        {
            Item item = GetItemByID(recipe.Items[i].ItemID);
            if (item != null)
            {
                int item_count = Inventory.Instance.CheckItemCount(recipe.Items[i].ItemID);
                if (item_count >= recipe.Items[i].Count * AmountSellMuiltplyer)
                {
                    string value = item.Name + " " + "x" + recipe.Items[i].Count * AmountSellMuiltplyer + " \n";
                    x += ColorString("FFA500", value);
                }
                else
                {
                    string value = item.Name + " " + "x" + recipe.Items[i].Count * AmountSellMuiltplyer + " \n";
                    x += ColorString("FFFFFF", value);
                }
            }
        }

        return x;
    }

    /// <summary>
    /// 
    /// </summary>
    public string ColorString(string color, string input)
    {
       return "<color=#" + color + ">" + input + "</color>";
    }

    /// <summary>
    /// 
    /// </summary>
    public Item GetItemByID(int id)
    {
        for (int y = 0; y < SaveLoad.Instance.Item_Database.Count; y++)
        {
            Item i = SaveLoad.Instance.Item_Database[y];

            if (i.ID == id)
                return i;
        }

        return null;
    }

    /// <summary>
    /// 
    /// </summary>
    public void Purchase()
    {
        int have_item_count = 0;

        if (recipe == null)
        {
            Debug.Log("No recipe found.");
            return;
        }

        for (int i = 0; i < recipe.Items.Count; i++)
        {
            Item item = GetItemByID(recipe.Items[i].ItemID);
            if (item != null)
            {
                int count = Inventory.Instance.CheckItemCount(item.ID);

                if (count > AmountSellMuiltplyer)
                {
                    have_item_count += 1;
                }
                else
                {
                    Debug.Log("need more resources");
                    break;
                }//ok
            }
        }

        //have desired items to craft
        if (have_item_count == recipe.Items.Count)
        {
            for (int i = 0; i < recipe.Items.Count; i++)
            {
                for (int x = 0; x < Inventory.Instance.slots.Count; x++)
                {
                    GameObject slot = Inventory.Instance.slots[x];
                    if (slot.transform.childCount > 0)
                    {
                        ItemData item = slot.transform.GetChild(0).GetComponent<ItemData>();

                        if (item.Item.ID == recipe.Items[i].ItemID)
                        {
                            Inventory.Instance.RemoveItem(recipe.Items[i].ItemID, recipe.Items[i].Count * AmountSellMuiltplyer, Inventory.Instance.slots[item.slot].GetComponent<Slot>());
                        }
                    }
                }
            }

            OnStart();
            Game.Instance.AddGold(recipe.SellValue * AmountSellMuiltplyer);
            Debug.Log("AddGold :" + recipe.SellValue * AmountSellMuiltplyer);
        }
        else
        {
            OnStop();
            Debug.Log("no item");
        }
    }
}
