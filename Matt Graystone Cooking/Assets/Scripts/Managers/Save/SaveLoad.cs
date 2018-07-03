using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using LitJson;

/// <summary>
/// 
/// </summary>
public class SaveLoad : MonoBehaviour
{
    /// <summary>
    /// Instantiate class object
    /// </summary>
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
    private static SaveLoad instance;

    /// <summary>
    /// 
    /// </summary>
    private string File_Path;

    /// <summary>
    /// 
    /// </summary>
    private string File_Name;

    /// <summary>
    /// 
    /// </summary>
    private string Save_Location
    {
        //C:\Users\matthew\AppData\LocalLow\DefaultCompany\Matt Graystone Cooking
        get { return File_Path + File_Name; } //Application.persistentDataPath
    }

    /// <summary>
    /// Main GUI Title Screen
    /// </summary>
    public GameObject GUI_Opening_Menu;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    public void Start()
    {
        File_Name = "savedGames.idle";
        File_Path = Application.dataPath + "/StreamingAssets/Save/";

        LoadAllImages();

        Json();
        //LoadFile();

        Current_Date_Time = DateTime.Now;

        if (Current_Date_Time > Previous_Date_Time)
        {
            Previous_Date_Time = Current_Date_Time;
        }

        ScreenManager.Instance.OpenScreen();
    }

    /// <summary>
    /// 
    /// </summary>
    private DateTime Previous_Date_Time;

    /// <summary>
    /// 
    /// </summary>
    private DateTime Current_Date_Time;

    /// <summary>
    /// 
    /// </summary>
    public List<Sprite> Item_Images = new List<Sprite>();

    /// <summary>
    /// 
    /// </summary>
    //public List<Sprite> BorderImages = new List<Sprite>();

    /// <summary>
    /// 
    /// </summary>
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
    public List<RecipeData> Recipe_Data = new List<RecipeData>();
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

    public string Chef_Requirment_Location = "";
    private JsonData Chef_Requirment_Json_Data;
    public List<Chef> Chef_Requirment_Purchasable = new List<Chef>();

    /// <summary>
    /// 
    /// </summary>
    private string FileType = ".json";

    /// <summary>
    /// 
    /// </summary>
    public void Json()
    {
        itemSellJsonData = MapJsonFile(itemSellLocation);
        mineralSellJsonData = MapJsonFile(mineralSellLocation);
        recipeCostJsonData = MapJsonFile(recipeCostLocation);
        ItemJsonData= MapJsonFile(itemLocation);
        Chef_Requirment_Json_Data = MapJsonFile(Chef_Requirment_Location);

        ItemSellJsonDataDatabase();
        MineralSellJsonDataDatabase();
        RecipeCostJsonDataDatabase();
        ItemJsonDataDatabase();
        Chef_RequirmentDatabase();

        //Purchasable Section

        JsonDataDatabase(Recipe_Data, Recipe_Item, Recipe_Location, Recipe_Json_Data, Recipe_Parent, SlotType.General);

        JsonDataDatabase(Pastry_Purchasable, Pastry_Item, Pastry_Location, Pastry_Json_Data, Pastry_Parent, SlotType.Consumable);
        JsonDataDatabase(Larder_Purchasable, Larder_Item, Larder_Location, Larder_Json_Data, Larder_Parent, SlotType.Consumable);
        JsonDataDatabase(Sauce_Purchasable, Sauce_Item, Sauce_Location, Sauce_Json_Data, Sauce_Parent, SlotType.Consumable);
        JsonDataDatabase(Fish_Purchasable, Fish_Item, Fish_Location, Fish_Json_Data, Fish_Parent, SlotType.Consumable);
        JsonDataDatabase(Vegetable_Purchasable, Vegetable_Item, Vegetable_Location, Vegetable_Json_Data, Vegetable_Parent, SlotType.Consumable);
        JsonDataDatabase(Meat_Purchasable, Meat_Item, Meat_Location, Meat_Json_Data, Meat_Parent, SlotType.Consumable);
    }

    /// <summary>
    /// 
    /// </summary>
    private JsonData MapJsonFile(string path)
    {
        return JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/" + path + ".json"));
    }

