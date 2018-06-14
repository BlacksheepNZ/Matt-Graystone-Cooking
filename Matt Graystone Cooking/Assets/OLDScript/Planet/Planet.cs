using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Planet : MonoBehaviour {

    private static Planet instance;
    public static Planet Instance
    {
        get
        {
            if (instance == null)
            {
                DontDestroyOnLoad(instance);
                instance = GameObject.FindObjectOfType<Planet>();
            }

            return Planet.instance;
        }
    }

    public GameObject buildingPrefab;

    public float Speed = 0.0f;

    public Transform transformParent;

    public int NumberOfBuildings = 1;

    public Vector3 Roation;

    private void Start()
    {
        if (buildingPrefab == null)
            return;

        CreateBuilding();
    }

    public void CreateBuilding()
    {
        float radius = gameObject.GetComponent<SphereCollider>().radius;
        Vector3 spawnPosition = UnityEngine.Random.onUnitSphere * (radius + 0 * 0.5f) + gameObject.transform.position;

        GameObject building = Instantiate(buildingPrefab);
        building.transform.SetParent(gameObject.transform.FindChild("BuildingManager").transform);
        building.transform.localScale = new Vector3(0.02f, 0.05f, 0.02f);
        building.transform.localPosition = Vector2.zero;
        building.transform.localPosition = spawnPosition;
        building.transform.LookAt(gameObject.transform.position);
        building.transform.Rotate(-90, 0, 0);
    }

    // Update is called once per frame
    void Update () {
        transformParent.transform.Rotate(Roation, Speed * Time.fixedDeltaTime);
    }
}
