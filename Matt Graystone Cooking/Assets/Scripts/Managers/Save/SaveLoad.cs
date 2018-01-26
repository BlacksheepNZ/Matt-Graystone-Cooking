using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;

using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.Collections;

public class SaveLoad : MonoBehaviour
{
    private static SaveLoad instance;
    public static SaveLoad Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<SaveLoad>();
            }

            return global::SaveLoad.instance;
        }
    }

    private string Save_Location
    {
        //C:/Users/matthew/AppData/LocalLow/Highwind/Idle_Journey
        get { return Application.persistentDataPath + "/savedGames.idle"; }
    }

    //private DateTime previousDateTime;
    //private DateTime currentDateTime;

    public List<Sprite> ItemImages = new List<Sprite>();
    public List<Sprite> BorderImages = new List<Sprite>();
    public List<Sprite> RarityImages = new List<Sprite>();

    //Item Sell Price
    public string itemSellLocation;
    private JsonData itemSellJsonData;
    public List<ItemSellValue> Item_Sell_Value = new List<ItemSellValue>();

    //Minerals Sell Price
    public string mineralSellLocation;
    private JsonData mineralSellJsonData;
    public List<MineralSellValue> Mineral_Sell_Value = new List<MineralSellValue>();

    //CraftingCost
    public string recipeCostLocation;
    private JsonData recipeCostJsonData;
    public List<RecipeCost> Recipe_Cost = new List<RecipeCost>();


    ////Items
    public string itemLocation;
    private JsonData ItemJsonData;
    public List<Item> Item_Database = new List<Item>();

    public GameObject Recipe_Prefab;
    public GameObject Purchasable_Prefab;

    //Recipe
    public string Recipe_Location = "";
    private JsonData Recipe_Json_Data = "";
    public Transform Recipe_Parent;
    public List<Recipe> Recipe_Data = new List<Recipe>();
    public List<GameObject> Recipe_Item = new List<GameObject>();

    //Pastry
    public string Pastry_Location = "";
    private JsonData Pastry_Json_Data = "";
    public Transform Pastry_Parent;
    public List<Purchasable> Pastry_Purchasable = new List<Purchasable>();
    public List<GameObject> Pastry_Item = new List<GameObject>();

    //Larder
    public string Larder_Location = "";
    private JsonData Larder_Json_Data;
    public Transform Larder_Parent;
    public List<Purchasable> Larder_Purchasable = new List<Purchasable>();
    public List<GameObject> Larder_Item = new List<GameObject>();

    //Sauce
    public string Sauce_Location = "";
    private JsonData Sauce_Json_Data;
    public Transform Sauce_Parent;
    public List<Purchasable> Sauce_Purchasable = new List<Purchasable>();
    public List<GameObject> Sauce_Item = new List<GameObject>();

    //Fish
    public string Fish_Location = "";
    private JsonData Fish_Json_Data;
    public Transform Fish_Parent;
    public List<Purchasable> Fish_Purchasable = new List<Purchasable>();
    public List<GameObject> Fish_Item = new List<GameObject>();

    //Vegetable
    public string Vegetable_Location = "";
    private JsonData Vegetable_Json_Data;
    public Transform Vegetable_Parent;
    public List<Purchasable> Vegetable_Purchasable = new List<Purchasable>();
    public List<GameObject> Vegetable_Item = new List<GameObject>();

    //Meat
    public string Meat_Location = "";
    private JsonData Meat_Json_Data;
    public Transform Meat_Parent;
    public List<Purchasable> Meat_Purchasable = new List<Purchasable>();
    public List<GameObject> Meat_Item = new List<GameObject>();

    string FileType = ".json";

    public void Json()
    {
        itemSellJsonData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/" + itemSellLocation + FileType));
        ItemSellJsonDataDatabase();

        mineralSellJsonData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/" + mineralSellLocation + FileType));
        MineralSellJsonDataDatabase();

        recipeCostJsonData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/" + recipeCostLocation + FileType));
        RecipeCostJsonDataDatabase();

        ItemJsonData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/" + itemLocation + FileType));
        ItemJsonDataDatabase();

        //Purchasable Section

        JsonDataDatabase(Recipe_Data, Recipe_Item, Recipe_Location, Recipe_Json_Data, Recipe_Parent, ItemType.General);

        JsonDataDatabase(Pastry_Purchasable, Pastry_Item, Pastry_Location, Pastry_Json_Data, Pastry_Parent, ItemType.Consumable);
        JsonDataDatabase(Larder_Purchasable, Larder_Item, Larder_Location, Larder_Json_Data, Larder_Parent, ItemType.Consumable);
        JsonDataDatabase(Sauce_Purchasable, Sauce_Item, Sauce_Location, Sauce_Json_Data, Sauce_Parent, ItemType.Consumable);
        JsonDataDatabase(Fish_Purchasable, Fish_Item, Fish_Location, Fish_Json_Data, Fish_Parent, ItemType.Consumable);
        JsonDataDatabase(Vegetable_Purchasable, Vegetable_Item, Vegetable_Location, Vegetable_Json_Data, Vegetable_Parent, ItemType.Consumable);
        JsonDataDatabase(Meat_Purchasable, Meat_Item, Meat_Location, Meat_Json_Data, Meat_Parent, ItemType.Consumable);
    }

    private void JsonDataDatabase(
    List<Recipe> purchasable,
    List<GameObject> item,
    string location,
    JsonData data,
    Transform Parent,
    ItemType item_Type)
    {
        try
        {
            string app_Path = Application.dataPath + "/StreamingAssets/" + location + FileType;
            data = JsonMapper.ToObject(File.ReadAllText(app_Path));

            for (int i = 0; i < data.Count; i++)
            {
                string name = (string)data[i]["Name"];
                string key = (string)data[i]["Key"];

                List<RecipeItem> ri = new List<RecipeItem>();

                for (int x = 0; x < data[i]["Recipe"].Count; x++)
                {
                    int id = (int)data[i]["Recipe"][x]["ItemID"];
                    int count = (int)data[i]["Recipe"][x]["Count"];

                    RecipeItem RecipeItem = new RecipeItem(id,count);
                    ri.Add(RecipeItem);
                }

                int sellvalue = (int)data[i]["SellValue"];

                purchasable.Add(new Recipe(
                    name,
                    key,
                    ri,
                    sellvalue));

                GameObject recipePrefab = Instantiate(Recipe_Prefab);
                recipePrefab.transform.SetParent(Parent);
                recipePrefab.transform.localScale = new Vector3(1, 1, 1);
                recipePrefab.transform.position = Parent.position;
                recipePrefab.SetActive(false);

                Recipe recipe_data = recipePrefab.GetComponent<RecipeData>().recipe;
                recipe_data.Name = purchasable[i].Name;
                recipe_data.Key = purchasable[i].Key;
                recipe_data.Items = purchasable[i].Items;
                recipe_data.SellValue = purchasable[i].SellValue;

                item.Add(recipePrefab);
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message + location);
        }
    }

    private void JsonDataDatabase(
        List<Purchasable> purchasable,
        List<GameObject> item,
        string location,
        JsonData data, 
        Transform Parent, 
        ItemType item_Type)
    {
        try
        {
            string app_Path = Application.dataPath + "/StreamingAssets/" + location + FileType;
            data = JsonMapper.ToObject(File.ReadAllText(app_Path));

            List<Purchasable> items = new List<Purchasable>();

            for (int i = 0; i < data.Count; i++)
            {
                int imageId = 0;

                ResourceType ResourceType = (ResourceType)Enum.Parse(typeof(ResourceType), (string)data[i]["ResourceType"]);

                purchasable.Add(new Purchasable(
                    i,
                    (string)data[i]["Name"],
                    float.Parse(data[i]["BaseCost"].ToString()),
                    ItemImages[imageId],
                    float.Parse(data[i]["Coefficent"].ToString()),
                    (int)data[i]["Count"],
                    float.Parse(data[i]["ResourceRate"].ToString()),
                    ResourceType,
                    float.Parse(data[i]["TimeToCompleteTask"].ToString())));

                GameObject purchasablePrefab = Instantiate(Purchasable_Prefab);
                purchasablePrefab.transform.SetParent(Parent);
                purchasablePrefab.transform.localScale = new Vector3(1, 1, 1);
                purchasablePrefab.transform.position = Parent.position;
                GameObject parent_Slot = purchasablePrefab.transform.Find("Slot").gameObject;

                Purchasable purchasable_data = purchasablePrefab.GetComponent<PurchasableData>().Purchasable;
                purchasable_data.ID = purchasable[i].ID;
                purchasable_data.ItemName = purchasable[i].ItemName;
                purchasable_data.BaseCost = purchasable[i].BaseCost;
                purchasable_data.Image = purchasable[i].Image;
                purchasable_data.Cost = purchasable[i].Cost;
                purchasable_data.Coefficent = purchasable[i].Coefficent;
                purchasable_data.Count = purchasable[i].Count;
                purchasable_data.ResourceRate = purchasable[i].ResourceRate;
                purchasable_data.ResourceType = purchasable[i].ResourceType;
                purchasable_data.TimeToCompleteTask = purchasable[i].TimeToCompleteTask;
                //Inventory.Instance.AddSlot(parent_Slot, item_Type);

                item.Add(purchasablePrefab);
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }

    private void MineralSellJsonDataDatabase()
    {
        for (int i = 0; i < mineralSellJsonData.Count; i++)
        {
            ResourceType ResourceType = (ResourceType)Enum.Parse(typeof(ResourceType), (string)mineralSellJsonData[i]["ResourceType"]);

            Mineral_Sell_Value.Add(new MineralSellValue(
                ResourceType,
                (int)(mineralSellJsonData[i]["Value"])));
        }
    }
    private void ItemSellJsonDataDatabase()
    {
        for (int i = 0; i < itemSellJsonData.Count; i++)
        {
            ResourceType ResourceType = (ResourceType)Enum.Parse(typeof(ResourceType), (string)itemSellJsonData[i]["ResourceType"]);
            ItemRarity ItemRarity = (ItemRarity)Enum.Parse(typeof(ItemRarity), (string)itemSellJsonData[i]["ItemRarity"]);

            Item_Sell_Value.Add(new ItemSellValue(
                ResourceType,
                ItemRarity,
                (int)(itemSellJsonData[i]["Value"])));
        }
    }
    private void RecipeCostJsonDataDatabase()
    {
        for (int i = 0; i < recipeCostJsonData.Count; i++)
        {
            ResourceType ResourceType = (ResourceType)Enum.Parse(typeof(ResourceType), (string)recipeCostJsonData[i]["ResourceType"]);

            Recipe_Cost.Add(new RecipeCost(
                ResourceType,
                float.Parse(recipeCostJsonData[i]["InitialCost"].ToString()),
                float.Parse(recipeCostJsonData[i]["Coefficient"].ToString()),
                float.Parse(recipeCostJsonData[i]["InitialTime"].ToString())));
        }
    }

    //private void CraftingJsonDataDatabase()
    //{
    //    for (int i = 0; i < craftingJsonData.Count; i++)
    //    {
    //        string name = (string)craftingJsonData[i]["Name"];
    //        string key = (string)craftingJsonData[i]["Key"]; var s = Split(key, 3).ToList();
    //        int countToComsume = (int)craftingJsonData[i]["CountToConsume"];
    //        ResourceType ResourceType = (ResourceType)Enum.Parse(typeof(ResourceType), (string)craftingJsonData[i]["ResourceType"]);
    //        int sellValue = (int)craftingJsonData[i]["SellValue"];
            
    //        Repice Repice = new Repice(
    //            name,
    //            countToComsume,
    //            ResourceType,
    //            sellValue,
    //            s[0], s[1], s[2], s[3], s[4], s[5], s[6], s[7], s[8]);

    //        RepiceDatabase.Add(Repice);
    //    }

    //    #region old code
    //    //for (int i = 0; i < craftingJsonData.Count; i++)
    //    //{
    //    //    BonusType BonusType = (BonusType)Enum.Parse(typeof(BonusType), (string)craftingJsonData[i]["BonusType"]);
    //    //    ResourceType ResourceType = (ResourceType)Enum.Parse(typeof(ResourceType), (string)craftingJsonData[i]["ResourceType"]);

    //    //    Bonus.Add(new Bonus(
    //    //        BonusType,
    //    //        ResourceType,
    //    //        float.Parse(craftingJsonData[i]["MinValue"].ToString()),
    //    //        float.Parse(craftingJsonData[i]["MaxValue"].ToString())));
    //    //}

    //    //if (CraftingItem.Count == 0)
    //    //{
    //    //    var basic = new Tuple<ItemType, ResourceType>(ItemType.General, ResourceType.Onions);

    //    //    CraftingManager.Instance.AddPrefab(basic);
    //    //}
    //    #endregion
    //}

    static IEnumerable<string> Split(string str, int chunkSize)
    {
        return Enumerable.Range(0, str.Length / chunkSize)
            .Select(i => str.Substring(i * chunkSize, chunkSize));
    }

    private void ItemJsonDataDatabase()
    {
        for (int i = 0; i < ItemJsonData.Count; i++)
        {
            string Name = (string)ItemJsonData[i]["Name"];
            int ID = int.Parse((string)ItemJsonData[i]["ID"]);
            int imageId = 0; //Function.FindImageID(Images, (string)ItemJsonData[i]["SpriteName"]);
            bool Satackable = (bool)ItemJsonData[i]["Stackable"];
            ItemRarity ItemRarity = (ItemRarity)Enum.Parse(typeof(ItemRarity), (string)ItemJsonData[i]["ItemRarity"]);
            ItemType ItemType = (ItemType)Enum.Parse(typeof(ItemType), (string)ItemJsonData[i]["ItemType"]);
            ResourceType ResourceType = (ResourceType)Enum.Parse(typeof(ResourceType), (string)ItemJsonData[i]["ResourceType"]);

            Item_Database.Add(new Item(
                (string)ItemJsonData[i]["Name"],
                ID,
                ItemImages[imageId],
                imageId,
                Satackable,
                ItemRarity,
                ItemType,
                ResourceType));
        }
    }
    //private void ScavangerJsonDataDatabase()
    //{
    //    for (int i = 0; i < ScavangerJsonData.Count; i++)
    //    {
    //        int imageId = 0;
    //        //for (int x = 0; x < Images.Count; x++)
    //        //{
    //        //    if (Images[x].name == (string)ScavangerJsonData[i]["SpriteName"])
    //        //    {
    //        //        imageId = x;
    //        //        break;
    //        //    }
    //        //    else
    //        //    {
    //        //        Debug.Log("No image found, using defualt");
    //        //        imageId = 0;
    //        //        break;
    //        //    }
    //        //}

    //        ResourceType ResourceType = (ResourceType)Enum.Parse(typeof(ResourceType), (string)ScavangerJsonData[i]["ResourceType"]);

    //        Scavanger.Add(new Scavanger(
    //            i,
    //            (string)ScavangerJsonData[i]["Name"],
    //            float.Parse(ScavangerJsonData[i]["BaseCost"].ToString()),
    //            ItemImages[imageId],
    //            float.Parse(ScavangerJsonData[i]["Coefficent"].ToString()),
    //            (int)ScavangerJsonData[i]["Count"],
    //            ResourceType,
    //            float.Parse(ScavangerJsonData[i]["CurrencyReward"].ToString()),
    //            float.Parse(ScavangerJsonData[i]["TimeToCompleteTask"].ToString())));

    //        GameObject purchasablePrefab = Instantiate(ScavangerPrefab);
    //        purchasablePrefab.transform.SetParent(ScavangerParent);
    //        purchasablePrefab.transform.localScale = new Vector3(1, 1, 1);
    //        purchasablePrefab.transform.position = ScavangerParent.position;
    //        GameObject parentSlotScavanger = purchasablePrefab.transform.Find("Slot").gameObject;

    //        Scavanger purchasable = purchasablePrefab.GetComponent<ScavangerData>().Purchasable;
    //        purchasable.ID = Scavanger[i].ID;
    //        purchasable.ItemName = Scavanger[i].ItemName;
    //        purchasable.BaseCost = Scavanger[i].BaseCost;
    //        purchasable.Image = Scavanger[i].Image;
    //        purchasable.Cost = Scavanger[i].Cost;
    //        purchasable.Coefficent = Scavanger[i].Coefficent;
    //        purchasable.Count = Scavanger[i].Count;
    //        purchasable.ResourceType = Scavanger[i].ResourceType;
    //        purchasable.CurrencyReward = Scavanger[i].CurrencyReward;
    //        purchasable.TimeToCompleteTask = Scavanger[i].TimeToCompleteTask;
    //        Inventory.Instance.AddSlot(parentSlotScavanger, ItemType.ScavangerItem);

    //        Scavanger_Item.Add(purchasablePrefab);
    //    }
    //}
    //private void DrillUnlockJsonDataDatabase()
    //{
    //    for (int i = 0; i < drillUnlockjsonData.Count; i++)
    //    {
    //        int imageId = -1;
    //        for (int x = 0; x < Images.Count; x++)
    //        {
    //            if (Images[x].name == (string)drillUnlockjsonData[i]["SpriteName"])
    //            {
    //                imageId = x;
    //                break;
    //            }
    //        }
    //        //could not find image

    //        if (imageId == -1)
    //        {
    //            Debug.Log("No image found");
    //            break;
    //        }

    //        unlocks.Add(UpgradeManager.Instance.AddUpgrade(
    //            (string)drillUnlockjsonData[i]["Name"],
    //            (string)drillUnlockjsonData[i]["Decription"],
    //            Images[imageId]));
    //    }
    //}

    //private void PowerNodeItemJsonDataDatabase()
    //{
    //    for (int i = 0; i < PowerNodeItemJsonData.Count; i++)
    //    {
    //        int imageId = -1;
    //        for (int x = 0; x < Images.Count; x++)
    //        {
    //            if (Images[x].name == (string)PowerNodeItemJsonData[i]["SpriteName"])
    //            {
    //                imageId = x;
    //                break;
    //            }
    //        }
    //        //could not find image
    //        if (imageId == -1)
    //        {
    //            Debug.Log("No image found");
    //            break;
    //        }

    //        ItemRarity ItemRarity = (ItemRarity)Enum.Parse(typeof(ItemRarity), (string)PowerNodeItemJsonData[i]["ItemRarity"]);
    //        ItemType ItemType = (ItemType)Enum.Parse(typeof(ItemType), (string)PowerNodeItemJsonData[i]["ItemType"]);
    //        BonusType BonusType = (BonusType)Enum.Parse(typeof(BonusType), (string)PowerNodeItemJsonData[i]["BonusType"]);

    //        PowerNodeItemDatabase.Add(new Item(
    //            (string)PowerNodeItemJsonData[i]["Name"],
    //            (int)PowerNodeItemJsonData[i]["ID"],
    //            Images[imageId],
    //            imageId,
    //            (bool)PowerNodeItemJsonData[i]["Stackable"],
    //            ItemRarity,
    //            ItemType,
    //            BonusType,
    //            float.Parse(PowerNodeItemJsonData[i]["BonusAmount"].ToString()),
    //            ResourceType.Currency,
    //            float.Parse(PowerNodeItemJsonData[i]["CostToPurchase"].ToString())));
    //    }
    //}

    public void Start()
    {
        Json();
        //Load();

        //currentDateTime = DateTime.Now;

        //if (currentDateTime > previousDateTime)
        //{
        //    previousDateTime = currentDateTime;
        //}
    }

    public void OnApplicationQuit()
    {
        //currentDateTime = DateTime.Now;
        //previousDateTime = currentDateTime;
        //SaveFile();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            //CraftingManager.Instance.Add(ItemType.PowerNode);
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
        }
    }

    //public void SaveFile()
    //{
    //    BinaryFormatter format = new BinaryFormatter();
    //    FileStream file = File.Create(SaveLocation);

    //    SaveState save = new SaveState();

    //    save.DateTime = Calendar.Instance.Date;

    //    //save.currentDateTime = currentDateTime;
    //    //save.previousDateTime = previousDateTime;

    //    save.AFKCurrency = ScavangerManager.Instance.GetRewardRate();
    //    save.AFKResource = DrillManager.Instance.GetRewardRate();

    //    save.TotalGold = Game.Instance.TotalGold;
    //    //save.TotalGoldPerClick = Game.Instance.TotalGoldPerClick;

    //    //save.PlayerSaveState = new PlayerSaveState();
    //    //save.PlayerSaveState.TotalXp = PlayerManager.Instance.TotalXp;
    //    //save.PlayerSaveState.CurrentLevel = PlayerManager.Instance.CurrentLevel;
    //    //save.PlayerSaveState.XpTillNextLevel = PlayerManager.Instance.XpTillNextLevel;
    //    //save.PlayerSaveState.CurrentExperience = PlayerManager.Instance.CurrentExperience;

    //    //save.PlayerSaveState.RemainingPoints = Prestige.Instance.RemainingPoints;
    //    //save.PlayerSaveState.TotalPoints = Prestige.Instance.TotalPoints;
    //    //save.PlayerSaveState.LifetimCurrency = Prestige.Instance.LifetimCurrency;

    //    save.ScavangerSaveState = new List<ScavangerSaveState>();
    //    save.DrillSaveState = new List<DrillSaveState>();

    //    save.ResourceSaveState = new List<ResourceSaveState>();
    //    save.ItemSaveState = new List<ItemSaveState>();

    //    save.CraftingSaveState = new List<CraftingSaveState>();

    //    for (int x = 0; x < Inventory.Instance.slots.Count; x++)
    //    {
    //        GameObject slot = Inventory.Instance.slots[x].gameObject;

    //        if (slot.transform.childCount > 0)
    //        {
    //            ItemData item = slot.transform.GetChild(0).GetComponent<ItemData>();

    //            if (item.Item.ItemType == ItemType.Ore)
    //            {
    //                ResourceSaveState resourceSave = new ResourceSaveState();

    //                resourceSave.ID = item.Item.ID;
    //                resourceSave.Count = item.count;
    //                resourceSave.SlotID = x;

    //                save.ResourceSaveState.Add(resourceSave);
    //            }
    //            if (item.Item.ItemType == ItemType.DrillItem || item.Item.ItemType == ItemType.ScavangerItem)
    //            {
    //                ItemSaveState itemSave = new ItemSaveState();

    //                itemSave.ID = item.Item.ID;
    //                itemSave.Count = item.count;
    //                itemSave.SlotID = x;

    //                itemSave.Name = item.Item.Name;
    //                itemSave.ID = item.Item.ID;
    //                itemSave.ImageID = item.Item.ImageID;
    //                itemSave.Stackable = item.Item.Stackable;
    //                itemSave.ItemRarity = item.Item.ItemRarity;
    //                itemSave.ItemType = item.Item.ItemType;

    //                itemSave.BonusSaveState = new List<BonusSaveState>();

    //                for (int i = 0; i < item.Item.BonusAttached.Count; i++)
    //                {
    //                    BonusType bType = item.Item.BonusAttached[i].Item1;
    //                    float bAmount = item.Item.BonusAttached[i].Item2;

    //                    BonusSaveState BonusSaveState = new BonusSaveState();
    //                    BonusSaveState.BonusType = bType;
    //                    BonusSaveState.Amount = bAmount;

    //                    itemSave.BonusSaveState.Add(BonusSaveState);
    //                }

    //                save.ItemSaveState.Add(itemSave);
    //            }
    //            else { }
    //        }
    //    }

    //    for (int i = 0; i < ScavangerItem.Count(); i++)
    //    {
    //        Scavanger item = ScavangerItem[i].GetComponent<ScavangerData>().Purchasable;
    //        ScavangerSaveState scavanger = new ScavangerSaveState();
    //        scavanger.Cost = item.Cost;
    //        scavanger.Count = item.Count;
    //        scavanger.CurrencyReward = item.CurrencyReward;
    //        scavanger.IsPurchased = item.IsPurchased;
    //        scavanger.OnComplete = item.OnComplete;
    //        scavanger.Unlocked = item.Unlocked;

    //        save.ScavangerSaveState.Add(scavanger);
    //    }

    //    for (int i = 0; i < DrillItem.Count(); i++)
    //    {
    //        Drill item = DrillItem[i].GetComponent<DrillData>().Purchasable;

    //        DrillSaveState drill = new DrillSaveState();
    //        drill.Cost = item.Cost;
    //        drill.Count = item.Count;
    //        drill.ResourceRate = item.ResourceRate;
    //        drill.IsPurchased = item.IsPurchased;
    //        drill.OnComplete = item.OnComplete;
    //        drill.Unlocked = item.Unlocked;

    //        save.DrillSaveState.Add(drill);
    //    }

    //    for (int i = 0; i < CraftingItem.Count; i++)
    //    {
    //        CraftingData craftData = CraftingItem[i].GetComponent<CraftingData>();
    //        Crafting craft = craftData.Crafting;

    //        CraftingSaveState craftSaveState = new CraftingSaveState();
    //        craftSaveState.ID = craft.ID;
    //        craftSaveState.CurrentTime = craftData.CurrentTime;
    //        craftSaveState.StartedTimer = craftData.StartedTimer;

    //        save.CraftingSaveState.Add(craftSaveState);
    //    }

    //    format.Serialize(file, save);
    //    file.Close();
    //}

    //public void Load()
    //{
    //    if (File.Exists(SaveLocation))
    //    {
    //        BinaryFormatter format = new BinaryFormatter();
    //        FileStream file = File.Open(SaveLocation, FileMode.Open);

    //        SaveState load = (SaveState)format.Deserialize(file);
    //        file.Close();

    //        Calendar.Instance.Date = load.DateTime;

    //        currentDateTime = load.currentDateTime;
    //        previousDateTime = load.previousDateTime;

    //        float currecy = load.AFKCurrency;
    //        float resource = load.AFKResource;

    //        TimeSpan Difference = DateTime.Now - previousDateTime;

    //        float afkC = Difference.Seconds * currecy;
    //        float afkR = Difference.Seconds * resource;

    //        Game.Instance.AddGold(afkC);

    //        Game.Instance.TotalGold = load.TotalGold;
    //        //Game.Instance.TotalGoldPerClick = load.TotalGoldPerClick;

    //        PlayerManager.Instance.TotalXp = load.PlayerSaveState.TotalXp;
    //        PlayerManager.Instance.CurrentLevel = load.PlayerSaveState.CurrentLevel;
    //        PlayerManager.Instance.XpTillNextLevel = load.PlayerSaveState.XpTillNextLevel;
    //        PlayerManager.Instance.CurrentExperience = load.PlayerSaveState.CurrentExperience;

    //        Prestige.Instance.RemainingPoints = load.PlayerSaveState.RemainingPoints;
    //        Prestige.Instance.TotalPoints = load.PlayerSaveState.TotalPoints;
    //        Prestige.Instance.LifetimCurrency = load.PlayerSaveState.LifetimCurrency;

    //        try
    //        {
    //            for (int i = 0; i < load.ResourceSaveState.Count; i++)
    //            {
    //                ResourceSaveState itemSave = load.ResourceSaveState[i];

    //                Inventory.Instance.AddItemToSlot(itemSave.SlotID, itemSave.ID, itemSave.Count + afkR);

    //                Debug.Log("added item to slot: " + itemSave.SlotID + " (" + itemSave.Count + afkR + ")");
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            Debug.Log(ex.Message);
    //        }

    //        for (int i = 0; i < load.ItemSaveState.Count; i++)
    //        {
    //            ItemSaveState itemSave = load.ItemSaveState[i];

    //            if (itemSave.ItemType == ItemType.DrillItem || itemSave.ItemType == ItemType.ScavangerItem)
    //            {
    //                //create item from save
    //                Item item = new Item(
    //                itemSave.Name,
    //                itemSave.ID,
    //                Images[itemSave.ImageID],
    //                itemSave.ImageID,
    //                false,
    //                itemSave.ItemRarity,
    //                itemSave.ItemType,
    //                BonusType.Empty,
    //                0,
    //                itemSave.ResourceType,
    //                itemSave.CostToPurchase);

    //                for (int x = 0; x < load.ItemSaveState[i].BonusSaveState.Count; x++)
    //                {
    //                    BonusType bType = load.ItemSaveState[i].BonusSaveState[x].BonusType;
    //                    float bAmount = load.ItemSaveState[i].BonusSaveState[x].Amount;

    //                    item.AddBonus(bType, bAmount);
    //                }

    //                //add to database
    //                SaveLoad.Instance.ItemDatabase.Add(item);
    //                Inventory.Instance.Items.Add(item);
    //                Inventory.Instance.AddItemToSlot(itemSave.SlotID, itemSave.ID, itemSave.Count + afkR);
    //            }
    //        }

    //        for (int i = 0; i < ScavangerItem.Count(); i++)
    //        {
    //            Scavanger item = ScavangerItem[i].GetComponent<ScavangerData>().Purchasable;

    //            ScavangerSaveState scavanger = load.ScavangerSaveState[i];
    //            item.Cost = scavanger.Cost;
    //            item.Count = scavanger.Count;
    //            item.CurrencyReward = scavanger.CurrencyReward;
    //            item.IsPurchased = scavanger.IsPurchased;
    //            item.Unlocked = scavanger.Unlocked;
    //            item.CurrentTime = scavanger.CurrentTime;
    //            item.StartedTimer = scavanger.StartedTimer;
    //        }

    //        for (int i = 0; i < DrillItem.Count(); i++)
    //        {
    //            Drill item = DrillItem[i].GetComponent<DrillData>().Purchasable;

    //            DrillSaveState drill = load.DrillSaveState[i];
    //            item.Cost = drill.Cost;
    //            item.Count = drill.Count;
    //            item.ResourceRate = drill.ResourceRate;
    //            item.IsPurchased = drill.IsPurchased;
    //            item.Unlocked = drill.Unlocked;
    //            item.CurrentTime = drill.CurrentTime;
    //            item.StartedTimer = drill.StartedTimer;
    //        }
    //        for (int i = 0; i < load.CraftingSaveState.Count; i++)
    //        {
    //            CraftingManager.Instance.Add();

    //            CraftingData craftData = CraftingItem[i].GetComponent<CraftingData>();
    //            Crafting craft = craftData.Crafting;

    //            CraftingSaveState craftSaveState = load.CraftingSaveState[i];
    //            craft.ID = craftSaveState.ID;
    //            craftData.CurrentTime = craftSaveState.CurrentTime;
    //            craftData.StartedTimer = craftSaveState.StartedTimer;

    //            craftData.Start();
    //        }
    //    }
    //    else
    //    {
    //        if (CraftingItem.Count == 0)
    //        {
    //            CraftingManager.Instance.Add();
    //        }

    //        SaveFile();
    //    }
    //}
}

