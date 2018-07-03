using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class HeroView : MonoBehaviour
{
    public GameObject Panel;
    public Text GUI_Text;

    public int SlotCount = 0;
    public List<int> SlotID = new List<int>();

    private void Start()
    {
        for (int i = 0; i < SlotCount; i++)
        {
            Inventory.Instance.AddSlot(Panel, SlotType.Item);
            SlotID.Add(Inventory.Instance.slots.Count);
        }
    }

    private void GetOccuipedSlot()
    {
        for (int i = 0; i < SlotID.Count; i++)
        {
            if(Inventory.Instance.slots[i].transform.childCount > 0)
            {
                //we have an item in the slot
                //do stuff
            }
        }
    }

    public void SetText(string text)
    {
        GUI_Text.text = text;
    }
}
