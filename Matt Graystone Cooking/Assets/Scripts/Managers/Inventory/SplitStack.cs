using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class SplitStack : MonoBehaviour
{
    public Text Text_Count;
    public Slider Slider;
    public Button Button_OK;
    public float newCount;
    public GameObject ItemData;

    public void Start()
    {
        Button_OK.onClick.AddListener(() =>
        {
            if (Slider.value > 0)
            {
                if (ItemData != null)
                {
                    ItemData i = ItemData.transform.GetChild(0).GetComponent<ItemData>();

                    Inventory.Instance.SplitItem(i, (int)newCount);
                    Inventory.Instance.HideSplitStack(i);

                    newCount = 0;
                    Slider.value = 0;
                    return;
                }
            }
        });
    }

    public void OnValueChanged(ItemData item)
    {
        if (ItemData != null)
        {
            Slider.onValueChanged.AddListener(delegate
            {
                ValueChangeCheck(item);
            });
        }
    }

    private void ValueChangeCheck(ItemData item)
    {
        if (ItemData != null)
        {
            ItemData i = ItemData.transform.GetChild(0).GetComponent<ItemData>();

            newCount = ((float)i.count * Slider.value) - 1;
            if (newCount > 0.000f)
            {
                Text_Count.text = newCount.ToString();
            }
            else
            {
                Text_Count.text = "0";
            }

            Slider.minValue = 0;
            Slider.maxValue = 1;
        }
    }
}
