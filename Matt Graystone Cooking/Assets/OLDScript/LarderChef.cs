using UnityEngine;
using System.Collections;

public class LarderChef : MonoBehaviour
{

    public GameObject TabHolder_Larder;
    public GameObject Player;

    private bool triggerEntered = false;

    private void Update()
    {
        // We check if user pressed PickUp key and character is inside trigger
        if (Input.GetButtonDown("PickUp") && triggerEntered == true)
        {
            TabHolder_Larder.GetComponent<Gui_Anim>().Move_In();
            PickupDialog.Instance.DeActivate();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject == Player)
        {
            PickupDialog.Instance.Activate("E", " Talk Larder Chef");
            triggerEntered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PickupDialog.Instance.DeActivate();
        triggerEntered = false;
        TabHolder_Larder.GetComponent<Gui_Anim>().Move_Out_Instant();
    }
}
