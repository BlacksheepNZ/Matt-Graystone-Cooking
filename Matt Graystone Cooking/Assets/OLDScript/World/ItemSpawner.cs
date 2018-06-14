using UnityEngine;
using System.Collections;

public class ItemSpawner : MonoBehaviour
{
    public GameObject Item_Prefab;
    private float Off_Set = 5;

    private GameObject PurchasablePrefab;

    public void CreateGameObject(int item_id, int count)
    {
        GameObject purchasablePrefab = Instantiate(Item_Prefab);
        purchasablePrefab.transform.SetParent(transform);
        purchasablePrefab.transform.localScale = new Vector3(1, 1, 1);
        purchasablePrefab.transform.localPosition =
            new Vector3(
                0,
                0,
                transform.localPosition.y + Off_Set);// transform.position;

        ItemData itemData = purchasablePrefab.GetComponent<ItemData>();
        itemData.Item = SaveLoad.Instance.Item_Database[item_id];
        itemData.count = count;
        purchasablePrefab.name = itemData.Item.Name;

        PurchasablePrefab = purchasablePrefab;
    }

    //collision events

    public GameObject Player;

    private bool triggerEntered = false;

    private void Update()
    {
        // We check if user pressed PickUp key and character is inside trigger
        if (Input.GetButtonDown("PickUp") && triggerEntered == true)
        {
            //add item to inventory
            ItemData item = PurchasablePrefab.GetComponent<ItemData>();
            if (item != null)
            {
                Inventory.Instance.AddItem(item.Item.ID, item.count);
            }

            DestroyImmediate(PurchasablePrefab, true);
            DeActivate();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (PurchasablePrefab == null)
            return;

        ItemData itemData = PurchasablePrefab.GetComponent<ItemData>();

        if (collision.gameObject == Player)
        {
            PickupDialog.Instance.Activate("E", "PICK UP " + itemData.name);
            triggerEntered = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        DeActivate();
    }

    private void DeActivate()
    {
        PickupDialog.Instance.DeActivate();
        triggerEntered = false;
    }
}
