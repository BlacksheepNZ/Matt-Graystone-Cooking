using UnityEngine;

public class GUI_Manager : MonoBehaviour
{
    public Vector2 Move_To_Position;
    public Vector2 Hide_Position;

    public void Activate()
    {
        this.gameObject.GetComponent<Transform>().localPosition = Move_To_Position;
    }

    public void DeActivate()
    {
        this.gameObject.GetComponent<Transform>().localPosition = Hide_Position;
    }

}