[Serializable]
class SaveState
{
    [SerializeField]
    public DateTime DateTime;

    [SerializeField]
    public DateTime previousDateTime;
    [SerializeField]
    public DateTime currentDateTime;

    [SerializeField]
    public float AFKCurrency;
    [SerializeField]
    public float AFKResource;

    [SerializeField]
    public float TotalGold;
    [SerializeField]
    public float TotalGoldPerClick;

    [SerializeField]
    public PlayerSaveState PlayerSaveState;

    [SerializeField]
    public List<ScavangerSaveState> ScavangerSaveState;

    [SerializeField]
    public List<DrillSaveState> DrillSaveState;

    [SerializeField]
    public List<ItemSaveState> ItemSaveState;

    [SerializeField]
    public List<ResourceSaveState> ResourceSaveState;

    [SerializeField]
    public List<CraftingSaveState> CraftingSaveState;
}

[Serializable]
class PlayerSaveState
{
    [SerializeField]
    public float TotalXp;
    [SerializeField]
    public int CurrentLevel;
    [SerializeField]
    public int XpTillNextLevel;
    [SerializeField]
    public float CurrentExperience;

    [SerializeField]
    public float RemainingPoints;
    [SerializeField]
    public float TotalPoints;
    [SerializeField]
    public float LifetimCurrency;
}

