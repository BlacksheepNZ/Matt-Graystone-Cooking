using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PickupDialog : MonoBehaviour {

    private static PickupDialog instance;
    public static PickupDialog Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<PickupDialog>();
            }

            return global::PickupDialog.instance;
        }
    }

    public GameObject Panel_Pickup_Dialog;
    public Text Text_Key;
    public Text Text_Pickup_Item_Name;

    private void Start()
    {
        DeActivate();
    }

    public void Activate(string key, string item_text)
    {
        Panel_Pickup_Dialog.gameObject.SetActive(true);
        Text_Key.text = key;
        Text_Pickup_Item_Name.text = item_text;
    }

    public void DeActivate()
    {
        Panel_Pickup_Dialog.gameObject.SetActive(false);
        Text_Key.text = "";
        Text_Pickup_Item_Name.text = "";
    }
}
