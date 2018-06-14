using UnityEngine;
using System.Collections;

public class RefrigeratorsManager : MonoBehaviour {

    public GameObject Inventory;
    public GameObject Player;

    private bool triggerEntered = false;

    private void Update()
    {
        // We check if user pressed PickUp key and character is inside trigger
        if (Input.GetButtonDown("PickUp") && triggerEntered == true)
        {
            Inventory.GetComponent<Gui_Anim>().Move_In();
            PickupDialog.Instance.DeActivate();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject == Player)
        {
            PickupDialog.Instance.Activate("E", " OPEN Refrigerator");
            triggerEntered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PickupDialog.Instance.DeActivate();
        triggerEntered = false;
        Inventory.GetComponent<Gui_Anim>().Move_Out_Instant();
    }
}