[Serializable]
class ScavangerSaveState
{
    [SerializeField]
    public float Cost;
    [SerializeField]
    public int Count;
    [SerializeField]
    public float CurrencyReward;
    [SerializeField]
    public bool IsPurchased;
    [SerializeField]
    public bool OnComplete;
    [SerializeField]
    public bool Unlocked;
    public float CurrentTime;
    [SerializeField]
    public bool StartedTimer;
}

[Serializable]
class DrillSaveState
{
    [SerializeField]
    public float Cost;
    [SerializeField]
    public int Count;
    [SerializeField]
    public float ResourceRate;
    [SerializeField]
    public bool IsPurchased;
    [SerializeField]
    public bool OnComplete;
    [SerializeField]
    public bool Unlocked;
    public float CurrentTime;
    [SerializeField]
    public bool StartedTimer;
}

[Serializable]
class ResourceSaveState
{
    [SerializeField]
    public int ID;
    [SerializeField]
    public float Count;
    [SerializeField]
    public int SlotID;
}

    [Serializable]
class ItemSaveState
{
    [SerializeField]
    public int ID;
    [SerializeField]
    public float Count;
    [SerializeField]
    public int SlotID;

    [SerializeField]
    public string Name;
    [SerializeField]
    public int ImageID;
    [SerializeField]
    public bool Stackable;
    [SerializeField]
    public ItemRarity ItemRarity;
    [SerializeField]
    public ItemType ItemType;
    [SerializeField]
    public ResourceType ResourceType;
    [SerializeField]
    public float CostToPurchase;
    [SerializeField]
    public List<BonusSaveState> BonusSaveState;
    //[SerializeField]
    //public float B_DecreseCost;
    //[SerializeField]
    //public float B_IncreaseResourceRate;
    //[SerializeField]
    //public float B_IncreaseCurrencyRewardRate;
    //[SerializeField]
    //public float B_DecreaseTimeToCompleteTask;
    //[SerializeField]
    //public float B_IncreaseCritChance;
    //[SerializeField]
    //public float B_MineNextTeir;
}

[Serializable]
class BonusSaveState
{
    [SerializeField]
    public BonusType BonusType;
    [SerializeField]
    public float Amount;
}

[Serializable]
class CraftingSaveState
{
    [SerializeField]
    public int ID;
    [SerializeField]
    public float CurrentTime;
    [SerializeField]
    public bool StartedTimer;
}

