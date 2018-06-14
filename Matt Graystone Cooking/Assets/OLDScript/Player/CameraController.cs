using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public float smoothing = 1.0f;
    public Transform target;

    public Vector3 offset;

    void Start()
    {
        //Assign offset for camera
        offset = transform.position - target.transform.position;
    }

    private void FixedUpdate()
    {
        //If target is still valid, it means the player has not been destroyed yet, so move camera
        if (target != null)
        {
            //Vector3 targetCamPos = target.transform.position + offset;
            //transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing);

            //transform.LookAt(target);

            offset = Quaternion.AngleAxis(target.localRotation.x * smoothing, Vector3.up) * offset;
            transform.position = target.position + offset;
            transform.LookAt(target.position);
        }
    }

}