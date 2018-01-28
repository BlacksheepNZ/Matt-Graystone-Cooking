using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LitJson;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

namespace ItemBuilder
{
    public partial class Form1 : Form
    {
        public List<Sprite> Images;

        //items
        public string Item_Location = @"C:\Users\matthew\Desktop\Matt Graystone Cooking\Matt-Graystone-Cooking\Matt Graystone Cooking\Assets\StreamingAssets\items";
        public string Recipe_Location = @"C:\Users\matthew\Desktop\Matt Graystone Cooking\Matt-Graystone-Cooking\Matt Graystone Cooking\Assets\StreamingAssets\recipeTest.json";
        private JsonData ItemJsonData = new JsonData();
        public List<Item> Item_Database = new List<Item>();

        string FileType = ".json";

        public Form1()
        {
            InitializeComponent();

            LoadJsonData(Item_Location, ItemJsonData, Item_Database);
        }

        public void LoadJsonData(string location, JsonData data, List<Item> item)
        {
            data = JsonMapper.ToObject(File.ReadAllText(location + FileType));

            for (int i = 0; i < data.Count; i++)
            {
                string Name = (string)data[i]["Name"];
                //if (Name == "Empty")
                //    continue;

                int ID = int.Parse((string)data[i]["ID"]);
                int imageId = 0; //Function.FindImageID(Images, (string)ItemJsonData[i]["SpriteName"]);
                bool Satackable = (bool)data[i]["Stackable"];
                ItemRarity ItemRarity = (ItemRarity)Enum.Parse(typeof(ItemRarity), (string)data[i]["ItemRarity"]);
                ItemType ItemType = (ItemType)Enum.Parse(typeof(ItemType), (string)data[i]["ItemType"]);
                ResourceType ResourceType = (ResourceType)Enum.Parse(typeof(ResourceType), (string)data[i]["ResourceType"]);

                item.Add(new Item(
                    Name,
                    ID,
                    imageId,
                    Satackable,
                    ItemRarity,
                    ItemType,
                    ResourceType));


            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = Enum.GetNames(typeof(ItemRarity));
            comboBox2.DataSource = Enum.GetNames(typeof(ItemType));
            comboBox3.DataSource = Enum.GetNames(typeof(ResourceType));

            BindData(comboBox5);
            BindData(comboBox6);
            BindData(comboBox7);
            BindData(comboBox8);
            BindData(comboBox9);
            BindData(comboBox10);
            BindData(comboBox11);
            BindData(comboBox12);
            BindData(comboBox13);
        }

        public void BindData(ComboBox comboBox)
        {
            comboBox.DataSource = Item_Database;
            comboBox.BindingContext = new BindingContext();
            comboBox.DisplayMember = "Name";
        }

        string key;

        string a = "000";
        string b = "000";
        string c = "000";
        string d = "000";
        string key_e = "000";
        string f = "000";
        string g = "000";
        string h = "000";
        string i = "000";

        private void UpdateKey()
        {
            key = a + b + c + d + key_e + f + g + h + i;

            textBox3.Text = key;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            BinaryFormatter format = new BinaryFormatter();
            FileStream file = File.Create(Recipe_Location);

            string name = textBox4.Name;
            string sell_value = textBox5.Name;
            List<RecipeItem> recipe_item = new List<RecipeItem>();
            //todo add items

            Recipe save = new Recipe();
            save.Name = name;
            save.Key = key;
            save.Items = recipe_item;
            save.SellValue = 1;

            format.Serialize(file, save);
            file.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            a = Item_Database[comboBox5.SelectedIndex].ID.ToString("000");
            b = Item_Database[comboBox6.SelectedIndex].ID.ToString("000");
            c = Item_Database[comboBox7.SelectedIndex].ID.ToString("000");
            d = Item_Database[comboBox8.SelectedIndex].ID.ToString("000");
            key_e = Item_Database[comboBox9.SelectedIndex].ID.ToString("000");
            f = Item_Database[comboBox10.SelectedIndex].ID.ToString("000");
            g = Item_Database[comboBox11.SelectedIndex].ID.ToString("000");
            h = Item_Database[comboBox12.SelectedIndex].ID.ToString("000");
            i = Item_Database[comboBox13.SelectedIndex].ID.ToString("000");


            UpdateKey();
        }
    }
}
