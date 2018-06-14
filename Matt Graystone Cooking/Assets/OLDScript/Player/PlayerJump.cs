using UnityEngine;
using System.Collections;

public class PlayerJump : MonoBehaviour {

    public float Fall_Muiltiplyer = 2.5f;
    public float Low_Jump_Muiltiplyer = 2f;

    private Rigidbody Rigid_Body;

    private void Awake()
    {
        Rigid_Body = GetComponent<Rigidbody>();
    }

    [Range(1, 100)]
    public float JumpVelocity = 25;

    public void FixedUpdate()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Rigid_Body.velocity = Vector3.up * JumpVelocity;
        }

        if (Rigid_Body.velocity.y < 0)
        {
            Rigid_Body.velocity += Vector3.up
                * Physics.gravity.y
                * (Fall_Muiltiplyer - 1)
                * Time.deltaTime;
        }
        else if (Rigid_Body.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            Rigid_Body.velocity += Vector3.up
                * Physics.gravity.y
                * (Low_Jump_Muiltiplyer - 1)
                * Time.deltaTime;
        }
    }
}
