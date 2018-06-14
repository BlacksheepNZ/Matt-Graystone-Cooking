using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Earth : MonoBehaviour {

    private static Earth instance;
    public static Earth Instance
    {
        get
        {
            if (instance == null)
            {
                DontDestroyOnLoad(instance);
                instance = GameObject.FindObjectOfType<Earth>();
            }

            return Earth.instance;
        }
    }

    public GameObject buildingPrefab;

    public float Speed = 10.0f;

    public Transform transformParent;

    public int NumberOfBuildings = 1;

    public GameObject Highlight;

    public GameObject BuildingParent;

    float minFov = 30f;
    float maxFov = 60f;
    float sensitivity = 20f;

    // Update is called once per frame
    void Update () {
        transformParent.transform.Rotate(new Vector3(0, -1, 0), Speed * Time.fixedDeltaTime);

        float fov = Camera.main.fieldOfView;
        fov += Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        Camera.main.fieldOfView = fov;
    }

    private void OnMouseEnter()
    {
        Highlight.GetComponent<Light>().intensity = 7.5f;
    }

    private void OnMouseExit()
    {
        Highlight.GetComponent<Light>().intensity = 2.0f;
    }

    private void OnMouseDown()
    {
        RaycastHit hitInfo = new RaycastHit();
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.tag == this.transform.tag)
        {
            SoundManager.Instance.Play_Click();

            //StartCoroutine(Explosion.Instance.OnPlay());
            //play animation
            Game.Instance.AddGold(1);
        }
    }
}
