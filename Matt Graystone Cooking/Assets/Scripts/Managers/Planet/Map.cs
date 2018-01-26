using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public enum MapType
{
    Zone1,
    Zone2,
    Zone3,
}

public class Map : MonoBehaviour
{
    private static Map instance;
    public static Map Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Map>();
            }

            return Map.instance;
        }
    }

    public MapType mapType;

    public GameObject[] BackgroundPrefab;
    public GameObject[] HomePlanetPrefab;
    public GameObject[] TargetMiningBasePrefab;

    public int Current;

    public GameObject Background;
    public GameObject HomePlanet;
    public GameObject TargetMiningBase;

    public GameObject trastition;

    public string CurrentPlanetName;

    void Start()
    {
        Current = (int)mapType;

        IniNewMap();

        if((int)mapType > HomePlanetPrefab.Length)
        {
            Debug.Log("More Zones than planet.");
        }

        CurrentPlanetName = HomePlanet.name;
    }

    void IniNewMap()
    {
        IniGameObjectBackground(Background, BackgroundPrefab[Current]);
        IniGameObjectPlanet(HomePlanet, HomePlanetPrefab[Current]);
        IniGameObjectMoon(TargetMiningBase, TargetMiningBasePrefab[Current]);
    }

    public void IniGameObjectBackground(GameObject item, GameObject item2)
    {
        item.transform.localScale = item2.transform.localScale;
        item.transform.position = item2.transform.position;
        item.transform.GetComponent<Image>().sprite = item2.transform.GetComponent<Image>().sprite;
        item.name = item2.name;
    }

    public void IniGameObjectPlanet(GameObject item, GameObject item2)
    {
        item.transform.localScale = item2.transform.localScale;
        item.transform.position = item2.transform.position;
        item.GetComponent<Renderer>().sharedMaterial = item2.GetComponent<Renderer>().sharedMaterial;
        item.name = item2.name;
        CurrentPlanetName = item.name;

        item.transform.GetComponent<Gravity>().range = item2.transform.GetComponent<Gravity>().range;
    }

    public void IniGameObjectMoon(GameObject item, GameObject item2)
    {
        item.transform.localScale = item2.transform.localScale;
        item.transform.position = item2.transform.position;
        item.transform.GetChild(0).GetComponent<Renderer>().sharedMaterial = item2.transform.GetChild(0).GetComponent<Renderer>().sharedMaterial;
        item.name = item2.name;
    }

    public void Update()
    {
        if (Current != (int)mapType)
        {
            Current = (int)mapType;

            IniNewMap();

            trastition.SetActive(true);
            StartCoroutine(TransitionFade());
        }
    }

    IEnumerator TransitionFade()
    {
        trastition.transform.GetChild(0).transform.GetChild(1).transform.FindChild("TransitionText").transform.GetComponent<Text>().text = CurrentPlanetName;

        while (trastition.activeInHierarchy == true)
        {
            //ParticleSystem particleSystem = stars.transform.GetComponent<ParticleSystem>();

            //ParticleSystem.Particle[] p = new ParticleSystem.Particle[particleSystem.particleCount + 1];
            //int l = particleSystem.GetParticles(p);

            //int i = 0;
            //while (i < l)
            //{
            //    p[i].velocity = new Vector3(0, 0, p[i].remainingLifetime / p[i].startLifetime * 5000F);
            //    i++;
            //}

            //particleSystem.SetParticles(p, l);

            //float speed = ps.playbackSpeed;
            //speed += 0.5f * Time.deltaTime;
            //speed = Mathf.Clamp(speed, iniSpeed, maxSpeed);

            CanvasGroup c = trastition.GetComponent<CanvasGroup>();
            c.alpha -= 0.5f * Time.deltaTime;

            if (c.alpha == 0)
            {
                c.alpha = 255;
                trastition.SetActive(false);
            }

            yield return new WaitForSeconds(0.01f);
        }

        yield return null;
    }

    public void ChangeMap(MapType MapType)
    {
        mapType = MapType;
    }
}
