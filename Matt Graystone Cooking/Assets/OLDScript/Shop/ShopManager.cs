//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using UnityEngine;
//using UnityEngine.UI;

//public class ShopManager : MonoBehaviour
//{
//    private static ShopManager instance;
//    public static ShopManager Instance
//    {
//        get
//        {
//            if (instance == null)
//            {
//                instance = GameObject.FindObjectOfType<ShopManager>();
//            }

//            return ShopManager.instance;
//        }
//    }

//    public GameObject ShopObject;

//    public GameObject ShopItemPrefab;
//    public Transform ShopItemParent;

//    public Button SellButton;
//    public Button SellAllButton;
//    public Button CompleteSell;

//    public GameObject CompleteSellPrefab;
//    public Text CompleteSellText;
//    public Button CompleteButtonYes;
//    public Button CompleteButtonNo;

//    public List<int> ItemID = new List<int>();

//    private void Start()
//    {
//        SellButton.onClick.AddListener(() =>
//        {
//            Inventory.Instance.SellItem();
//        });
//        SellAllButton.onClick.AddListener(() =>
//        {
//            Inventory.Instance.SelectAll();
//        });

//        CompleteSell.onClick.AddListener(() =>
//        {
//            if (Inventory.Instance.SellItemCount() <= 0)
//                return;

//            CompleteSellPrefab.SetActive(true);

//            CompleteSellText.text = "Are you sure you want to sell "
//            + Inventory.Instance.SellItemCount() +
//            " items?";

//            CompleteButtonYes.onClick.AddListener(() =>
//            {
//                Inventory.Instance.ConfirmSell();
//                CompleteSellPrefab.SetActive(false);
//            });
//            CompleteButtonNo.onClick.AddListener(() =>
//            {
//                Inventory.Instance.UnSelectAll();
//                CompleteSellPrefab.SetActive(false);
//            });
//        });
//    }

//    public void Update()
//    {
//        if(Input.GetKeyDown(KeyCode.Alpha1))
//        {
//            ShowShop();
//            PopulateItems();
//        }
//    }

//    public void ShowShop()
//    {
//        ShopObject.SetActive(true);
//    }

//    public void CloseShop()
//    {
//        ShopItems.Clear();
//        for (int i = 0; i < ShopItemParent.transform.childCount; i++)
//        {
//            Destroy(ShopItemParent.GetChild(i).gameObject);
//        } 
//        ShopObject.SetActive(false);
//    }

//    List<GameObject> ShopItems = new List<GameObject>();
//    private void PopulateItems()
//    {
//        for(int i = 0; i < SaveLoad.Instance.ItemDatabase.Count(); i++)
//        {
//            Item item = SaveLoad.Instance.ItemDatabase[i];
//            for (int x = 0; x < ItemID.Count; x++)
//            {
//                if (item.ID == ItemID[x])
//                {
//                    ShopItems.Add(CreateItem(item));
//                }
//            }
//        }

//        for (int i = 0; i < SaveLoad.Instance.PowerNodeItemDatabase.Count(); i++)
//        {
//            Item item = SaveLoad.Instance.PowerNodeItemDatabase[i];
//            for (int x = 0; x < ItemID.Count; x++)
//            {
//                if (item.ID == ItemID[x])
//                {
//                    ShopItems.Add(CreateItem(item));
//                }
//            }
//        }
//    }

//    private GameObject CreateItem(Item item)
//    {
//        GameObject shopItemObject = Instantiate(ShopItemPrefab);
//        shopItemObject.transform.SetParent(ShopItemParent);
//        shopItemObject.transform.localPosition = Vector3.zero;
//        shopItemObject.transform.localScale = Vector3.one;
//        shopItemObject.SetActive(true);

//        ShopItem shopItem = shopItemObject.GetComponent<ShopItem>();
//        shopItem.UpdateText(item.ID, item.Name, item.CostToPurchase);

//        return shopItemObject;
//    }
//}
