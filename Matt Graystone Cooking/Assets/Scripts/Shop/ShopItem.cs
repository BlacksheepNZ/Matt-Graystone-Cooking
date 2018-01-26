//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections.Generic;
//using System;

//public class ShopItem : MonoBehaviour
//{
//    public int ID;
//    public string Name;
//    public float Price;

//    public Text NameText;
//    public Text PriceText;
//    public Button Button;

//    public void UpdateText(int id, string name, float price)
//    {
//        ID = id;
//        Name = name;
//        Price = price;

//        NameText.text = Name;
//        PriceText.text = CurrencyConverter.Instance.GetCurrencyIntoString(Price);
//        Button.onClick.AddListener(() => {
//            PurchaseItem();
//        });
//    }

//    public void PurchaseItem()
//    {
//        if (Inventory.Instance.InventoryFull())
//        {
//            Debug.Log("Inventory Full");
//            return;
//        }

//        if (Price <= 0)
//        {
//            Debug.Log("No Price Set");
//            return;
//        }

//        if(Price <= Game.Instance.TotalGold)
//        {
//            Game.Instance.RemoveGold(Price);
//            Inventory.Instance.AddItem(ID, 1);
//        }
//    }
//}
