using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StartingSide
{
    Up = 0,
    Down = 1,
    Left = 2,
    Right = 3,
}
public enum StartingDirection
{
    Left = 0,
    Right = 1,
}

public class Comet : MonoBehaviour {

    public Vector3 speed;
    public Vector3 position;

    private StartingSide StartingSide;
    private StartingDirection StartingDirection;

    //public Reward Reward;

    public void InIComet(float disatnce, float velocity)
    {
        Disatnce = disatnce;
        Velocity = velocity;
        int randomStartingSide = Random.Range(0, 4);
        StartingSide = (StartingSide)randomStartingSide;
        int randomStartingDirection = Random.Range(0, 3);
        StartingDirection = (StartingDirection)randomStartingDirection;

        GetStartingPosition();

        this.transform.position = position;

        Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
        rigidbody.AddForce(speed);
    }

    private float Disatnce;
    private float Velocity;

    private void GetStartingPosition()
    {
        switch (StartingSide)
        {
            default:
                break;
            case StartingSide.Up:
                position = new Vector3(0, Disatnce, 0);
                switch (StartingDirection)
                {
                    case StartingDirection.Left:
                        speed = new Vector3(Velocity, 0, 0);
                        break;
                    case StartingDirection.Right:
                        speed = new Vector3(-Velocity, 0, 0);
                        break;
                }
                break;
            case StartingSide.Down:
                position = new Vector3(0, -Disatnce, 0);
                switch (StartingDirection)
                {
                    case StartingDirection.Left:
                        speed = new Vector3(Velocity, 0, 0);
                        break;
                    case StartingDirection.Right:
                        speed = new Vector3(-Velocity, 0, 0);
                        break;
                }
                break;
            case StartingSide.Left:
                position = new Vector3(-Disatnce, 0, 0);
                switch (StartingDirection)
                {
                    case StartingDirection.Left:
                        speed = new Vector3(0, Velocity, 0);
                        break;
                    case StartingDirection.Right:
                        speed = new Vector3(0, -Velocity, 0);
                        break;
                }
                break;
            case StartingSide.Right:
                position = new Vector3(Disatnce, 0, 0);
                switch (StartingDirection)
                {
                    case StartingDirection.Left:
                        speed = new Vector3(0, Velocity, 0);
                        break;
                    case StartingDirection.Right:
                        speed = new Vector3(0, -Velocity, 0);
                        break;
                }
                break;

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == this.gameObject.name)
        {
            Destroy(this.gameObject);
        }
        if (collision.gameObject.name == "Earth")
        {
            Destroy(this.gameObject);
        }
        if (collision.gameObject.name == "Moon")
        {
            Destroy(this.gameObject);
        }
    }

    private void OnMouseDown()
    {
        RaycastHit hitInfo = new RaycastHit();
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.tag == this.transform.tag)
        {
            Destroy(this.transform.gameObject);

            //Reward.Begin(ScavangerManager.Instance.GetRewardRate());
        }
    }
}