    /// <summary>
    /// 
    /// </summary>
    private void Chef_RequirmentDatabase()
    {
        for (int i = 0; i < Chef_Requirment_Json_Data.Count; i++)
        {
            string Name = (string)Chef_Requirment_Json_Data[i]["Name"];
            int Required_Level = (int)Chef_Requirment_Json_Data[i]["Required_Level"];
            int Cost_To_Level = (int)Chef_Requirment_Json_Data[i]["Cost_To_Level"];
            int Max_Level = (int)Chef_Requirment_Json_Data[i]["Max_Level"];

            Chef chef = Chef_Requirment_Purchasable[i];

            chef.Name = Name;
            chef.RequiredLevel = Required_Level;
            chef.CostToLevel = Cost_To_Level;
            chef.MaxLevel = Max_Level;
            chef.UpdateGUI();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void JsonDataDatabase(List<RecipeData> purchasable,
                                  List<GameObject> item,
                                  string location,
                                  JsonData data,
                                  Transform Parent,
                                  SlotType slotType)
    {
        try
        {
            string app_Path = Application.dataPath + "/StreamingAssets/" + location + FileType;
            data = JsonMapper.ToObject(File.ReadAllText(app_Path));

            for (int i = 0; i < data.Count; i++)
            {
                string name = (string)data[i]["Name"];
                string key = (string)data[i]["Key"];
                string image_preview = (string)data[i]["Image_Preview"];

                int image_id = Function.FindImageID(Item_Images, image_preview);

                List<RecipeItem> ri = new List<RecipeItem>();

                for (int x = 0; x < data[i]["Recipe"].Count; x++)
                {
                    int id = (int)data[i]["Recipe"][x]["ItemID"];
                    int count = (int)data[i]["Recipe"][x]["Count"];

                    RecipeItem RecipeItem = new RecipeItem(id,count);
                    ri.Add(RecipeItem);
                }

                int sellvalue = (int)data[i]["SellValue"];

                Recipe recipe = new Recipe(
                    name,
                    key,
                    Item_Images[image_id],
                    ri,
                    sellvalue);

                GameObject recipePrefab = CreateRecipePrefab(Parent);

                RecipeData recipe_data = recipePrefab.GetComponent<RecipeData>();
                recipe_data.recipe.Name = recipe.Name;
                recipe_data.recipe.Key = recipe.Key;
                recipe_data.recipe.Items = recipe.Items;
                recipe_data.recipe.SellValue = recipe.SellValue;
                recipe_data.recipe.Preview_Image = recipe.Preview_Image;

                purchasable.Add(recipe_data);
                item.Add(recipePrefab);
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message + location);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void JsonDataDatabase(List<Purchasable> purchasable,
                                  List<GameObject> item,
                                  string location,
                                  JsonData data,
                                  Transform Parent,
                                  SlotType slotType)
    {
        try
        {
            string app_Path = Application.dataPath + "/StreamingAssets/" + location + FileType;
            data = JsonMapper.ToObject(File.ReadAllText(app_Path));

            List<Purchasable> items = new List<Purchasable>();

            for (int i = 0; i < data.Count; i++)
            {
                purchasable.Add(new Purchasable(
                    i,
                    (string)data[i]["Name"],
                    float.Parse(data[i]["BaseCost"].ToString()),
                    Item_Images[Function.FindImageID(Item_Images, (string)data[i]["SpriteName"])],
                    float.Parse(data[i]["Coefficent"].ToString()),
                    (int)data[i]["Count"],
                    float.Parse(data[i]["ResourceRate"].ToString()),
                    (string)data[i]["ItemID"],
                    float.Parse(data[i]["TimeToCompleteTask"].ToString())));

                GameObject purchasablePrefab = Instantiate(Purchasable_Prefab);
                purchasablePrefab.transform.SetParent(Parent);
                purchasablePrefab.transform.localScale = new Vector3(1, 1, 1);
                purchasablePrefab.transform.position = Parent.position;

                Purchasable purchasable_data = purchasablePrefab.GetComponent<PurchasableData>().Purchasable;
                purchasable_data.ID = purchasable[i].ID;
                purchasable_data.Item_Name = purchasable[i].Item_Name;
                purchasable_data.Base_Cost = purchasable[i].Base_Cost;
                purchasable_data.Cost = purchasable[i].Cost;
                purchasable_data.Coefficent = purchasable[i].Coefficent;
                purchasable_data.Count = purchasable[i].Count;
                purchasable_data.Resource_Rate = purchasable[i].Resource_Rate;
                purchasable_data.Item_ID = purchasable[i].Item_ID;
                purchasable_data.Time_To_Complete_Task = purchasable[i].Time_To_Complete_Task;
                purchasable_data.Image = purchasable[i].Image;

                item.Add(purchasablePrefab);
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message + location);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private GameObject CreateRecipePrefab(Transform Parent)
    {
        GameObject recipePrefab = Instantiate(Recipe_Prefab);
        recipePrefab.transform.SetParent(Parent);
        recipePrefab.transform.localScale = new Vector3(1, 1, 1);
        recipePrefab.transform.position = Parent.position;
        recipePrefab.SetActive(false);

        return recipePrefab;
    }

    /// <summary>
    /// 
    /// </summary>
    public Sprite PlaceHolder()
    {
        return Item_Images.FirstOrDefault();
    }

    /// <summary>
    /// 
    /// </summary>
    public void LoadAllImages()
    {
        string path = Application.dataPath + "/FoodAssets/";

        foreach (string file in Directory.GetFiles(path, "*.png"))
        {
            Item_Images.Add(LoadPNG(file));
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public Sprite LoadPNG(string filePath)
    {
        Texture2D tex = null;
        byte[] fileData;

        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(256, 256);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        }

        Rect rect = new Rect(0, 0, tex.width, tex.height);
        Sprite sprite = Sprite.Create(tex, rect, new Vector2(0.5f, 0.5f), 100);
        sprite.name = Path.GetFileNameWithoutExtension(filePath);
        return sprite;
    }

    /// <summary>
    /// 
    /// </summary>
    private void MineralSellJsonDataDatabase()
    {
        if (mineralSellJsonData == null)
            return;

        for (int i = 0; i < mineralSellJsonData.Count; i++)
        {
            ResourceType ResourceType = (ResourceType)Enum.Parse(typeof(ResourceType), (string)mineralSellJsonData[i]["ResourceType"]);

            Mineral_Sell_Value.Add(new MineralSellValue(
                ResourceType,
                (int)(mineralSellJsonData[i]["Value"])));
        }
    }

    /// <summary>
    /// 
    /// </summary>
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

    /// <summary>
    /// 
    /// </summary>
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

    /// <summary>
    /// 
    /// </summary>
    private static IEnumerable<string> Split(string str, 
                                             int chunkSize)
    {
        return Enumerable.Range(0, str.Length / chunkSize)
            .Select(i => str.Substring(i * chunkSize, chunkSize));
    }

    /// <summary>
    /// 
    /// </summary>
    private void ItemJsonDataDatabase()
    {
        for (int i = 0; i < ItemJsonData.Count; i++)
        {
            string Name = (string)ItemJsonData[i]["Name"];
            int ID = int.Parse((string)ItemJsonData[i]["ID"]);
            int imageId = Function.FindImageID(Item_Images, (string)ItemJsonData[i]["SpriteName"]);
            bool Satackable = (bool)ItemJsonData[i]["Stackable"];
            ItemRarity ItemRarity = (ItemRarity)Enum.Parse(typeof(ItemRarity), (string)ItemJsonData[i]["ItemRarity"]);
            SlotType SlotType = (SlotType)Enum.Parse(typeof(SlotType), (string)ItemJsonData[i]["ItemType"]);
            ResourceType ResourceType = (ResourceType)Enum.Parse(typeof(ResourceType), (string)ItemJsonData[i]["ResourceType"]);

            Item_Database.Add(new Item(
                Name,
                ID,
                Item_Images[imageId],
                imageId,
                Satackable,
                ItemRarity,
                SlotType,
                ResourceType));
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnApplicationQuit()
    {
        Current_Date_Time = DateTime.Now;
        Previous_Date_Time = Current_Date_Time;
        SaveFile();
        Application.Quit();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            SaveFile();
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            LoadFile();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void SaveFile()
    {
        BinaryFormatter format = new BinaryFormatter();
        FileStream file = File.Create(Save_Location);

        Save_State save = new Save_State();

        if(FirstTime == true)
            save.FirstTime = 0;
        else
            save.FirstTime = 1;

        //save.Date_Time = Calendar.Instance.Date;

        save.Current_Date_Time = Current_Date_Time;
        save.Previous_Date_Time = Previous_Date_Time;

        save.Total_Gold = Game.Instance.TotalGold;

        save.Player_Save_State = new Player_Save_State();
        save.Player_Save_State.Total_Xp = PlayerManager.Instance.TotalXp;
        save.Player_Save_State.Current_Level = PlayerManager.Instance.CurrentLevel;
        save.Player_Save_State.Xp_Till_Next_Level = PlayerManager.Instance.XpTillNextLevel;
        save.Player_Save_State.Current_Experience = PlayerManager.Instance.CurrentExperience;
        save.Player_Save_State.ResearchPoints = PlayerManager.Instance.ResearchPoints;

        ResearchTreeSaveSate(save);

        save.Larder_Manager_Save_Sate = new List<Food_Section_Save_State>();
        save.Meat_Manager_Save_Sate = new List<Food_Section_Save_State>();
        save.Pastry_Manager_Save_Sate = new List<Food_Section_Save_State>();
        save.Sauce_Manager_Save_Sate = new List<Food_Section_Save_State>();
        save.Vegetable_Manager_Save_Sate = new List<Food_Section_Save_State>();
        save.Fish_Manager_Save_Sate = new List<Food_Section_Save_State>();

        save.Resource_Save_State = new List<Resource_Save_State>();
        save.Item_Save_State = new List<Item_Save_State>();

        for (int x = 0; x < Inventory.Instance.slots.Count; x++)
        {
            GameObject slot = Inventory.Instance.slots[x].gameObject;

            if (slot.transform.childCount > 0)
            {
                ItemData item = slot.transform.GetChild(0).GetComponent<ItemData>();

                if (item.Item.SlotType == SlotType.Consumable)
                {
                    Resource_Save_State resourceSave = new Resource_Save_State();

                    resourceSave.ID = item.Item.ID;
                    resourceSave.Count = item.count;
                    resourceSave.Slot_ID = x;

                    save.Resource_Save_State.Add(resourceSave);
                }
            }
        }

        FishManager.Instance.Research_Points = save.Fish_Manager_Research_Points;
        LarderManager.Instance.Research_Points = save.Larder_Manager_Research_Points;
        MeatManager.Instance.Research_Points = save.Meat_Manager_Research_Points;
        PastryManager.Instance.Research_Points = save.Pastry_Manager_Research_Points;
        SauceManager.Instance.Research_Points = save.Sauce_Manager_Research_Points;
        VegetableManager.Instance.Research_Points = save.Vegetable_Manager_Research_Points;

        float afk_difference = Current_Date_Time.Second - Previous_Date_Time.Second;

        ItemSaveState(save.Fish_Manager_Save_Sate, Fish_Item, afk_difference);
        ItemSaveState(save.Larder_Manager_Save_Sate, Larder_Item, afk_difference);
        ItemSaveState(save.Meat_Manager_Save_Sate, Meat_Item, afk_difference);
        ItemSaveState(save.Pastry_Manager_Save_Sate, Pastry_Item, afk_difference);
        ItemSaveState(save.Sauce_Manager_Save_Sate, Sauce_Item, afk_difference);
        ItemSaveState(save.Vegetable_Manager_Save_Sate, Vegetable_Item, afk_difference);

        RecipeSaveState(save.Recipe_Save_State, Recipe_Item);

        format.Serialize(file, save);
        file.Close();

        Debug.Log("saved");
    }

    /// <summary>
    /// 
    /// </summary>
    private void ResearchTreeSaveSate(Save_State save)
    {
        for (int  i = 0; i < Chef_Requirment_Purchasable.Count; i++)
        {
            Chef chef = Chef_Requirment_Purchasable[i];
            Chef_Research_Save_State save_state = new Chef_Research_Save_State();

            save_state.Name = chef.Name;
            save_state.Unlocked = chef.Unlocked;
            save_state.Current_Level = chef.CurrentLevel;
            save.Chef_Research_Save_State.Add(save_state);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void ResearchTreeLoadSate(Save_State load)
    {
        for (int i = 0; i < Chef_Requirment_Purchasable.Count; i++)
        {
            Chef chef = Chef_Requirment_Purchasable[i];
            Chef_Research_Save_State loadState = load.Chef_Research_Save_State[i];

            chef.Name = loadState.Name;
            chef.Unlocked = loadState.Unlocked;
            chef.CurrentLevel = loadState.Current_Level;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void RecipeSaveState(List<Recipe_Save_State> save, 
                                 List<GameObject> purchasable)
    {
        for (int i = 0; i < purchasable.Count(); i++)
        {
            RecipeData item = purchasable[i].GetComponent<RecipeData>();
            Recipe_Save_State save_sate = new Recipe_Save_State();
            save_sate.Unlocked = item.recipe.Unlocked;
            save.Add(save_sate);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void ItemSaveState(List<Food_Section_Save_State> save, 
                               List<GameObject> purchasable, 
                               float afk_rate)
    {
        for (int i = 0; i < purchasable.Count(); i++)
        {
            Purchasable item = purchasable[i].GetComponent<PurchasableData>().Purchasable;
            Food_Section_Save_State save_sate = new Food_Section_Save_State();
            save_sate.Cost = item.Cost;
            save_sate.Count = item.Count;
            save_sate.Resource_Rate = item.Resource_Rate;
            save_sate.Is_Purchased = item.Is_Purchased;
            save_sate.On_Complete = item.On_Complete;
            save_sate.Unlocked = item.Unlocked;
            save_sate.Current_Time = item.Current_Time;
            save_sate.Started_Timer = item.Started_Timer;
            save.Add(save_sate);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void LoadFile()
    {
        if (File.Exists(Save_Location))
        {
            BinaryFormatter format = new BinaryFormatter();
            FileStream file = File.Open(Save_Location, FileMode.Open);

            Save_State load = (Save_State)format.Deserialize(file);
            file.Close();

            if (load.FirstTime == 0)
                FirstTime = true;
            else
                FirstTime = false;

            //Calendar.Instance.Date = load.Date_Time;

            Current_Date_Time = load.Current_Date_Time;
            Previous_Date_Time = load.Previous_Date_Time;

            int resource = load.AFK_Resource;

            TimeSpan Difference = DateTime.Now - Previous_Date_Time;

            Game.Instance.AddGold(load.Total_Gold);

            PlayerManager.Instance.TotalXp = load.Player_Save_State.Total_Xp;
            PlayerManager.Instance.CurrentLevel = load.Player_Save_State.Current_Level;
            PlayerManager.Instance.XpTillNextLevel = load.Player_Save_State.Xp_Till_Next_Level;
            PlayerManager.Instance.CurrentExperience = load.Player_Save_State.Current_Experience;
            PlayerManager.Instance.ResearchPoints = load.Player_Save_State.ResearchPoints;

            ResearchTreeLoadSate(load);

            for (int i = 0; i < load.Resource_Save_State.Count; i++)
            {
                Resource_Save_State itemSave = load.Resource_Save_State[i];
                Inventory.Instance.AddItemToSlot(itemSave.Slot_ID, itemSave.ID, itemSave.Count);

                //Debug.Log("added item to slot: " + itemSave.Slot_ID + " (" + itemSave.Count + ")");
            }

            for (int i = 0; i < load.Item_Save_State.Count; i++)
            {
                Item_Save_State item_Save = load.Item_Save_State[i];

                if (item_Save.Slot_Type == SlotType.Consumable)
                {
                    //create item from save
                    Item item = new Item(
                    item_Save.Name,
                    item_Save.ID,
                    item_Save.Image_ID,
                    item_Save.Stackable,
                    item_Save.Item_Rarity,
                    item_Save.Slot_Type,
                    item_Save.Resource_Type);

                    //for (int x = 0; x < load.Item_Save_State[i].Bonus_Save_State.Count; x++)
                    //{
                    //    BonusType bType = load.Item_Save_State[i].Bonus_Save_State[x].Bonus_Type;
                    //    float bAmount = load.Item_Save_State[i].Bonus_Save_State[x].Amount;

                    //    item.AddBonus(bType, bAmount);
                    //}

                    //add to database
                    Item_Database.Add(item);
                    Inventory.Instance.Items.Add(item);
                    Inventory.Instance.AddItemToSlot(item_Save.Slot_ID, item_Save.ID, item_Save.Count);
                }
            }

            load.Fish_Manager_Research_Points = FishManager.Instance.Research_Points;
            load.Larder_Manager_Research_Points = LarderManager.Instance.Research_Points;
            load.Meat_Manager_Research_Points = MeatManager.Instance.Research_Points;
            load.Pastry_Manager_Research_Points = PastryManager.Instance.Research_Points;
            load.Sauce_Manager_Research_Points = SauceManager.Instance.Research_Points;
            load.Vegetable_Manager_Research_Points = VegetableManager.Instance.Research_Points;

            ItemLoadState(load.Fish_Manager_Save_Sate, Fish_Item);
            ItemLoadState(load.Larder_Manager_Save_Sate, Larder_Item);
            ItemLoadState(load.Meat_Manager_Save_Sate, Meat_Item);
            ItemLoadState(load.Pastry_Manager_Save_Sate, Pastry_Item);
            ItemLoadState(load.Sauce_Manager_Save_Sate, Sauce_Item);
            ItemLoadState(load.Vegetable_Manager_Save_Sate, Vegetable_Item);

            RecipeLoadState(load.Recipe_Save_State, Recipe_Item);

            //MessageBox.Instance.SetText("Welcome Back!", "You earned $" + afkR + " while you where away.");
        }
        else
        {
            SaveFile();
        }

        //Debug.Log("loaded");
    }

    /// <summary>
    /// 
    /// </summary>
    private void ItemLoadState(List<Food_Section_Save_State> load, 
                               List<GameObject> purchasable)
    {
        for (int i = 0; i < purchasable.Count(); i++)
        {
            Purchasable item = purchasable[i].GetComponent<PurchasableData>().Purchasable;

            Food_Section_Save_State load_sate = load[i];
            item.Cost = load_sate.Cost;
            item.Count = load_sate.Count;
            item.Resource_Rate = load_sate.Resource_Rate;
            item.Is_Purchased = load_sate.Is_Purchased;
            item.On_Complete = load_sate.On_Complete;
            item.Unlocked = load_sate.Unlocked;
            item.Current_Time = load_sate.Current_Time;
            item.Started_Timer = load_sate.Started_Timer;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void RecipeLoadState(List<Recipe_Save_State> load, 
                                 List<GameObject> purchasable)
    {
        for (int i = 0; i < purchasable.Count(); i++)
        {
            RecipeData item = purchasable[i].GetComponent<RecipeData>();

            Recipe_Save_State load_sate = load[i];
            item.recipe.Unlocked = load_sate.Unlocked;
            item.Unlock();
        }
    }


    public GameObject GUI_Chef_Selection;

    private bool FirstTime = true;

    /// <summary>
    /// 
    /// </summary>
    public void CheckFirstTime()
    {
        //check if we have a new user
        if (FirstTime == true)
        {
            //show new profession screen
            GUI_Chef_Selection.SetActive(true);
            GUI_Chef_Selection.GetComponent<Gui_Anim>().Fade_In();

            FirstTime = false;
        }
    }
}

/// <summary>
/// 
/// </summary>
[Serializable]
public class Save_State
{
    /// <summary>
    /// 0 = ture, 1 = false
    /// </summary>
    [SerializeField]
    public int FirstTime;

    [SerializeField]
    public DateTime Date_Time;

    [SerializeField]
    public DateTime Previous_Date_Time;
    [SerializeField]
    public DateTime Current_Date_Time;

    [SerializeField]
    public int AFK_Resource;

    [SerializeField]
    public float Total_Gold;

    [SerializeField]
    public Player_Save_State Player_Save_State;

    [SerializeField]
    public List<Food_Section_Save_State> Fish_Manager_Save_Sate;

    [SerializeField]
    public int Fish_Manager_Research_Points;

    [SerializeField]
    public List<Food_Section_Save_State> Larder_Manager_Save_Sate;

    [SerializeField]
    public int Larder_Manager_Research_Points;

    [SerializeField]
    public List<Food_Section_Save_State> Meat_Manager_Save_Sate;

    [SerializeField]
    public int Meat_Manager_Research_Points;

    [SerializeField]
    public List<Food_Section_Save_State> Pastry_Manager_Save_Sate;

    [SerializeField]
    public int Pastry_Manager_Research_Points;

    [SerializeField]
    public List<Food_Section_Save_State> Sauce_Manager_Save_Sate;

    [SerializeField]
    public int Sauce_Manager_Research_Points;

    [SerializeField]
    public List<Food_Section_Save_State> Vegetable_Manager_Save_Sate;

    [SerializeField]
    public int Vegetable_Manager_Research_Points;

    [SerializeField]
    public List<Item_Save_State> Item_Save_State;

    [SerializeField]
    public List<Resource_Save_State> Resource_Save_State;

    [SerializeField]
    public List<Chef_Research_Save_State> Chef_Research_Save_State = new List<global::Chef_Research_Save_State>();

    [SerializeField]
    public List<Recipe_Save_State> Recipe_Save_State = new List<global::Recipe_Save_State>();
}

/// <summary>
/// 
/// </summary>
[Serializable]
public class Player_Save_State
{
    [SerializeField]
    public float Total_Xp;
    [SerializeField]
    public int Current_Level;
    [SerializeField]
    public int Xp_Till_Next_Level;
    [SerializeField]
    public float Current_Experience;
    [SerializeField]
    public int ResearchPoints;

    [SerializeField]
    public float Remaining_Points;
    [SerializeField]
    public float Total_Points;
    [SerializeField]
    public float Lifetime_Currency;
}

/// <summary>
/// 
/// </summary>
[Serializable]
public class Recipe_Save_State
{
    public bool Unlocked;
}

/// <summary>
/// 
/// </summary>
[Serializable]
public class Food_Section_Save_State
{
    [SerializeField]
    public float Cost;
    [SerializeField]
    public int Count;
    [SerializeField]
    public float Resource_Rate;
    [SerializeField]
    public bool Is_Purchased;
    [SerializeField]
    public bool On_Complete;
    [SerializeField]
    public bool Unlocked;
    [SerializeField]
    public float Current_Time;
    [SerializeField]
    public bool Started_Timer;
}

/// <summary>
/// 
/// </summary>
[Serializable]
public class Chef_Research_Save_State
{
    [SerializeField]
    public string Name;

    [SerializeField]
    public int RequiredLevel;
    [SerializeField]
    public bool Unlocked;

    [SerializeField]
    public int Cost_To_Level;
    [SerializeField]
    public int Max_Level;
    [SerializeField]
    public int Current_Level;
}

/// <summary>
/// 
/// </summary>
[Serializable]
public class Resource_Save_State
{
    [SerializeField]
    public int ID;
    [SerializeField]
    public int Count;
    [SerializeField]
    public int Slot_ID;
}

/// <summary>
/// 
/// </summary>
[Serializable]
public class Item_Save_State
{
    [SerializeField]
    public int ID;
    [SerializeField]
    public int Count;
    [SerializeField]
    public int Slot_ID;

    [SerializeField]
    public string Name;
    [SerializeField]
    public int Image_ID;
    [SerializeField]
    public bool Stackable;
    [SerializeField]
    public ItemRarity Item_Rarity;
    [SerializeField]
    public SlotType Slot_Type;
    [SerializeField]
    public ResourceType Resource_Type;
    [SerializeField]
    public float Cost_To_Purchase;
    [SerializeField]
    public List<Bonus_Save_State> Bonus_Save_State;
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

/// <summary>
/// 
/// </summary>
[Serializable]
public class Bonus_Save_State
{
    [SerializeField]
    public BonusType Bonus_Type;
    [SerializeField]
    public float Amount;
}