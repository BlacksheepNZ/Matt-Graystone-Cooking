/// <summary>
/// Make sure these match the id in item database
/// </summary>
public enum ResourceType
{
    Empty = -1,
    Food_1_Pastry = 1,
    Food_2_Pastry,
    Food_3_Pastry,
    Food_4_Pastry,
    Food_5_Pastry,
    Food_6_Pastry,
    Food_7_Pastry,
    Food_8_Pastry,
    Food_9_Pastry,
    Food_10_Pastry,
    Food_1_Larder = 100,
    Food_2_Larder,
    Food_3_Larder,
    Food_4_Larder,
    Food_5_Larder,
    Food_6_Larder,
    Food_7_Larder,
    Food_8_Larder,
    Food_9_Larder,
    Food_10_Larder,
    Food_1_Sauce = 200,
    Food_2_Sauce,
    Food_3_Sauce,
    Food_4_Sauce,
    Food_5_Sauce,
    Food_6_Sauce,
    Food_7_Sauce,
    Food_8_Sauce,
    Food_9_Sauce,
    Food_10_Sauce,
    Food_1_Fish = 300,
    Food_2_Fish,
    Food_3_Fish,
    Food_4_Fish,
    Food_5_Fish,
    Food_6_Fish,
    Food_7_Fish,
    Food_8_Fish,
    Food_9_Fish,
    Food_10_Fish,
    Food_1_Vegetable = 400,
    Food_2_Vegetable,
    Food_3_Vegetable,
    Food_4_Vegetable,
    Food_5_Vegetable,
    Food_6_Vegetable,
    Food_7_Vegetable,
    Food_8_Vegetable,
    Food_9_Vegetable,
    Food_10_Vegetable,
    Food_1_Meat = 500,
    Food_2_Meat,
    Food_3_Meat,
    Food_4_Meat,
    Food_5_Meat,
    Food_6_Meat,
    Food_7_Meat,
    Food_8_Meat,
    Food_9_Meat,
    Food_10_Meat,
}

/// <summary>
/// 
/// </summary>
public enum ItemType
{
    General = 0,
    Consumable = 1,
}

/// <summary>
/// 
/// </summary>
public enum ItemRarity
{
    Common = 0,
    Uncommon = 1,
    Rare = 2,
    Epic = 3,
    Legendary= 4,
}

/// <summary>
/// Not implemented
/// </summary>
public enum BonusType
{
    Empty,
    DecreseSpeed, //implemented
    DecreasePurchaseCost, //implemented
    IncreaseCurrency, //implemented
    IncreaseResource, //implemeted
    CritChance,
    MineNextTeir,
    PowerNode,
    Consumable,
}

/// <summary>
/// 
/// </summary>
public class Resource
{
    public static int ItemNumberOFStats(ItemRarity ItemRarity)
    {
        switch (ItemRarity)
        {
            default:
                return 0;
            case ItemRarity.Common:
                return 1;
            case ItemRarity.Uncommon:
                return 2;
            case ItemRarity.Rare:
                return 3;
            case ItemRarity.Epic:
                return 4;
            case ItemRarity.Legendary:
                return 5;
        }
    }

    public static string ItemRarityColor(ItemRarity ItemRarity)
    {
        switch (ItemRarity)
        {
            default:
                return "#FFF";
            case ItemRarity.Common:
                return "#FFF";
            case ItemRarity.Uncommon:
                return "#00FF00";
            case ItemRarity.Rare:
                return "#0473f0";
            case ItemRarity.Epic:
                return "#800080";
            case ItemRarity.Legendary:
                return "#FFA500";
        }
    }
}
