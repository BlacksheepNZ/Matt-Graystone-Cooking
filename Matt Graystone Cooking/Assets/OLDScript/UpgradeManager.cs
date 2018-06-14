using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour {

    private static UpgradeManager instance;
    public static UpgradeManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<UpgradeManager>();
            }

            return UpgradeManager.instance;
        }
    }

    public GameObject UnlockParent;
    public GameObject UnlockPrefab;
    public Transform Parent;

    public Unlock AddUpgrade(string name, string decription, Sprite image)
    {
        GameObject purchasablePrefab = Instantiate(UnlockPrefab);
        purchasablePrefab.transform.SetParent(Parent);
        purchasablePrefab.transform.localScale = new Vector3(1, 1, 1);
        purchasablePrefab.transform.position = Parent.position;

        Unlock unlock = purchasablePrefab.GetComponent<Unlock>();
        unlock.name = name;
        unlock.Decription = decription;
        unlock.Image = image;

        purchasablePrefab.transform.FindChild("Image").GetComponent<Image>().sprite = image;
        purchasablePrefab.transform.FindChild("TextName").GetComponent<Text>().text = name;
        purchasablePrefab.transform.FindChild("TextDecription").GetComponent<Text>().text = decription;

        return unlock;
    }

    public void ShowPanel()
    {
        UnlockParent.SetActive(true);
    }

    public void HidePanel()
    {
        UnlockParent.SetActive(false);
    }
}
